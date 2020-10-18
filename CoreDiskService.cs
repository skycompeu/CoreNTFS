using CoreDisk.Enums;
using CoreDisk.Models;
using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDisk {

    public interface ICoreDiskService {
        List<PhysicalDrive> GetPhysicalDrives();
        List<string> GetDiskTypes();
    }

    public class CoreDiskService : ICoreDiskService {

        private CoreDisk _coreDisk;

        public CoreDiskService() {
            _coreDisk = new CoreDisk();
        }

        public List<string> GetDiskTypes() {
            return _coreDisk.GetDiskTypes();
        }

        public List<PhysicalDrive> GetPhysicalDrives() {
            return _coreDisk.GetPhysicalDrives();
        }
    }
}
