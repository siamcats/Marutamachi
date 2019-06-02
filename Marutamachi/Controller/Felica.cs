using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felica;

namespace Marutamachi.Controller
{
    class Felica
    {

        AccessHandler _handler;

        Felica(AccessHandler handler)
        {
            _handler = handler;
        }

        /*
        public String GetIdm()
        {
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
        }
        */
    }
}
