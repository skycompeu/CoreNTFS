using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Attributes {
    public class DescriptionPl : Attribute {
        public string Description { get; set; }

        public DescriptionPl(string description) {
            Description = description;
        }
    }
}
