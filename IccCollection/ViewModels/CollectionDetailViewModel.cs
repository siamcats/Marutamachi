using System;
using System.Linq;
using System.Threading.Tasks;

using IccCollection.Core.Models;
using IccCollection.Core.Services;
using IccCollection.Helpers;

namespace IccCollection.ViewModels
{
    public class CollectionDetailViewModel : Observable
    {
        private SampleOrder _item;

        public SampleOrder Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public CollectionDetailViewModel()
        {
        }

        public async Task InitializeAsync(long orderID)
        {
            var data = await SampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }
    }
}
