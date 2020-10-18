using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Attributes {
    public class DescriptionEn : Attribute {

        public string Description { get; set; }

        public DescriptionEn(string description) {
            Description = description;
        }
    }
}
