using Pcsc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        private static async void DeviceSetAsync()
        {
            // Reader検索
            var selector = SmartCardReader.GetDeviceSelector(SmartCardReaderKind.Any);
            var devices = await DeviceInformation.FindAllAsync(selector);
            var device = devices.FirstOrDefault();
            if (device == null)
            {
                return;
            }
            Console.WriteLine("Device Set");

            Reader = await SmartCardReader.FromIdAsync(device.Id);
            if (Reader == null)
            {
                return;
            }
            Console.WriteLine("Reader Set");

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
            Console.WriteLine("Card OK");

            // カードタイプ判別
            using (var con = await card.ConnectAsync())
            {
                IccDetection detection = new IccDetection(card, con);
                await detection.DetectCardTypeAync();

                Console.WriteLine(" CardName : " + detection.PcscCardName.ToString());
                Console.WriteLine(" DeviceClass : " + detection.PcscDeviceClass.ToString());

                if (detection.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                    && detection.PcscCardName == Pcsc.CardName.FeliCa) { }
            }
        }
    }
}
