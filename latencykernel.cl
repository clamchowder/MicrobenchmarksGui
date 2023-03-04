// unrolled until terascale no longer saw further improvement (10x unroll)
// assumes count will be a multiple of 10. but it won't be too inaccurate with a big count
// not divisible by 10
__kernel void unrolled_latency_test(__global const int* A, int count, __global int* ret) {
    int current = A[0];
    int result;
    for (int i = 0; i < count; i += 10) {
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
    }

    ret[0] = result;
}

__kernel void unrolled_latency_test_amdvectorworkaround(__global const int* A, int count, __global int* ret) {
    int start = A[1 + get_local_id(0)];
    int current = A[start];
    int result;
    for (int i = 0; i < count; i += 10) {
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
    }

    ret[0] = result;
}

// latency test like the unrolled one above, but with input as constant memory
__kernel void constant_unrolled_latency_test(__constant const int* A, int count, __global int* ret) {
    int current = A[0];
    int result;
    for (int i = 0; i < count; i += 10) {
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
        result += current;
        current = A[current];
    }

    ret[0] = result;
}

#define local_mem_test_size 1024
// uses local memory (LDS/shmem)
__kernel void local_unrolled_latency_test(__global const int* A, int count, __global int* ret) {
    __local int local_a[local_mem_test_size]; // 4 KB, should be present on all GPUs, amirite?
    // better be fast
    for (int i = get_local_id(0);i < local_mem_test_size; i += get_local_size(0))
        local_a[i] = A[i];
    barrier(CLK_LOCAL_MEM_FENCE);

    // everyone else can chill/get masked off
    if (get_local_id(0) == 0) {
        int current = local_a[0];
        int result;
        for (int i = 0; i < count; i += 10) {
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
            result += current;
            current = local_a[current];
        }

        ret[0] = result;
    }
}
