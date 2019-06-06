using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;
using Windows.Networking.Proximity;
using Pcsc.Common;
using Marutamachi.Service;
using System.Diagnostics;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Marutamachi
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SmartCardReader _reader;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Reader検索
            var selector = SmartCardReader.GetDeviceSelector(SmartCardReaderKind.Any);
            var devices = await DeviceInformation.FindAllAsync(selector);
            var device = devices.FirstOrDefault();
            if (device == null)
            {
                return;
            }
            Log("Device Set");


            var _reader = await SmartCardReader.FromIdAsync(device.Id);
            if (_reader == null)
            {
                return;
            }
            Log("Reader Set");

            _reader.CardAdded += OnCardAdded;
        }

        private async void OnCardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            // カード検索
            var card = args.SmartCard;
            if (card == null)
            {
                return;
            }
            Log("Card OK");

            // カードタイプ判別
            using (var con = await card.ConnectAsync())
            {
                IccDetection detection = new IccDetection(card, con);
                await detection.DetectCardTypeAync();

                Log(" CardName : " + detection.PcscCardName.ToString());
                Log(" DeviceClass : " + detection.PcscDeviceClass.ToString());

                if (detection.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                    && detection.PcscCardName == Pcsc.CardName.FeliCa)

                    new Controller.FelicaController(con);


            }
        }

        private async void Polling()
        {
            // カード検索
            var cards = await _reader.FindAllCardsAsync();
            var card = cards.FirstOrDefault();
            if (card == null)
            {
                return;
            }
            Log("Card OK");

            // カードタイプ判別
            using (var con = await card.ConnectAsync())
            {
                IccDetection detection = new IccDetection(card, con);
                await detection.DetectCardTypeAync();

                Log(" CardName : " + detection.PcscCardName.ToString());
                Log(" DeviceClass : " + detection.PcscDeviceClass.ToString());

                if (detection.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                    && detection.PcscCardName == Pcsc.CardName.FeliCa)

                    new Controller.FelicaController(con);
                {
                }
            }
        }

        private async void Log(String message)
        {
            Debug.WriteLine(message);
        }

    }
}
