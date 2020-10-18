using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk.Models {
    public class BootSector {

		private byte[] _jumpInstruction = new byte [3];
		public byte[] JumpInstruction {
			get { return _jumpInstruction; }
			set { _jumpInstruction = value; }
		}

		public string OEMName { get; set; }


	}
}
