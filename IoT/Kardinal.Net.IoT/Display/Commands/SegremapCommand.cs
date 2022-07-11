namespace Kardinal.Net.IoT.Display
{
    public sealed class SegremapCommand : DisplayCommand
    {
        public SegremapCommand() : base(new byte[] { 0xA1 })
        {

        }
    }
}
