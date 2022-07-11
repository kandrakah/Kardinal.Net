namespace Kardinal.Net.IoT.Display
{
    public sealed class MemoryAddressModeCommand : DisplayCommand
    {
        public MemoryAddressModeCommand() : base(new byte[] { 0x20, 0x00 })
        {

        }
    }
}
