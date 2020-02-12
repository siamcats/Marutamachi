using System;

using IccCollection.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IccCollection.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
