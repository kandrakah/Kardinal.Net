namespace Kardinal.Net.IoT.Display
{
    internal class CharacterData
    {
        internal char Character { get; }
        internal uint WidthPixels { get; }
        internal uint HeightBytes { get; }
        internal byte[] Data { get; }

        internal CharacterData(char character, uint heightBytes, byte[] data)
        {
            this.Character = character;
            this.WidthPixels = (uint)data.Length / heightBytes;
            this.HeightBytes = heightBytes;
            this.Data = data;
        }

        public override string ToString()
        {
            return $"{this.Character}";
        }
    }
}
