using CoreDisk.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoreDisk.Parsers {
    public class MFTParser {

        private VolumeReader _volumeReader { get; set; }
        


        public MFTParser(VolumeReader volumeReader) {
            _volumeReader = volumeReader;
           
        }

    



    }
}
