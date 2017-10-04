using System;
using System.IO;
using System.Windows.Forms;
using MushROMs.Properties;

namespace LC_Sharp
{
    /// <summary>
    /// Provides a strong class-library (originally built in C) for SNES-related functionally in programming.
    /// </summary>
    public static unsafe partial class LunarCompress
    {
        private const string LunarDLLNotFound = "Could not locate " + LunarCompress.LunarDLLPath + "\nWould you like to recreate Lunar Compress.dll v1.61 from MushROMs resource?";

        private static byte[] _newData;

        /// <summary>
        /// Checks to make sure that Lunar Compress.dll exists in the specified directory.
        /// </summary>
        /// <returns>
        /// True if Lunar Compress.dll exists.
        /// </returns>
        public static bool CheckLunarDLL()
        {
            if (!File.Exists(LunarDLLPath))
            {
                if (MessageBox.Show(LunarCompress.LunarDLLNotFound, "Lunar Compress", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    try
                    {
                        File.WriteAllBytes(LunarDLLPath, Resources.Lunar_Compress);
                        return true;
                    }
                    catch (Exception ex)    //Maybe one day I'll handle exceptions better.
                    {
                        MessageBox.Show(ex.Message, "Lunar Compress");
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return true;
        }

        /// <summary>
        /// The current version of the DLL as an integer.
        /// For example, version 1.30 of the DLL would return "130" (decimal)
        /// </summary>
        public static int Version
        {
            get
            {
                return (int)LunarVersion();
            }
        }

        /// <summary>
        /// Open file for access by the DLL. If another file is already open, it
        /// will be closed.
        /// </summary>
        /// <param name="path">
        /// Name of File.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool OpenFile(string path)
        {
            return LunarOpenFile(path, (uint)FileModes.ReadOnly);
        }

        /// <summary>
        /// Open file for access by the DLL. If another file is already open, it
        /// will be closed.
        /// </summary>
        /// <param name="path">
        /// Name of File.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool OpenFile(string path, FileModes fileMode)
        {
            return LunarOpenFile(path, (uint)fileMode);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array data to use
        /// </param>
        /// <returns>
        /// Pointer to array on success, 0 on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(ref byte[] data)
        {
            fixed (byte* p = data)
                return LunarOpenRAMFile(p, (uint)FileModes.ReadOnly, (uint)data.Length);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array data to use
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// Pointer to array on success, 0 on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(ref byte[] data, FileModes fileMode)
        {
            fixed (byte* p = data)
                return LunarOpenRAMFile(p, (uint)fileMode, (uint)data.Length);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array data to use
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <returns>
        /// Pointer to array on success, 0 on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(ref byte[] data, FileModes fileMode, LockArraySizeOptions flags)
        {
            fixed (byte* p = data)
                return LunarOpenRAMFile(p, (uint)fileMode | (uint)flags, (uint)data.Length);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file.  This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array data to use
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <param name="createNewArray">
        /// If true, the DLL will create it's own array of the file.
        /// </param>
        /// <returns>
        /// Pointer to array on success, 0 on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(ref byte[] data, FileModes fileMode, LockArraySizeOptions flags, bool createNewArray)
        {
            if (createNewArray)
            {
                _newData = (byte[])data.Clone();
                fixed (byte* p = _newData)
                    return LunarOpenRAMFile(p, (uint)fileMode | (uint)flags, (uint)data.Length);
            }
            else
                fixed (byte* p = data)
                    return LunarOpenRAMFile(p, (uint)fileMode | (uint)flags, (uint)data.Length);
        }

        /// <summary>
        /// Saves the currently open byte array in RAM to a file.
        /// </summary>
        /// <param name="path">
        /// The file to write to.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool SaveRAMFile(string path)
        {
            return LunarSaveRAMFile(path) != 0;
        }

        /// <summary>
        /// Closes the file or RAM byte array currently open in the DLL.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool CloseFile()
        {
            return LunarCloseFile();
        }

        /// <summary>
        /// Gets the size of the file in bytes that is currently open in the DLL.
        /// </summary>
        /// <returns>
        /// Returns 0 on failure.
        /// </returns>
        public static int GetFileSize()
        {
            return LunarGetFileSize();
        }

        /// <summary>
        /// Reads all data from the currently open file into a byte array.
        /// </summary>
        /// <returns>
        /// Null if failed.
        /// </returns>
        public static byte[] ReadFile()
        {
            int s = LunarGetFileSize();
            byte[] d = new byte[s];
            bool b;
            fixed (byte* p = d)
                b = LunarReadFile(p, (uint)s, 0, 0) != 0;
            return b ? d : null;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="offset">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// Null if failed.
        /// </returns>
        public static byte[] ReadFile(int size, int offset)
        {
            byte[] d = new byte[size];
            bool b;
            fixed (byte* p = d)
                b = LunarReadFile(p, (uint)size, (uint)offset, 0) != 0;
            return b ? d : null;
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <returns>
        /// True if success, false if failed.
        /// </returns>
        public static bool WriteFile(ref byte[] data)
        {
            fixed (byte* p = data)
                return LunarWriteFile(p, (uint)data.Length, 0, 0) != 0;
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="offset">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// True if success, false if failed.
        /// </returns>
        public static bool WriteFile(ref byte[] data, int size, int offset)
        {
            fixed (byte* p = data)
                return LunarWriteFile(p, (uint)size, (uint)offset, 0) != 0;
        }

        /// <summary>
        /// Changes the bytes used for scanning for free space and erasing data.
        /// </summary>
        /// <param name="val1">
        /// First free space byte.
        /// </param>
        /// <param name="val2">
        /// Second free space byte.
        /// </param>
        /// <param name="erase">
        /// Byte for erasing (good idea to make same as val1 or val2).
        /// </param>
        /// <returns>
        /// True if success, false if failed.
        /// </returns>
        public static bool SetFreeBytes(byte val1, byte val2, byte erase)
        {
            return LunarSetFreeBytes((uint)(val1 | (val2 << 8) | (erase << 0x10))) != 0;
        }

        /// <summary>
        /// Converts a SNES ROM Address to a PC file offset.
        /// </summary>
        /// <param name="value">
        /// SNES address to convert.
        /// </param>
        /// <param name="addressFormat">
        /// The ROM type.
        /// </param>
        /// <param name="header">
        /// True if the ROM has a 0x200 byte copier header.
        /// </param>
        /// <returns>
        /// The PC file offset of the SNES ROM address.
        /// </returns>
        public static int SNEStoPC(int value, AddressFormats addressFormat, bool header)
        {
            return LunarSNEStoPC((uint)value, (uint)addressFormat, (uint)(header ? 1 : 0));
        }

        /// <summary>
        /// Converts PC file offset to SNES ROM address.
        /// </summary>
        /// <param name="value">
        /// PC file offset to convert
        /// </param>
        /// <param name="addressFormat">
        /// The ROM type.
        /// </param>
        /// <param name="header">
        /// True if the ROM has a 0x200 byte copier header.
        /// </param>
        /// <returns>
        /// The SNES ROM address of the PC file offset.
        /// </returns>
        public static int PCtoSNES(int value, AddressFormats addressFormat, bool header)
        {
            return LunarPCtoSNES((uint)value, (uint)addressFormat, (uint)(header ? 1 : 0));
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <returns>
        /// A byte array of the decomressed data.
        /// </returns>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize)
        {
            return Decompress(compressionFormat, maxSize, 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// A byte array of the decomressed data.
        /// </returns>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset)
        {
            return Decompress(compressionFormat, maxSize, offset, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tiles desired. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// A byte array of the decomressed data.
        /// </returns>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions)
        {
            byte[] data = new byte[maxSize];
            int l = maxSize;
            fixed (byte* p = data)
                l = LunarDecompress(p, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, null);
            if (l < maxSize)
                Array.Resize(ref data, l);
            return data;
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tiles desired. For all other instances, the value should be 0.
        /// </param>
        /// <param name="compressLength">
        /// Returns the length of the compressed data.
        /// </param>
        /// <returns>
        /// A byte array of the decompressed data.
        /// </returns>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions, ref int compressLength)
        {
            byte[] data = new byte[maxSize];
            int l = maxSize;
            int* last = stackalloc int[1];
            fixed (byte* p = data)
                l = LunarDecompress(p, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, (uint*)last);
            if (l < maxSize)
                Array.Resize(ref data, l);
            compressLength = last[0] - offset;
            return data;
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Destination pointer for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize)
        {
            return LunarDecompress(destination, 0, (uint)maxSize, (uint)compressionFormat, 0, null);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Destination pointer for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize, int offset)
        {
            return LunarDecompress(destination, (uint)offset, (uint)maxSize, (uint)compressionFormat, 0, null);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Destination pointer for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tiles desired. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions)
        {
            return LunarDecompress(destination, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, null);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Destination pointer for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tiles desired. For all other instances, the value should be 0.
        /// </param>
        /// <param name="compressLength">
        /// Returns the length of the compressed data.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions, ref int compressLength)
        {
            int* last = stackalloc int[1];
            int l = LunarDecompress(destination, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, null);
            compressLength = last[0] - offset;
            return l;
        }

        /// <summary>
        /// Compresses data from a byte array.. 
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maximum number of bytes the compressed data can be.
        /// </param>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data.
        /// </returns>
        public static byte[] Recompress(CompressionFormats compressionFormat, int maxSize, ref byte[] data)
        {
            return Recompress(compressionFormat, maxSize, ref data, 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.. 
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maximum number of bytes the compressed data can be.
        /// </param>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="offset">
        /// Starting offset to read to data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data.
        /// </returns>
        public static byte[] Recompress(CompressionFormats compressionFormat, int maxSize, ref byte[] data, int offset, int dataSize)
        {
            return Recompress(compressionFormat, maxSize, ref data, offset, dataSize, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.. 
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maximum number of bytes the compressed data can be.
        /// </param>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="offset">
        /// Starting offset to read to data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 compression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tiles desired. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data.
        /// </returns>
        public static byte[] Recompress(CompressionFormats compressionFormat, int maxSize, ref byte[] data, int offset, int dataSize, int otherOptions)
        {
            byte[] recomp = new byte[maxSize];
            int l = maxSize;
            fixed (byte* src = &data[offset])
            fixed (byte* dest = recomp)
                l = LunarRecompress(src, dest, (uint)dataSize, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions);
            if (l < maxSize)
                Array.Resize(ref recomp, l);
            return recomp;
        }

        /// <summary>
        /// Compresses data from a byte array and places it into another array. 
        /// </summary>
        /// <param name="source">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="destination">
        /// Destination byte array for compressed data.
        /// </param>
        /// <param name="dataSize">
        /// Size of the data to compress in bytes.
        /// </param>
        /// <param name="maxSize">
        /// Maximum number of bytes the compressed data can be.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <returns>
        /// The size of the compressed data.  A value of zero indicates failure.
        /// </returns>
        public static int Recompress(void* source, void* destination, int dataSize, int maxSize, CompressionFormats compressionFormat)
        {
            return Recompress(source, destination, dataSize, maxSize, compressionFormat, 0);
        }

        /// <summary>
        /// Compresses data from a byte array and places it into another array. 
        /// </summary>
        /// <param name="source">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="destination">
        /// Destination byte array for compressed data.
        /// </param>
        /// <param name="dataSize">
        /// Size of the data to compress in bytes.
        /// </param>
        /// <param name="maxSize">
        /// Maximum number of bytes the compressed data can be.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 compression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tiles desired. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data.  A value of zero indicates failure.
        /// </returns>
        public static int Recompress(void* source, void* destination, int dataSize, int maxSize, CompressionFormats compressionFormat, int otherOptions)
        {
            return LunarRecompress(source, destination, (uint)dataSize, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions);
        }

        /// <summary>
        /// Erases an area in a file/ROM by overwriting it with 0's. The 0 byte used 
        /// to erase the area can be changed with the <see cref="SetFreeBytes"/>() function.
        /// </summary>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="size">
        /// Number of bytes to zero out.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// This function will not prevent you from erasing past the end of the file,
        /// which will expand the file size.
        /// </remarks>
        public static bool EraseArea(int offset, int size)
        {
            return LunarEraseArea((uint)offset, (uint)size);
        }

        /// <summary>
        /// Expands a SNES ROM by appending 0's to the end of the file, and fixes the
        /// size byte (if possible). The 0 byte used for expansion can be changed with
        /// the <see cref="SetFreeBytes"/>() function.
        /// </summary>
        /// <param name="mBits">
        /// The size (in Mbits) of the ROM.
        /// </param>
        /// <returns>
        /// The expanded size of the ROM. The ROM is bigger than the size passed, returns the ROMs original size.
        /// Returns 0 on fail.
        /// </returns>
        public static int ExpandROM(int mBits)
        {
            return LunarExpandROM((uint)mBits);
        }

        /// <summary>
        /// Expands a SNES ROM by appending 0's to the end of the file, and fixes the
        /// size byte (if possible). The 0 byte used for expansion can be changed with
        /// the <see cref="SetFreeBytes"/>() function.
        /// </summary>
        /// <param name="mBits">
        /// The size (in Mbits) of the ROM.
        /// </param>
        /// <returns>
        /// The expanded size of the ROM. The ROM is bigger than the size passed, returns the ROMs original size.
        /// Returns 0 on fail.
        /// </returns>
        /// <remarks>
        /// you can pass a typecast value to size.
        /// </remarks>
        public static int ExpandROM(ExROMExpansionModes mBits)
        {
            return LunarExpandROM((uint)mBits);
        }

        /// <summary>
        /// Verifies free space in the ROM available for inserting data (free space is
        /// defined as an area filled with 0's). The 0 byte used to check 
        /// for free space can be changed with the <see cref="SetFreeBytes"/>() function.
        /// </summary>
        /// <param name="addressStart">
        /// File offset to start searching for space.
        /// </param>
        /// <param name="addressEnd">
        /// File offset to start searching
        /// </param>
        /// <param name="size">
        /// Amount of free space to find, in bytes.
        /// </param>
        /// <param name="bankType">
        /// Ignore results the cross the specified bank boundaries.
        /// </param>
        /// <returns>
        /// Returns the file offset of the first valid location in the specified range
        /// that matches the size of the free space requested.  A value of zero indicates
        /// failure.
        /// </returns>
        public static int VerifyFreeSpace(int addressStart, int addressEnd, int size, BankTypes bankType)
        {
            return LunarVerifyFreeSpace((uint)addressStart, (uint)addressEnd, (uint)size, (uint)bankType);
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the LunarOpenRAMFile() function.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files.
        /// </remarks>
        public static bool IPSCreate()
        {
            return LunarIPSCreate(null, null, null, null, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the LunarOpenRAMFile() function.
        /// </summary>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool IPSCreate(string romPath1, string romPath2)
        {
            return LunarIPSCreate(null, null, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the LunarOpenRAMFile() function.
        /// </summary>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool IPSCreate(string romPath1, string romPath2, string ipsPath)
        {
            return LunarIPSCreate(null, ipsPath, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the LunarOpenRAMFile() function.
        /// </summary>
        /// <param name="hwnd">
        /// Handle to the window. This is used only to prevent user input to the window.
        /// </param>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool IPSCreate(IntPtr hwnd, string romPath1, string romPath2, string ipsPath)
        {
            return LunarIPSCreate(hwnd.ToPointer(), ipsPath, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the OpenRAMFile() function.
        /// </summary>
        /// <param name="hwnd">
        /// Handle to the window. This is used only to prevent user input to the window.
        /// </param>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool IPSCreate(IntPtr hwnd, string romPath1, string romPath2, string ipsPath, bool quiet)
        {
            return LunarIPSCreate(hwnd.ToPointer(), ipsPath, romPath1, romPath2, (uint)(quiet ? 0x40000000 : 0)) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the OpenRAMFile() function.
        /// </summary>
        /// <param name="hwnd">
        /// Handle to the window. This is used only to prevent user input to the window.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <param name="log">
        /// Create a log file of the patch.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool IPSApply(IntPtr hwnd, string ipsPath, string romPath, bool quiet, bool log)
        {
            return LunarIPSApply(hwnd.ToPointer(), ipsPath, romPath, (uint)((quiet ? 0x40000000 : 0) | (log ? -1 : 0))) != 0;
        }

        /// <summary>
        /// Creates an indexed pixel map of a formatted GFX array.
        /// </summary>
        /// <param name="source">
        /// Byte array of SNES source graphics.
        /// </param>
        /// <param name="offset">
        /// Starting offset to begin reading at.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// A byte array of the pixel map.
        /// </returns>
        public static byte[] CreatePixelMap(ref byte[] source, int offset, int numTiles, GraphicsFormats gfxType)
        {
            byte[] data = new byte[numTiles << 6];
            fixed (byte* src = &data[offset])
            fixed (byte* dest = data)
            {
                if (LunarCreatePixelMap(src, dest, (uint)numTiles, (uint)gfxType))
                    return data;
                else
                    return new byte[0];
            }
        }

        /// <summary>
        /// Creates an indexed pixel map of a formatted GFX array.
        /// </summary>
        /// <param name="source">
        /// Byte array of the SNES source graphics.
        /// </param>
        /// <param name="destination">
        /// Byte array of destination graphics.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert.
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool CreatePixelMap(void* source, void* destination, int numTiles, GraphicsFormats gfxType)
        {
            return LunarCreatePixelMap(source, destination, (uint)numTiles, (uint)gfxType);
        }

        /// <summary>
        /// Converts an indexed pixel map into standard 8x8 SNES BPP tiles.
        /// </summary>
        /// <param name="source">
        /// Byte array of source graphics.
        /// </param>
        /// <param name="offset">
        /// Starting offset to begin reading at.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert.
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// A byte array of the 8x8 SNES BPP tiles
        /// </returns>
        public static byte[] CreateBPPMap(ref byte[] source, int offset, int numTiles, GraphicsFormats gfxType)
        {
            byte[] data = new byte[(numTiles * ((int)gfxType & 0x0F)) << 3];
            fixed (byte* src = source)
            fixed (byte* dest = data)
            {
                if (LunarCreateBppMap(src, dest, (uint)numTiles, (uint)gfxType))
                    return data;
                else
                    return new byte[0];
            }
        }

        /// <summary>
        /// Converts an indexed pixel map into standard 8x8 SNES BPP tiles.
        /// </summary>
        /// <param name="source">
        /// Byte array of source graphics.
        /// </param>
        /// <param name="destination">
        /// Byte array of SNES destination graphics.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// True on sucess, false on fail.
        /// </returns>
        public static bool CreateBPPMap(void* source, void* destination, int numTiles, GraphicsFormats gfxType)
        {
            return LunarCreateBppMap((byte*)source, (byte*)destination, (uint)numTiles, (uint)gfxType);
        }

        /// <summary>
        /// Converts a standard SNES 15-bit color into a PC 24-bit color.
        /// </summary>
        /// <param name="snesColor">
        /// SNES RGB value. (?bbbbbgg gggrrrrr)
        /// </param>
        /// <returns>
        /// PC color value. (00000000 rrrrr000 ggggg000 bbbbb000)
        /// </returns>
        public static uint SNEStoPCRGB(ushort snesColor)
        {
            return LunarSNEStoPCRGB((uint)snesColor);
        }

        /// <summary>
        /// Converts a standard PC 24-bit color into the nearest SNES 15-bit color, by
        /// rounding each color component to the nearest 5-bit value.
        /// </summary>
        /// <param name="pcColor">
        /// PC RGB value (???????? rrrrrrrr gggggggg bbbbbbbb).
        /// </param>
        /// <returns>
        /// SNES color value. (0bbbbbgg gggrrrrr)
        /// </returns>
        public static ushort PCtoSNESRGB(uint pcColor)
        {
            return (ushort)LunarPCtoSNESRGB(pcColor);
        }

        /// <summary>
        /// Converts a standard PC 24-bit color into the nearest SNES 15-bit color, by
        /// rounding each color component to the nearest 5-bit value.
        /// </summary>
        /// <param name="pcColor">
        /// PC RGB value (???????? rrrrrrrr gggggggg bbbbbbbb).
        /// </param>
        /// <returns>
        /// SNES color value. (0bbbbbgg gggrrrrr)
        /// </returns>
        public static ushort PCtoSNESRGB(int pcColor)
        {
            return (ushort)LunarPCtoSNESRGB((uint)pcColor);
        }

        /// <summary>
        /// Draws an SNES tile to a PC bitmap (with optional effects). Both the sprite and
        /// BG tile data types are supported.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to the BitmapData Scan0 vraibale.
        /// </param>
        /// <param name="width">
        /// Width of the bitmap you're drawing to.
        /// </param>
        /// <param name="height">
        /// Height of the bitmap you're drawing to.
        /// </param>
        /// <param name="x">
        /// X-position in the bitmap to draw the tile to.
        /// </param>
        /// <param name="y">
        /// Y-position in the bitmap to draw the tile to.
        /// </param>
        /// <param name="pixelMap">
        /// Pointer to a pixel map of SNES tiles. The arry should have at leasr 0x400 tiles (0x10000 bytes) for
        /// BG tiles, or 0x200 tiles (0x8000 bytes) for sprite tiles.
        /// </param>
        /// <param name="palette">
        /// Pointer to an array of 32-bit uints containg the PC colors to use to render the tiles. The array should
        /// contain at least 16 palettes of 16 colors each (0x400 bytes).
        /// </param>
        /// <param name="map8Tile">
        /// SNES data that defines the tile number and flags used to render the tile.
        /// </param>
        /// <param name="flags">
        /// Special flags for rendering. These flags can be combined.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool Render8x8(uint* scan0, int width, int height, int x, int y, void* pixelMap, uint* palette, ushort map8Tile, Render8x8Flags flags)
        {
            return LunarRender8x8(scan0, width, height, x, y, pixelMap, palette, map8Tile, (uint)flags);
        }

        /// <summary>
        /// Scans the ROM in a uder-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATSArea(ref byte[] data, int minRange, int maxRange)
        {
            fixed (byte* p = data)
                return LunarWriteRatArea(p, (uint)data.Length, (uint)minRange, (uint)minRange, (uint)maxRange, 0);
        }

        /// <summary>
        /// Scans the ROM in a uder-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATSArea(ref byte[] data, int minRange, int maxRange, int preferredAddress)
        {
            fixed (byte* p = data)
                return LunarWriteRatArea(p, (uint)data.Length, (uint)preferredAddress, (uint)maxRange, (uint)maxRange, 0);
        }

        /// <summary>
        /// Scans the ROM in a uder-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATSArea(ref byte[] data, int offset, int size, int minRange, int maxRange, int preferredAddress)
        {
            fixed (byte* p = &data[offset])
                return LunarWriteRatArea(p, (uint)size, (uint)preferredAddress, (uint)maxRange, (uint)maxRange, 0);
        }

        /// <summary>
        /// Scans the ROM in a uder-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality. Multiple flags can be passed with OR operations.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATSArea(ref byte[] data, int offset, int size, int minRange, int maxRange, int preferredAddress, RATSFunctionFlags flags)
        {
            fixed (byte* p = &data[offset])
                return LunarWriteRatArea(p, (uint)size, (uint)preferredAddress, (uint)maxRange, (uint)maxRange, (uint)flags);
        }

        /// <summary>
        /// Erases data at a specified location using the RATS tag to get the size, or a specified size if one is not found.
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. Note that this is the address of the data, not the RATS tag.
        /// </param>
        /// <param name="size">
        /// Default size to erase of no RATS tag is found.
        /// </param>
        /// <returns>
        /// The size of the data erased (not including the RATS tag).
        /// Returns 0 on fail.
        /// </returns>
        public static int EraseRATSArea(int address, int size)
        {
            return LunarEraseRatArea((uint)address, (uint)size, 0);
        }

        /// <summary>
        /// Erases data at a specified location using the RATS tag to get the size. If one cannot be found, you can optionally
        /// check if the data can be decompressed to get the size. If both fail, the given default size will be used.
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. Note that this is the address of the data, not the RATS tag.
        /// </param>
        /// <param name="size">
        /// Default size to erase of no RATS tag is found or data could not be decompressed correctly.
        /// </param>
        /// <param name="flags">
        /// Specify if you want to check for size by decompressing the data if no RATS tag is found. You can pass multiple flags
        /// with the OR operation.
        /// </param>
        /// <param name="compressionFormat">
        /// The compression algorithm to check for.
        /// </param>
        /// <returns>
        /// The size of the data erased (not including the RATS tag).
        /// Returns 0 on fail.
        /// </returns>
        public static int EraseRATSArea(int address, int size, RATSCompressFlags flags, CompressionFormats compressionFormat)
        {
            return LunarEraseRatArea((uint)address, (uint)size, (uint)flags | (uint)compressionFormat);
        }

        /// <summary>
        /// Determines the size of a segment of data determined by a RATS tag.
        /// </summary>
        /// <param name="address">
        /// Address of the data to find the size of. Note this is the address of the data, not the RATS tag itself.
        /// </param>
        /// <returns>
        /// The size of the data erased (not including the RATS tag).
        /// Returns 0 on fail.
        /// </returns>
        public static int GetRATSAreaSize(int address)
        {
            return LunarGetRatAreaSize((uint)address, 0);
        }

        /// <summary>
        /// Determines the size of a segment of data determined by a RATS tag. If the RATS tag does not exist, you can optionally look for 
        /// the size by decompressing the data.
        /// </summary>
        /// <param name="address">
        /// Address of the data to find the size of. Note this is the address of the data, not the RATS tag itself.
        /// </param>
        /// <param name="flags">
        /// Specify if you want to check for size by decompressing the data if no RATS tag is found. You can pass multiple flags
        /// with the OR operation.
        /// </param>
        /// <param name="compressionFormat">
        /// The compression algorithm to check for.
        /// </param>
        /// <returns>
        /// The size of the data erased (not including the RATS tag).
        /// Returns 0 on fail.
        /// </returns>
        public static int GetRATSAreaSize(int address, RATSCompressFlags flags, CompressionFormats compressionFormat)
        {
            return LunarGetRatAreaSize((uint)address, (uint)flags | (uint)compressionFormat);
        }
    }
}