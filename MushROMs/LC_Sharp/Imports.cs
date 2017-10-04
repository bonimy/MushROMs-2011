using System;
using System.Runtime.InteropServices;

namespace LC_Sharp
{
    public static unsafe partial class LunarCompress
    {
        /// <summary>
        /// Path of the Lunar DLL file.
        /// </summary>
        public const string LunarDLLPath = "Lunar Compress.dll";

        [DllImport(LunarDLLPath)]
        private static extern uint LunarVersion();
        [DllImport(LunarDLLPath)]
        private static extern bool LunarOpenFile(string fileName, uint fileMode);
        [DllImport(LunarDLLPath)]
        private static extern IntPtr LunarOpenRAMFile(void* data, uint fileMode, uint size);
        [DllImport(LunarDLLPath)]
        private static extern int LunarSaveRAMFile(string fileName);
        [DllImport(LunarDLLPath)]
        private static extern bool LunarCloseFile();
        [DllImport(LunarDLLPath)]
        private static extern int LunarGetFileSize();
        [DllImport(LunarDLLPath)]
        private static extern int LunarReadFile(void* destination, uint size, uint address, uint seek);
        [DllImport(LunarDLLPath)]
        private static extern int LunarWriteFile(void* source, uint size, uint address, uint seek);
        [DllImport(LunarDLLPath)]
        private static extern int LunarSetFreeBytes(uint value);
        [DllImport(LunarDLLPath)]
        private static extern int LunarSNEStoPC(uint pointer, uint romType, uint header);
        [DllImport(LunarDLLPath)]
        private static extern int LunarPCtoSNES(uint pointer, uint romType, uint header);
        [DllImport(LunarDLLPath)]
        private static extern int LunarDecompress(void* destination, uint addressToStart, uint maxDataSize, uint format1, uint format2, uint* lastROMPosition);
        [DllImport(LunarDLLPath)]
        private static extern int LunarRecompress(void* source, void* destination, uint dataSize, uint maxDataSize, uint format, uint format2);
        [DllImport(LunarDLLPath)]
        private static extern bool LunarEraseArea(uint address, uint size);
        [DllImport(LunarDLLPath)]
        private static extern int LunarExpandROM(uint mBits);
        [DllImport(LunarDLLPath)]
        private static extern int LunarVerifyFreeSpace(uint addressStart, uint addressEnd, uint size, uint bankType);
        [DllImport(LunarDLLPath)]
        private static extern int LunarIPSCreate(void* hwnd, string ipsFileName, string romFileName, string rom2FileName, uint flags);
        [DllImport(LunarDLLPath)]
        private static extern int LunarIPSApply(void* hwnd, string ipsFileName, string romFileName, uint flags);
        [DllImport(LunarDLLPath)]
        private static extern bool LunarCreatePixelMap(void* source, void* destination, uint numTiles, uint gfxType);
        [DllImport(LunarDLLPath)]
        private static extern bool LunarCreateBppMap(byte* source, byte* destination, uint numTiles, uint gfxType);
        [DllImport(LunarDLLPath)]
        private static extern uint LunarSNEStoPCRGB(uint snesColor);
        [DllImport(LunarDLLPath)]
        private static extern uint LunarPCtoSNESRGB(uint pcColor);
        [DllImport(LunarDLLPath)]
        private static extern bool LunarRender8x8(uint* theMapBits, int theWidth, int theHeight, int displayAtX, int displayAtY, void* pixelMap, uint* pcPalette, uint map8Tile, uint extra);
        [DllImport(LunarDLLPath)]
        private static extern int LunarWriteRatArea(void* theData, uint size, uint preferredAddress, uint minRange, uint maxRange, uint flags);
        [DllImport(LunarDLLPath)]
        private static extern int LunarEraseRatArea(uint address, uint size, uint flags);
        [DllImport(LunarDLLPath)]
        private static extern int LunarGetRatAreaSize(uint address, uint flags);
    }
}