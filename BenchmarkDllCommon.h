#pragma once
#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <math.h>
#include <sys/timeb.h>
#include <intrin.h>
#include <Windows.h>

void FillPatternArr(uint32_t* pattern_arr, uint32_t list_size, uint32_t byte_increment);
void FillTlbTestPatternArr(uint32_t* pattern_arr, uint32_t list_size, uint32_t cacheline_size, uint32_t page_size);