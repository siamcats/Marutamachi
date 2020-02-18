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
        private Card _item;

        public Card Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public CollectionDetailViewModel()
        {
        }

        public async Task InitializeAsync(long id)
        {
            var data = await CardService.GetAllCardsAsync();
            Item = data.First(i => i.Id == id);
        }
    }
}
