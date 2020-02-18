using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using IccCollection.Core.Models;
using IccCollection.Core.Services;
using IccCollection.Helpers;
using IccCollection.Services;
using IccCollection.Views;

using Microsoft.Toolkit.Uwp.UI.Animations;

namespace IccCollection.ViewModels
{
    public class CollectionViewModel : Observable
    {
        private ICommand _itemClickCommand;

        public ICommand ItemClickCommand => _itemClickCommand ?? (_itemClickCommand = new RelayCommand<Card>(OnItemClick));

        public ObservableCollection<Card> Source { get; } = new ObservableCollection<Card>();

        public CollectionViewModel()
        {
        }

        //public async Task LoadDataAsync()
        //{
        //    Source.Clear();

        //    // TODO WTS: Replace this with your actual data
        //    var data = await SampleDataService.GetContentGridDataAsync();
        //    foreach (var item in data)
        //    {
        //        Source.Add(item);
        //    }
        //}

        public async Task LoadDataAsync()
        {
            Source.Clear();

            var data = await CardService.GetAllCardsAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void OnItemClick(Card clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate<CollectionDetailPage>(clickedItem.Id);
            }
        }
    }
}
