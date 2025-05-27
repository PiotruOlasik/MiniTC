using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    public class DiskOperations
    {
        public List<string> GetDisks()
        {
            string[] drives = Directory.GetLogicalDrives();
            return drives.ToList();
            //return string.Join(", ", drives);
        }
    }
}
