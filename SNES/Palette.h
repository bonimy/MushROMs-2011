#pragma once

#include "_base.h"
#include "list.h"

EXPORT uint SNEStoPCRGB(const uint val);

EXPORT uint PCtoSNESRGB(const uint val);

EXPORT void CreatePaletteData(uint *dest, const byte *src);

EXPORT void DrawPalette(uint *scan0, uint *palette);

EXPORT void DrawPaletteRow(uint *scan0, uint *palette);