using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Structures {

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NtfsBootSector {
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public readonly byte[] JMPInstruction;

        public readonly ulong OEMID;

        public readonly ushort BytesPerSector;

        public readonly byte SectorsPerCluster;

        public readonly ushort ReservedSectors;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public readonly byte[] AlwaysZero1;

        public readonly ushort NotUsed1;

        public readonly byte MediaDescriptor;

        public readonly ushort AlwaysZero2;

        public readonly ushort SectorsPerTrack;

        public readonly ushort NumberOfHeads;

        public readonly uint HiddenSectors;

        public readonly uint NotUsed2;

        public readonly uint NotUsed3;

        public readonly ulong TotalSectors;

        public readonly ulong MFTLCN;

        public readonly ulong MFTMirrLCN;

        public readonly byte ClustersPerMFTRecord;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public readonly byte[] NotUsed4;

        public readonly uint ClustersPerIndexBuffer;

        public readonly ulong VolumeSerialNumber;

        public readonly uint NTFSChecksum;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 426)]
        public readonly byte[] BootStrapCode;


        public readonly ushort Signature;

    }
}
