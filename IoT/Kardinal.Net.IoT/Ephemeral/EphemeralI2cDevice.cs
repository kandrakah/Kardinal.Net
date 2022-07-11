using System.Device.I2c;

namespace Kardinal.Net.IoT
{
    /// <summary>
    /// 
    /// </summary>
    public class EphemeralI2cDevice : I2cDevice
    {
        /// <summary>
        /// 
        /// </summary>
        public override I2cConnectionSettings ConnectionSettings { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        public EphemeralI2cDevice()
        {
            this.ConnectionSettings = new I2cConnectionSettings(1, 0x00);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static I2cDevice Create()
        {
            return new EphemeralI2cDevice();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public override void Read(Span<byte> buffer)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override byte ReadByte()
        {
            return new byte();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public override void Write(ReadOnlySpan<byte> buffer)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteByte(byte value)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeBuffer"></param>
        /// <param name="readBuffer"></param>
        public override void WriteRead(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer)
        {
        }
    }
}
