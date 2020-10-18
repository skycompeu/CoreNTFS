using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.WinNative;

namespace CoreDisk.Readers {
    public class VolumeReader : IDisposable {

        SafeFileHandle _volumeHandle;

        public VolumeReader(DriveInfo drive) {
            Open(drive);
        }

        private void Open(DriveInfo driveInfo) {
            var volumeName = GetVolumeNameForVolumeMountPoint(driveInfo.RootDirectory.Name);

            _volumeHandle = Kernel32.CreateFile(volumeName,
                Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                IntPtr.Zero,
                Kernel32.OPEN_EXISTING,
                0,
                IntPtr.Zero);

            if (_volumeHandle == null || _volumeHandle.IsInvalid) {
                throw new Exception($"Unable to open volume '{volumeName}'. Run Program as administrator!");
            }
        }

        internal void GetBootSectorData(byte[] data) {
            Kernel32.NativeOverlapped nativeOverlapped;

            unsafe {
                fixed (byte* pointer = data) {
                    bool status = Kernel32.ReadFile(_volumeHandle, pointer, data.Length, IntPtr.Zero, &nativeOverlapped);
                }
            }
        }

        string GetVolumeNameForVolumeMountPoint(string rootDirectoryName) {
            StringBuilder builder = new StringBuilder(1024);
            Kernel32.GetVolumeNameForVolumeMountPoint(rootDirectoryName, builder, builder.Capacity);
            return builder.ToString().TrimEnd(new char[] { '\\' });
        }


        #region dispose      
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (disposed)
                return;

            if (disposing) {
                _volumeHandle.Dispose();
            }

            disposed = true;
        }
        #endregion
    }
}
