
using CoreDisk.Models;
using CoreDisk.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Parsers {
    public class BootSectorParser {

        private VolumeReader _volumeReader { get; set; }
       

        public BootSectorParser(VolumeReader volumeReader) {
            _volumeReader = volumeReader;
        }

        public BootSector Parse() {

            var data = new byte[512];

            _volumeReader.GetBootSectorData(data);

            var bsm = new BootSector();




            return bsm;
        }

    }
}
