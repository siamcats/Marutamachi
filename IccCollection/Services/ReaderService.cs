using Pcsc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;

namespace IccCollection.Services
{
    public static class ReaderService
    {
        public static SmartCardReader Reader;

        //public static bool Status
        //{
        //    get
        //    {
        //        var scStatus = await Reader.GetStatusAsync();
        //        if(scStatus == SmartCardReaderStatus.Ready) return false;
        //    }
        //}


        public static async void DeviceSetAsync()
        {
            // Reader検索
            var selector = SmartCardReader.GetDeviceSelector(SmartCardReaderKind.Any);
            var devices = await DeviceInformation.FindAllAsync(selector);
            var device = devices.FirstOrDefault();
            if (device == null)
            {
                return;
            }
            Debug.WriteLine("Device Set");

            Reader = await SmartCardReader.FromIdAsync(device.Id);
            if (Reader == null)
            {
                return;
            }
            Debug.WriteLine("Reader Set");

            Reader.CardAdded += OnCardAdded;

            return;
        }

        public static async void OnCardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            // カード検索
            var card = args.SmartCard;
            if (card == null)
            {
                return;
            }
            Debug.WriteLine("Card OK");

            // カードタイプ判別
            using (var connection = await card.ConnectAsync())
            {
                IccDetection detection = new IccDetection(card, connection);
                await detection.DetectCardTypeAync();
                Debug.WriteLine("CardName : " + detection.PcscCardName.ToString());

                if (detection.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass && detection.PcscCardName == Pcsc.CardName.FeliCa)
                {
                    var felicaAccess = new Felica.AccessHandler(connection);

                    byte[] commandPacket = new byte[] { 0x00, 0x00, 0xff, 0xff, 0x01, 0x0f };
                    commandPacket[0] = (byte)commandPacket.Length;

                    byte[] result = await felicaAccess.TransparentExchangeAsync(commandPacket);

                    byte[] idm = new byte[8];
                    Array.Copy(result, 2, idm, 0, idm.Length);

                    byte[] systemCode = new byte[2];
                    Array.Copy(result, 18, systemCode, 0, systemCode.Length);

                    string strIdm = BitConverter.ToString(idm);
                    string strSystemCode = BitConverter.ToString(systemCode);

                    Debug.WriteLine("IDm        : " + strIdm);
                    Debug.WriteLine("SystemCode : " + strSystemCode);
                }
            }
        }

        /// <summary>
        /// 4.4.2 Polling
        /// コマンドパケット
        /// - コマンドコード    1byte 00h
        /// - システムコード    2byte
        /// - リクエストコード  1byte 01h:システムコード要求
        /// - タイムスロット    1byte
        /// レスポンスパケット
        /// - レスポンスコード  1byte 01h
        /// - IDm               8byte
        /// - PMm               8byte
        /// - リクエストデータ  2byte
        /// </summary>
        private static void PollingWithWildcard()
        {
            byte systemCodeHigher = 0xff;
            byte systemCodeLower = 0xff;
        }
    }
}
