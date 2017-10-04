#include "Palette.h"

EXPORT uint SNEStoPCRGB(const uint val)
{
	static uint rgb;

	__asm
	{
		mov eax, val
		and eax, 01fh
		shl eax, (8 * 3) - 5
		mov rgb, eax
		mov eax, val
		and eax, 01fh << 5
		shl eax, (8 - 5) * 2
		or rgb, eax
		mov eax, val
		and eax, 01fh << 10
		shr eax, (5 * 3) - 8
		or eax, rgb
	}
}

EXPORT uint PCtoSNESRGB(const uint val)
{
	static uint rgb;

	__asm
	{
		mov eax, val
		and eax, 0f8h
		shr eax, (8 * 3) - 5
		mov rgb, eax
		mov eax, val
		and eax, 0f800h
		shr eax, (8 - 5) * 2
		or rgb, eax
		mov eax, val
		and eax, 0f80000h
		shl eax, (5 * 3) - 8
		or eax, rgb
	}
}

EXPORT void CreatePaletteData(uint *dest, const byte *src)
{
	static uint val, rgb, i;

	__asm
	{
		push esi
		push edi
		mov i, 100h
		mov esi, src
		mov edi, dest
L1:
		dec i
		jl E1
		mov eax, [esi]
		and eax, 7fffh
		mov val, eax
		and eax, 01fh
		shl eax, (8 * 3) - 5
		mov rgb, eax
		mov eax, val
		and eax, 01fh << 5
		shl eax, (8 - 5) * 2
		or rgb, eax
		mov eax, val
		and eax, 01fh << 10
		shr eax, (5 * 3) - 8
		or eax, rgb
		or eax, 0ff000000h
		mov [edi], eax
		add edi, 4
		add esi, 2
		jmp L1
E1:
		mov i, 10h
L2:
		dec i
		jl E2
		sub edi, 40h
		mov eax, [edi]
		and eax, 0ffffffh
		mov [edi], eax
		jmp L2
E2:
		pop edi
		pop esi
	}
}

EXPORT void DrawPalette(uint *scan0, uint* palette)
{
	static int i, j, x, y;

	__asm
	{
		push esi
		push edi
		mov esi, scan0
		mov edi, palette
		mov y, 10h
loop_y:
		dec y
		jl E
		mov x, 10h
		add esi, 4000h-400h
loop_x:
		dec x
		jl loop_y
		mov eax, [edi]
		add edi, 4
		mov j, 10h
		sub esi, 4000h-40h
loop_j:
		dec j
		jl loop_x
		mov i, 10h
		add esi, 400h-40h
loop_i:
		dec i
		jl loop_j
		mov [esi], eax
		add esi, 4
		jmp loop_i
E:
		pop edi
		pop esi
	}
}

EXPORT void DrawPaletteRow(uint *scan0, uint *palette)
{
	static int i, j, x;

	__asm
	{
		push esi
		add esi, 4000h-400h
		push edi
		mov esi, scan0
		mov edi, palette
		mov x, 10h
loop_x:
		dec x
		jl E
		mov eax, [edi]
		add edi, 4
		mov j, 10h
		sub esi, 4000h-40h
loop_j:
		dec j
		jl loop_x
		mov i, 10h
		add esi, 400h-40h
loop_i:
		dec i
		jl loop_j
		mov [esi], eax
		add esi, 4
		jmp loop_i
E:
		pop edi
		pop esi
	}
}