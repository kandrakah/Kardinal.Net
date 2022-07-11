namespace Kardinal.Net.IoT.Display
{
    public interface IData
    {
        byte Id { get; }

        byte[] GetBytes();
    }
}
