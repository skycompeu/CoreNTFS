using CoreLib.WinNative;
using CoreDisk.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDisk.Enums;
using CoreLib.Extensions;
using CoreDisk.Extensions;


namespace CoreDisk {
    public class CoreDisk {
      
        public CoreDisk() {
           
        }

        public List<PhysicalDrive> GetPhysicalDrives() {
            var physicalDriveList = new List<PhysicalDrive>();
            var totalDevices = QueryDosDevice().ToList();
            var physicalDrives = GetPhysicalDrives(totalDevices);

            foreach (var physicalDrive in physicalDrives) {
                PhysicalDrive pd = new PhysicalDrive();

                SafeFileHandle fHandle = null;
                try {

                    string volume = string.Format(@"\\.\{0}", physicalDrive.ToUpper());
                    pd.DeviceID = volume;

                    fHandle = CreateSafeFileHandle(volume);

                    IntPtr lpInBuffer = IntPtr.Zero;
                    uint nInBufferSize = 0;
                    IntPtr lpOutBuffer = IntPtr.Zero;  //DISK_GEOMETRY_EX
                    uint nOutBufferSize = 0;
                    uint lpBytesReturned = 0;
                    IntPtr lpOverlapped = IntPtr.Zero;

                    lpOutBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Kernel32.DISK_GEOMETRY_EX)));
                    nOutBufferSize = (uint)Marshal.SizeOf(typeof(Kernel32.DISK_GEOMETRY_EX));

                    var state = Kernel32.DeviceIoControl(fHandle,
                        Kernel32.IOCTL_DISK_GET_DRIVE_GEOMETRY_EX,
                        lpInBuffer,
                        nInBufferSize,
                        lpOutBuffer,
                        nOutBufferSize,
                        ref lpBytesReturned,
                        lpOverlapped
                        );


                    if (state == true) {
                        var discGeometry = (Kernel32.DISK_GEOMETRY_EX)Marshal.PtrToStructure(lpOutBuffer, typeof(Kernel32.DISK_GEOMETRY_EX));

                        pd.Data = discGeometry.Data;
                        pd.DiskSize = discGeometry.DiskSize;
                        pd.DiskGeometry = new DiskGeometry();
                        pd.DiskGeometry.BytesPerSector = discGeometry.Geometry.BytesPerSector;
                        pd.DiskGeometry.Cylinders = discGeometry.Geometry.Cylinders;
                        pd.DiskGeometry.MediaType = discGeometry.Geometry.MediaType;
                        pd.DiskGeometry.SectorsPerTrack = discGeometry.Geometry.SectorsPerTrack;
                        pd.DiskGeometry.TracksPerCylinder = discGeometry.Geometry.TracksPerCylinder;

                        uint dlBufferSize = 1024;
                        int error = 0;
                        IntPtr dlPtr = IntPtr.Zero;
                        do {
                            dlPtr = Marshal.AllocHGlobal(Convert.ToInt32(dlBufferSize));
                            state = Kernel32.DeviceIoControl(fHandle, Kernel32.IOCTL_DISK_GET_DRIVE_LAYOUT_EX, IntPtr.Zero,
                                0, dlPtr, dlBufferSize, ref lpBytesReturned, IntPtr.Zero);

                            if (state) {
                                var driveLayout = (Kernel32.DRIVE_LAYOUT_INFORMATION_EX)Marshal.PtrToStructure(dlPtr, typeof(Kernel32.DRIVE_LAYOUT_INFORMATION_EX));

                            } else {
                                error = Marshal.GetLastWin32Error();
                                dlBufferSize *= 2;
                            }

                        } while (error == Kernel32.ERROR_INSUFFICIENT_BUFFER);
                    }

                    physicalDriveList.Add(pd);

                } finally {
                    if (fHandle != null) {
                        fHandle.Dispose();
                    }
                }
            }
            return physicalDriveList;
        }

        public List<string> GetDiskTypes() {

            var diskEnums = Enum.GetValues(typeof(DiskTypeEnum));

            



            //DiskTypeEnum.GetEnumValuesList()

            /*foreach (DiskTypeEnum de in diskEnums) {

                var desc = de.GetDescription("pl");

            }*/


            return default;
        }

        private SafeFileHandle CreateSafeFileHandle(string name) {

            SafeFileHandle fHandle = null;

            fHandle = Kernel32.CreateFile(name,
                        Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                        Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                        IntPtr.Zero,
                        Kernel32.OPEN_EXISTING,
                        0,
                        IntPtr.Zero);

            if (fHandle.IsInvalid) {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return fHandle;
        }



        private List<string> GetPhysicalDrives(List<string> totalDevices) {
            var pattern = new Regex(@"PhysicalDrive\d+", RegexOptions.IgnoreCase);
            return totalDevices.Where(w => pattern.Match(w).Success == true).ToList();
        }

        private string[] QueryDosDevice() {
            uint returnSize = 0;
            int maxSize = 100;
            string allDevices = null;
            IntPtr mem;
            string[] retval = null;

            while (returnSize == 0) {
                mem = Marshal.AllocHGlobal(maxSize);
                if (mem != IntPtr.Zero) {
                    try {
                        returnSize = Kernel32.QueryDosDevice(null, mem, Convert.ToUInt32(maxSize));
                        if (returnSize != 0) {
                            allDevices = Marshal.PtrToStringAnsi(mem, Convert.ToInt32(returnSize));
                            retval = allDevices.Split('\0');
                            break;
                        } else if (Marshal.GetLastWin32Error() == Kernel32.ERROR_INSUFFICIENT_BUFFER) {
                            maxSize *= 10;
                        } else {
                            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
                        }
                    } finally {
                        Marshal.FreeHGlobal(mem);
                    }
                } else {
                    throw new OutOfMemoryException();
                }
            }
            return retval;
        }
    }
}
