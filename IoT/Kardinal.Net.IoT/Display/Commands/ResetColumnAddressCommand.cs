namespace Kardinal.Net.IoT.Display
{
    public sealed class ResetColumnAddressCommand : DisplayCommand
    {
        public ResetColumnAddressCommand() : base(new byte[] { 0x21, 0x00, 0x7F })
        {
        }
    }
}
