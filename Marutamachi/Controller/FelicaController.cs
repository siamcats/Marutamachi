using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.SmartCards;
using Felica;

namespace Marutamachi.Controller
{
    class FelicaController
    {        
        AccessHandler _handler;

        public FelicaController(SmartCardConnection con)
        {
            _handler = new Felica.AccessHandler(con);
        }

        public async Task<String> GetIdm()
        {
            var result = await _handler.TransparentExchangeAsync(new byte[] { 6, 0, 0xff, 0xff, 0, 3 });
            Debug.WriteLine(result.ToString());

            /*
            byte systemCodeHigher = (byte)(systemCode >> 8);
            byte systemCodeLower = (byte)(systemCode & 0x00ff);

            byte[] commandData = new byte[] {
            0x00, 0x00, systemCodeHigher, systemCodeLower, 0x01, 0x0f,
            };
            commandData[0] = (byte)commandData.Length;

            byte[] result = await felicaAccess.TransparentExchangeAsync(commandData);

            byte[] idm = new byte[8];
            Array.Copy(result, 2, idm, 0, idm.Length);

            return idm;
            */
            return null;
        }
    }
}
