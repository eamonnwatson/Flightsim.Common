using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace FlightSimulator.Common
{
    class Class1
    {
        public void Test1()
        {
            var test = new OffsetDictionary();

            test.AddOffset<int>("TIME", "main", 0x2345);

        }

    }
}
