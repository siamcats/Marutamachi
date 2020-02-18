using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IccCollection.Core.Models;
using System.Threading.Tasks;

namespace IccCollection.Core.Services
{
    public static class CardService
    {
        private static IEnumerable<Card> _allCards;

        private static IEnumerable<Card> AllCards()
        {
            using (var context = new IccContext())
            {
                return context.Cards.ToList();
            }

        }
        public static async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            if (_allCards == null)
            {
                _allCards = AllCards();
            }

            await Task.CompletedTask;
            return _allCards;
        }
    }
}
