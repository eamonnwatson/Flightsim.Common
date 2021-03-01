using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSUIPC;
using UnitsNet;

namespace FlightSimulator.Common
{
    class OffsetDictionary
    {
        private readonly Dictionary<string, Offset> offsets = new Dictionary<string, Offset>();

        internal void AddOffset<TOffset>(string name, string groupName, int address)
        {
            var offset = new Offset<TOffset>(groupName, address);
            offsets.Add(name, offset);
        }



    }
}
