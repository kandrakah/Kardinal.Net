namespace Kardinal.Net.IoT.Display
{
    public sealed class ChangePumpCommand : DisplayCommand
    {
        public ChangePumpCommand(bool on) : base(on ? new byte[] { 0x8D, 0x14 } : new byte[] { 0x8D, 0x14 })
        {

        }
    }
}
