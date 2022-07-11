using Iot.Device.Ssd13xx.Commands;

namespace Kardinal.Net.IoT.Display
{
    public class DisplayCommand : ISsd1306Command
    {
        public byte Id { get; }

        private byte[] Data { get; }

        public DisplayCommand(byte id, byte[] data)
        {
            this.Id = id;
            this.Data = data;
        }

        public DisplayCommand(byte[] data) : this(0x00, data)
        {
        }

        public byte[] GetBytes()
        {
            return this.Data;
        }

        public override string ToString()
        {
            return $"{this.Id}";
        }
    }
}
