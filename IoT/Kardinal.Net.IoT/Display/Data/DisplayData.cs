using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kardinal.Net.IoT.Display
{
    public class DisplayData : IData
    {
        public byte Id { get; }

        private byte[] Data { get; }

        public DisplayData(byte[] data) : this(0x40, data)
        {

        }

        public DisplayData(byte id, byte[] data)
        {
            this.Id = id;
            this.Data = data;
        }

        public byte[] GetBytes()
        {
            var data = new byte[this.Data.Length + 1];
            this.Data.CopyTo(data, 1);
            data[0] = 0x40;
            return data;
        }
    }
}
