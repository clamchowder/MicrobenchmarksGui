// does not work on Fermi
__kernel void tex_latency_test(__read_only image1d_buffer_t A, int count, __global int* ret) {
    __local uint4 local_a[128];
    int localId = get_local_id(0);
    uint4 current = read_imageui(A, 0);
    for (int i = 0; i < count; i += 10) {
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        current = read_imageui(A, current.x);
        local_a[localId] = current;
    }

    ret[0] = local_a[localId].x;
}