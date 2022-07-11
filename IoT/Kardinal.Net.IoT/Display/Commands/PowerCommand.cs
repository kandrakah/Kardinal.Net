namespace Kardinal.Net.IoT.Display
{
    public sealed class PowerCommand : DisplayCommand
    {
        public PowerCommand(bool on) : base(on ? new byte[] { 0xAF } : new byte[] { 0xAE })
        {
        }
    }
}
