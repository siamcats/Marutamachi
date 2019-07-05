using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;
using Pcsc.Common;
using System.Diagnostics;
using Marutamachi.ViewModel;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Marutamachi
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; set; } = new MainPageViewModel();
        
        public MainPage()
        {
            DataContext = ViewModel;
            this.InitializeComponent();
        }

        private async void Log(String message)
        {
            Debug.WriteLine(message);
        }

    }
}
