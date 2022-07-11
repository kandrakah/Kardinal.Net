using Iot.Device.Ssd13xx;
using Iot.Device.Ssd13xx.Commands;

namespace Kardinal.Net.IoT
{
    /// <summary>
    /// 
    /// </summary>
    public class EphemeralSsd1306: Ssd1306
    {
        /// <summary>
        /// 
        /// </summary>
        public EphemeralSsd1306() : base(new EphemeralI2cDevice())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public new void SendCommand(ISsd1306Command command)
        {
            
        }
    }
}
