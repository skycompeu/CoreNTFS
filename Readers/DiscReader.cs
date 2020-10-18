using CoreDisk.Models;
using CoreLib.WinNative;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Readers {


    //https://www.little-apps.com/2017/04/boot-sector-marshalling-and-pinvoke/

    public class DiscReaderV2 : Stream {

        public PhysicalDrive PhysicalDrive { get; private set; }

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DiscReaderV2(PhysicalDrive drive) {
            PhysicalDrive = drive;
        }

        public override void Flush() {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }
    }






    public class DiscReader : IDisposable {


        FileStream _diskStream;

        public SafeFileHandle FileHandle { get; private set; }
        public PhysicalDrive PhysicalDrive { get; private set; }

        public DiscReader(PhysicalDrive physicalDrive) {
            PhysicalDrive = physicalDrive;
            FileHandle = Open(PhysicalDrive);
            _diskStream = new FileStream(FileHandle, FileAccess.Read);

            var CanRead = _diskStream.CanRead;
            var CanWrite = _diskStream.CanWrite;


        }


        public byte[] Read(int offset, int count) {
            byte[] array = new byte[count];
            _diskStream.Read(array, offset, count);
            return array;
        }

        public int Read(byte[] array, int offset, int count) {
            int abc = 12;

            return _diskStream.Read(array, offset, count);
        }


        private FileStream CreateStream(SafeFileHandle handle, FileAccess access) {
            return new FileStream(handle, access);
        }

        private SafeFileHandle Open(PhysicalDrive physicalDrive) {
            FileHandle = Kernel32.CreateFile(physicalDrive.DeviceID,
                   Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                   Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                   IntPtr.Zero,
                   Kernel32.OPEN_EXISTING,
                   0,
                   IntPtr.Zero);

            if (FileHandle == null || FileHandle.IsInvalid) {
                throw new Exception($"Unable to open volume '{physicalDrive.DeviceID}'. Run Program as administrator!");
            }

            return FileHandle;
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
                FileHandle.Dispose();
            }

            disposed = true;
        }
        #endregion


    }



   /* public class DiscReader : IDisposable {


        FileStream _diskStream;

        public SafeFileHandle FileHandle { get; private set; }
        public PhysicalDrive PhysicalDrive { get; private set; }

        public DiscReader(PhysicalDrive physicalDrive) {
            PhysicalDrive = physicalDrive;
            FileHandle = Open(PhysicalDrive);
            _diskStream = new FileStream(FileHandle, FileAccess.Read);

            var CanRead = _diskStream.CanRead;
            var CanWrite = _diskStream.CanWrite;


        }


        public byte[] Read(int offset, int count) {
            byte[] array = new byte[count];
            _diskStream.Read(array, offset, count);
            return array;
        }

        public int Read(byte[] array, int offset, int count) {
            int abc = 12;

            return _diskStream.Read(array, offset, count);
        }


        private FileStream CreateStream(SafeFileHandle handle, FileAccess access) {
            return new FileStream(handle, access);
        }

        private SafeFileHandle Open(PhysicalDrive physicalDrive) {
            FileHandle = Kernel32.CreateFile(physicalDrive.DeviceID,
                   Kernel32.GENERIC_READ | Kernel32.GENERIC_WRITE,
                   Kernel32.FILE_SHARE_READ | Kernel32.FILE_SHARE_WRITE,
                   IntPtr.Zero,
                   Kernel32.OPEN_EXISTING,
                   0,
                   IntPtr.Zero);

            if (FileHandle == null || FileHandle.IsInvalid) {
                throw new Exception($"Unable to open volume '{physicalDrive.DeviceID}'. Run Program as administrator!");
            }

            return FileHandle;
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
                FileHandle.Dispose();
            }

            disposed = true;
        }
        #endregion


    }*/
}
