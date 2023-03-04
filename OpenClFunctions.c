#include "pch.h"
#include "BenchmarkDllCommon.h"

#define CL_USE_DEPRECATED_OPENCL_1_2_APIS
#include <CL/cl.h>

// These functions will generally not handle out of range indices. Calling C# code
// is responsible for making sure platform/device indices do not go out of bounds.
__declspec(dllexport) int __stdcall SetOpenCLContext(int platformIndex, int deviceIndex);
__declspec(dllexport) int __stdcall GetPlatformCount();
__declspec(dllexport) int __stdcall GetDeviceCount(int platformIndex);
__declspec(dllexport) int __stdcall GetDeviceName(int platformIndex, int deviceIndex, char* deviceNamePtr, int maxDeviceNameLen);
__declspec(dllexport) int __stdcall GetPlatformName(int platformIndex, char* platformNamePtr, int maxPlatformNameLen);
__declspec(dllexport) float __stdcall RunCLLatencyTest(uint32_t size_kb, uint32_t iterations, enum CLTestType testType);
__declspec(dllexport) int __stdcall InitializeLatencyTest(enum CLTestType testType);
__declspec(dllexport) int __stdcall DeinitializeLatencyTest();
__declspec(dllexport) uint64_t __stdcall GetDeviceMaxConstantBufferSize();
__declspec(dllexport) uint64_t __stdcall GetDeviceMaxBufferSize();
__declspec(dllexport) uint64_t __stdcall GetDeviceMaxTextureSize();

// Internal convenience functions
cl_device_id GetDeviceIdFromIndex(int platformIndex, int deviceIndex);
cl_platform_id GetPlatformIdFromIndex(int platformIndex);

// Test run state
cl_context context;
cl_command_queue command_queue;
cl_device_id selected_device_id;
cl_program program;

/// <summary>
/// Set opencl context to use for subsequent tests
/// </summary>
/// <param name="platformIndex">platform index</param>
/// <param name="deviceIndex">device index</param>
/// <returns>0 (CL_SUCCESS) on success, opencl error code on fail</returns>
int SetOpenCLContext(int platformIndex, int deviceIndex)
{
	cl_int ret;
	cl_device_id deviceId = GetDeviceIdFromIndex(platformIndex, deviceIndex);
	context = clCreateContext(NULL, 1, &deviceId, NULL, NULL, &ret);
	if (ret != CL_SUCCESS) return ret;
	command_queue = clCreateCommandQueue(context, deviceId, 0, &ret);
	selected_device_id = deviceId;
	return ret;
}

/// <summary>
/// Get number of OpenCL platforms
/// </summary>
/// <returns>number of opencl platforms</returns>
int32_t GetPlatformCount()
{
	cl_uint platformCount;
	cl_int ret = clGetPlatformIDs(0, NULL, &platformCount);
	if (ret == CL_SUCCESS) return (int32_t)platformCount;
	else return ret;
}

/// <summary>
/// Get number of OpenCL devices for a platform
/// </summary>
/// <param name="platformIndex">platform index</param>
/// <returns>number of opencl devices</returns>
int32_t GetDeviceCount(int platformIndex)
{
	cl_uint deviceCount;
	cl_platform_id platformId = GetPlatformIdFromIndex(platformIndex);
	cl_uint ret = clGetDeviceIDs(platformId, CL_DEVICE_TYPE_ALL, 0, NULL, &deviceCount);
	if (ret == CL_SUCCESS) return (int32_t)deviceCount;
	else return ret;
}

int GetPlatformName(int platformIndex, char* platformNamePtr, int maxPlatformNameLen)
{
	memset(platformNamePtr, 0, maxPlatformNameLen);
	cl_platform_id platformId = GetPlatformIdFromIndex(platformIndex);

	size_t platformNameLength;
	cl_int ret = clGetPlatformInfo(platformId, CL_PLATFORM_NAME, 0, NULL, &platformNameLength);
	if (ret != CL_SUCCESS) return (int)ret;
	char* tempPlatformNameBuffer = (char*)malloc(platformNameLength + 1);
	ret = clGetPlatformInfo(platformId, CL_PLATFORM_NAME, platformNameLength, tempPlatformNameBuffer, NULL);
	if (ret != CL_SUCCESS) return (int)ret;

	memcpy(platformNamePtr, tempPlatformNameBuffer, platformNameLength >= maxPlatformNameLen ? maxPlatformNameLen - 1 : platformNameLength);
	free(tempPlatformNameBuffer);
	return 0;
}

int GetDeviceName(int platformIndex, int deviceIndex, char *deviceNamePtr, int maxDeviceNameLen)
{
	memset(deviceNamePtr, 0, maxDeviceNameLen);
	cl_device_id deviceId = GetDeviceIdFromIndex(platformIndex, deviceIndex);

	// Place the name into a buffer big enough to hold it, then copy out as much as we can into the 
	// buffer we're given from C# code
	size_t deviceNameLength;
	cl_int ret = clGetDeviceInfo(deviceId, CL_DEVICE_NAME, 0, NULL, &deviceNameLength);
	if (ret != CL_SUCCESS) return (int)ret;
	char *tempDeviceNameBuffer = (char*)malloc(deviceNameLength + 1);
	ret = clGetDeviceInfo(deviceId, CL_DEVICE_NAME, deviceNameLength, tempDeviceNameBuffer, &deviceNameLength);
	if (ret != CL_SUCCESS) return (int)ret;

	memcpy(deviceNamePtr, tempDeviceNameBuffer, deviceNameLength >= maxDeviceNameLen ? maxDeviceNameLen - 1 : deviceNameLength);
	free(tempDeviceNameBuffer);
	return 0;
}

/// <summary>
/// Get max constant buffer size. Device must be selected before calling this function
/// </summary>
/// <returns>Max constant buffer size in bytes</returns>
uint64_t GetDeviceMaxConstantBufferSize() {
	uint64_t constantBufferSize = 0;
	cl_int ret = clGetDeviceInfo(selected_device_id, CL_DEVICE_MAX_CONSTANT_BUFFER_SIZE, sizeof(cl_ulong), &constantBufferSize, NULL);
	if (ret != CL_SUCCESS) return 0;
	return constantBufferSize;
}

/// <summary>
/// Get max global memory allocation. Device must be selected before calling this function
/// </summary>
/// <returns>Max buffer size in bytes</returns>
uint64_t GetDeviceMaxBufferSize() {
	uint64_t bufferSize = 0;
	cl_int ret = clGetDeviceInfo(selected_device_id, CL_DEVICE_MAX_MEM_ALLOC_SIZE, sizeof(cl_ulong), &bufferSize, NULL);
	if (ret != CL_SUCCESS) return 0;
	return bufferSize;
}

/// <summary>
/// Get max 1D texture buffer size. Device must be selected first.
/// </summary>
/// <returns>Max buffer size in bytes</returns>
uint64_t GetDeviceMaxTextureSize() {
	uint64_t bufferSize = 0;
	cl_int ret = clGetDeviceInfo(selected_device_id, CL_DEVICE_IMAGE_MAX_BUFFER_SIZE, sizeof(cl_ulong), &bufferSize, NULL);
	if (ret != CL_SUCCESS) return 0;
	return bufferSize;
}

enum CLTestType
{
	None = 0,
	GlobalScalar = 1,
	GlobalVector = 2,
	ConstantScalar = 3,
	Texture = 4,
	Local = 5
};

#define MAX_SOURCE_SIZE (0x100000)
cl_kernel latencyTestKernel;

/// <summary>
/// Sets up state to run a latency test
/// </summary>
/// <param name="testType">Test type to set up for</param>
/// <returns>0 on success, something else if it went south</returns>
int InitializeLatencyTest(enum CLTestType testType)
{
	// build the latency test kernel for the device in question
	FILE* fp = NULL;
	char* sourceBuffer = NULL;
	size_t sourceSize;
	if (testType == Texture) fp = fopen("latencykernel_tex.cl", "rb");
	else fp = fopen("latencykernel.cl", "rb");
	if (!fp)
	{
		return -1;
	}

	// don't try to read the exact file because using ftell + rewinding via seek_set will 
	// result in garbage prepended to the buffer
	sourceBuffer = (char*)malloc(MAX_SOURCE_SIZE);
	memset(sourceBuffer, 0, MAX_SOURCE_SIZE);
	sourceSize = fread(sourceBuffer, 1, MAX_SOURCE_SIZE, fp);
	fclose(fp);

	// fread prepends three garbage characters before reading the actual file
	cl_int ret;
	program = clCreateProgramWithSource(context, 1, (const char**)&sourceBuffer, (const size_t*)&sourceSize, &ret);
	if (ret < 0) goto InitializeLatencyTestEnd;

	ret = clBuildProgram(program, 1, &selected_device_id, NULL, NULL, NULL);
	if (ret < 0) goto InitializeLatencyTestEnd;

	ret = -2;
	if (testType == GlobalScalar) latencyTestKernel = clCreateKernel(program, "unrolled_latency_test", &ret);
	else if (testType == GlobalVector) latencyTestKernel = clCreateKernel(program, "unrolled_latency_test_amdvectorworkaround", &ret);
	else if (testType == ConstantScalar) latencyTestKernel = clCreateKernel(program, "constant_unrolled_latency_test", &ret);
	else if (testType == Texture) latencyTestKernel = clCreateKernel(program, "tex_latency_test", &ret);
	else if (testType == Local) latencyTestKernel = clCreateKernel(program, "local_unrolled_latency_test", &ret);

InitializeLatencyTestEnd:
	free(sourceBuffer); // we don't have to hang onto it right?
	return ret;
}

/// <summary>
/// Cleans up OpenCL state after a test run completes
/// </summary>
/// <returns>error code if it didn't work</returns>
int DeinitializeLatencyTest()
{
	cl_int ret;
	ret = clReleaseKernel(latencyTestKernel);
	if (ret != CL_SUCCESS) return ret;
	ret = clReleaseCommandQueue(command_queue);
	if (ret != CL_SUCCESS) return ret;
	ret = clReleaseContext(context);
	if (ret != CL_SUCCESS) return ret;
	ret = clReleaseProgram(program);
	if (ret != CL_SUCCESS) return ret;
}

/// <summary>
/// Runs an opencl latency test. Context must be set before this function is called
/// </summary>
/// <param name="size_kb">test size, kb</param>
/// <param name="iterations">iteration count</param>
/// <returns>latency in ns, negative value on error</returns>
float RunCLLatencyTest(uint32_t size_kb, uint32_t iterations, enum CLTestType testType)
{
	cl_mem a_mem_obj = NULL, result_obj = NULL, tex_obj = NULL;
	struct timeb start, end;
	float latency = -1;
	uint32_t list_size = size_kb * 1024 / 4;
	uint32_t result;
	uint32_t* A = (uint32_t*)malloc(sizeof(uint32_t) * list_size);
	memset(A, 0, sizeof(uint32_t) * list_size);
	FillPatternArr(A, list_size, 64);

	size_t global_item_size = 1, local_item_size = 1;
	if (testType == GlobalVector)
	{
		global_item_size = 2;
		local_item_size = 2;
	}

	// copy to device, please work lol
	cl_int ret;
	a_mem_obj = clCreateBuffer(context, CL_MEM_READ_ONLY, list_size * sizeof(uint32_t), NULL, &ret);
	clEnqueueWriteBuffer(command_queue, a_mem_obj, CL_TRUE, 0, list_size * sizeof(uint32_t), A, 0, NULL, NULL);
	result_obj = clCreateBuffer(context, CL_MEM_READ_WRITE, sizeof(uint32_t), NULL, &ret);
	clEnqueueWriteBuffer(command_queue, result_obj, CL_TRUE, 0, sizeof(uint32_t), &result, 0, NULL, NULL);
	clFinish(command_queue);

	if (testType == Texture)
	{
		cl_image_format imageFormat;
		imageFormat.image_channel_data_type = CL_UNSIGNED_INT32;
		imageFormat.image_channel_order = CL_R;

		cl_image_desc imageDesc;
		memset(&imageDesc, 0, sizeof(cl_image_desc));
		imageDesc.buffer = a_mem_obj;
		imageDesc.image_type = CL_MEM_OBJECT_IMAGE1D_BUFFER;
		imageDesc.image_width = list_size; // width in pixels
		tex_obj = clCreateImage(context, CL_MEM_READ_ONLY, &imageFormat, &imageDesc, NULL, &ret);
	}

	if (testType == Texture) ret = clSetKernelArg(latencyTestKernel, 0, sizeof(cl_mem), (void*)&tex_obj);
	else ret = clSetKernelArg(latencyTestKernel, 0, sizeof(cl_mem), (void*)&a_mem_obj);
	ret = clSetKernelArg(latencyTestKernel, 1, sizeof(cl_int), (void*)&iterations);
	ret = clSetKernelArg(latencyTestKernel, 2, sizeof(cl_mem), (void*)&result_obj);

	ftime(&start);
	ret = clEnqueueNDRangeKernel(command_queue, latencyTestKernel, 1, NULL, &global_item_size, &local_item_size, 0, NULL, NULL);
	if (ret != CL_SUCCESS) goto RunCLLatencyTestEnd;
	ret = clFinish(command_queue);
	if (ret != CL_SUCCESS) goto RunCLLatencyTestEnd;
	ftime(&end);
	int64_t time_diff_ms = 1000 * (end.time - start.time) + (end.millitm - start.millitm);
	latency = (float)(1e6 * (float)time_diff_ms / (float)iterations);

RunCLLatencyTestEnd:
	free(A);
	clReleaseMemObject(a_mem_obj);
	clReleaseMemObject(result_obj);
	if (testType == Texture) clReleaseMemObject(tex_obj);
	return latency;
}

/// <summary>
/// Gets OpenCL platform id given a platform index.
/// </summary>
/// <param name="platformIndex">Platform index. Must be less than number of platforms</param>
/// <returns>Platform id</returns>
cl_platform_id GetPlatformIdFromIndex(int platformIndex)
{
	cl_uint platformCount = GetPlatformCount();
	cl_platform_id* platforms = (cl_platform_id*)malloc(platformCount * sizeof(cl_platform_id));
	cl_uint ret = clGetPlatformIDs(platformCount, platforms, NULL);
	cl_platform_id platform = platforms[platformIndex];
	free(platforms);
	return platform;
}

/// <summary>
/// Gets OpenCL device id given platform index and device index
/// </summary>
/// <param name="platformIndex">Platform index. Must be less than number of platforms</param>
/// <param name="deviceIndex">Device index. Must be less than number of devices</param>
/// <returns>Device id</returns>
cl_device_id GetDeviceIdFromIndex(int platformIndex, int deviceIndex)
{
	cl_platform_id platformId = GetPlatformIdFromIndex(platformIndex);
	cl_uint deviceCount = GetDeviceCount(platformIndex);
	cl_device_id *devices = (cl_device_id*)malloc(deviceCount * sizeof(cl_device_id));
	cl_int ret = clGetDeviceIDs(platformId, CL_DEVICE_TYPE_ALL, deviceCount, devices, NULL);
	cl_device_id deviceId = devices[deviceIndex];
	free(devices);
	return deviceId;
}