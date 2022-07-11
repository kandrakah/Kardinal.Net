namespace Kardinal.Net.IoT.Display
{
    public sealed class ComScanDirCommand : DisplayCommand
    {
        public ComScanDirCommand() : base(new byte[] { 0xC8 })
        {

        }
    }
}
