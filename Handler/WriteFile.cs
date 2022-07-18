using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOET
{
    static class SaveFile
    {
        public static void WriteLine(List<string> lines)
        {
            File.WriteAllLines(@"output.txt", lines);
        }
    }
}
