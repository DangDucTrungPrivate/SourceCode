using System.Collections.Generic;
using System.Linq;
using GCMP.Models.Entities;
using GCMP.Models.Helper;

namespace GCMP.Models
{
    public class CardModel
    {
        private readonly GCMPDBEntities _db = null;

        public CardModel()
        {
            _db = new GCMPDBEntities();
        }

        //trungdd- get all available cards in a single category, ordered by date add
        public List<Card> GetCardsInCategory(int id)
        {
            var cardslist =
                _db.Cards.Where(c => c.CategoryId == id && c.Status == Const.CaActive)
                    .OrderByDescending(o => o.LastComment)
                    .ToList();

            return cardslist;
        }
    }
}