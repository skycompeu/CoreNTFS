using CoreDisk.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Enums {
    public enum DiskTypeEnum {

        [DescriptionPl("Fizyczny dysk")]
        [DescriptionEn("Physical drive")]
        Physical = 0,

        [DescriptionPl("Dysk logiczny")]
        [DescriptionEn("Logical drive")]
        Logical = 1
    }
}