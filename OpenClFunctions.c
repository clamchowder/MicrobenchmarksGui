#include "pch.h"
#include "BenchmarkDllCommon.h"

#define CL_USE_DEPRECATED_OPENCL_1_2_APIS
#include <CL/cl.h>

// These functions will generally not handle out of range indices. Calling C# code
// is responsible for making sure platform/device indices do not go out of bounds.
__declspec(dllexport) int __stdcall SetOpenCLContext(int platformIndex, int deviceIndex);
__declspec(dllexport) int GetPlatformCount();
__declspec(dllexport) int GetDeviceCount(int platformIndex);
__declspec(dllexport) int GetDeviceName(int platformIndex, int deviceIndex, char* deviceNamePtr, int maxDeviceNameLen);
__declspec(dllexport) int GetPlatformName(int platformIndex, char* platformNamePtr, int maxPlatformNameLen);
__declspec(dllexport) float __stdcall RunCLLatencyTest(uint32_t size_kb, uint64_t iterations);
__declspec(dllexport) int InitializeLatencyTest(enum CLTestType testType);
__declspec(dllexport) int DeinitializeLatencyTest();

cl_device_id GetDeviceIdFromIndex(int platformIndex, int deviceIndex);
cl_platform_id GetPlatformIdFromIndex(int platformIndex);
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

enum CLTestType
{
	None = 0,
	GlobalScalar = 1,
	GlobalVector = 2,
	ConstantScalar = 3
};

#define MAX_SOURCE_SIZE (0x100000)
cl_kernel latencyTestKernel;
int InitializeLatencyTest(enum CLTestType testType)
{
	// build the latency test kernel for the device in question
	cl_kernel testKernel;
	FILE* fp = NULL;
	char* sourceBuffer = NULL;
	size_t sourceSize;
	fp = fopen("latencykernel.cl", "rb");
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

InitializeLatencyTestEnd:
	free(sourceBuffer); // we don't have to hang onto it right?
	return ret;
}

int DeinitializeLatencyTest()
{
	clReleaseKernel(latencyTestKernel);
	clReleaseCommandQueue(command_queue);
	clReleaseContext(context);
	clReleaseProgram(program);
}

/// <summary>
/// Runs an opencl latency test. Context must be set before this function is called
/// </summary>
/// <param name="size_kb">test size, kb</param>
/// <param name="iterations">iteration count</param>
/// <returns>latency in ns</returns>
float RunCLLatencyTest(uint32_t size_kb, uint64_t iterations, enum CLTestType testType)
{

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