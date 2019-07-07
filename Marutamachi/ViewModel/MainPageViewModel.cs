using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;
using Pcsc.Common;
using Marutamachi.Model;

namespace Marutamachi.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public SmartCardReader Reader;

        //public List<Card> AllCards = new List<Card>();
        public ObservableCollection<Card> AllCards = new ObservableCollection<Card>();

        public ObservableCollection<Card> ReadCard = new ObservableCollection<Card>();

        public MainPageViewModel()
        {
            LoadCardData();
            DeviceSetAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;


        private async void DeviceSetAsync()
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

            Reader = await SmartCardReader.FromIdAsync(device.Id);
            if (Reader == null)
            {
                return;
            }
            Log("Reader Set");

            Reader.CardAdded += OnCardAdded;

            return;
        }


        public async void OnCardAdded(SmartCardReader sender, CardAddedEventArgs args)
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


        public void IsSavePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Card toggledCard = sender as Card;
            if (toggledCard.IsSave)
            {
                AllCards.Add(toggledCard);
            }
            else
            {
                AllCards.Remove(toggledCard);
            }
        }


        private void LoadCardData()
        {
            ReadCard.Add(new Card()
            {
                Name = "MyEdyCard",
                MoneyType = MoneyType.Edy,
                IsSave = false
            });

            AllCards.Add(new Card()
            {
                Name = "MySuicaCard",
                MoneyType = MoneyType.Suica,
                IsSave = true
            });
            AllCards.Add(new Card()
            {
                Name = "MyNanacoCard",
                MoneyType = MoneyType.nanaco,
                IsSave = true
            });

            foreach (var card in ReadCard)
            {
                card.PropertyChanged += IsSavePropertyChanged;
                if (card.IsSave)
                {
                    AllCards.Add(card);
                }
            }
        }


        private void Log(String message)
        {
            Debug.WriteLine(message);
        }


    }
}
