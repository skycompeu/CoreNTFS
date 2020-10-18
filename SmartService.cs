using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk {
    public interface ISmartService {

        void ReadSMARTValues();
    }
    public class SmartService : ISmartService {
        public void ReadSMARTValues() {
           
        }
    }
}
