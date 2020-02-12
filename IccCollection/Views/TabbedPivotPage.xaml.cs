using System;

using IccCollection.ViewModels;

using Windows.UI.Xaml.Controls;

namespace IccCollection.Views
{
    public sealed partial class TabbedPivotPage : Page
    {
        public TabbedPivotViewModel ViewModel { get; } = new TabbedPivotViewModel();

        public TabbedPivotPage()
        {
            InitializeComponent();
        }
    }
}
