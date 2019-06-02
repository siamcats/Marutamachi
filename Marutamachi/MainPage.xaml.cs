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

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Marutamachi
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Polling();
        }

        private async void Polling()
        {
            // Reader検索
            var selector = SmartCardReader.GetDeviceSelector(SmartCardReaderKind.Any);
            var devices = await DeviceInformation.FindAllAsync(selector);
            var device = devices.FirstOrDefault();
            if (device == null)
            {
                return;
            }
            DevMsg.Text = DevMsg.Text + "device ok \r\n";

            var reader = await SmartCardReader.FromIdAsync(device.Id);
            if (reader == null)
            {
                return;
            }
            DevMsg.Text = DevMsg.Text + "reader ok \r\n";

            // カード検索
            var cards = await reader.FindAllCardsAsync();
            var card = cards.FirstOrDefault();
            if (card == null)
            {
                return;
            }
            DevMsg.Text = DevMsg.Text + "Cardを検出 \r\n";

            // カードタイプ判別
            using (var con = await card.ConnectAsync())
            {
                IccDetection detection = new IccDetection(card, con);
                await detection.DetectCardTypeAync();

                DevMsg.Text = DevMsg.Text + " CardName : " + detection.PcscCardName.ToString() + "\r\n" ;
                DevMsg.Text = DevMsg.Text + " DeviceClass : " + detection.PcscDeviceClass.ToString() + "\r\n" ;

                if (detection.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass
                    && detection.PcscCardName == Pcsc.CardName.FeliCa)
                {
                    var handler = new Felica.AccessHandler(con);
                    var result = await handler.TransparentExchangeAsync(new byte[] { 6, 0, 0xff, 0xff, 0, 3 });
                }
            }
        }
    }
}
