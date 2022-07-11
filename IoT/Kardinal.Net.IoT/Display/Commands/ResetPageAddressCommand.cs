namespace Kardinal.Net.IoT.Display
{
    public sealed class ResetPageAddressCommand : DisplayCommand
    {
        public ResetPageAddressCommand() : base(new byte[] { 0x22, 0x00, 0x07 })
        {

        }
    }
}
