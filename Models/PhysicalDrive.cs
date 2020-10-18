using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Models {
    public class  PhysicalDrive {
        /// <summary>
        /// "\\\\.\\PhysicalDrive{0}"
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// Numer dysku. Numerowane od 0.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Lista partycji znajdujących sie na dysku fizycznym 
        /// </summary>
        public List<Partition> Partitions { get; set; }

        /// <summary>
        /// Informacje o geometri dysku.
        /// </summary>
        public DiskGeometry DiskGeometry { get; set; }

        /// <summary>
        /// ??
        /// </summary>
        public byte Data { get;  set; }

        /// <summary>
        /// Rozmiar dysku w bajtach.
        /// </summary>
        public long DiskSize { get;  set; }

        public override string ToString() {
            return base.ToString();
        }
    }
}
