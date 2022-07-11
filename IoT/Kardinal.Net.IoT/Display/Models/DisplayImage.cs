namespace Kardinal.Net.IoT.Display
{
    public class DisplayImage
    {
        public uint ImageWidthPx { get; }
        public uint ImageHeightBytes { get; }
        public byte[] ImageData { get; }

        public DisplayImage(uint heightBytes, byte[] data)
        {
            ImageWidthPx = (uint)data.Length / heightBytes;
            ImageHeightBytes = heightBytes;
            ImageData = data;
        }
    }
}
