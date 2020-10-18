using CoreLib.WinNative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Models {
    public class DiskGeometry {

        /// <summary>
        /// Liczba bajtów w sektorze.
        /// </summary>
        public uint BytesPerSector { get; internal set; }

        /// <summary>
        /// Liczba cylindrów
        /// </summary>
        public long Cylinders { get; internal set; }

        /// <summary>
        /// https://www.pinvoke.net/default.aspx/Enums/MEDIA_TYPE.html
        /// http://www.cs.rpi.edu/academics/courses/fall09/os/win32/MEDIA_TYPE.html
        /// </summary>
        public Kernel32.MEDIA_TYPE MediaType { get; internal set; }
        public uint SectorsPerTrack { get; internal set; }
        public uint TracksPerCylinder { get; internal set; }
    }
}
