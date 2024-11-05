using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class Heal : IAction
    {
        private readonly ShopItem _shopItem;
        public readonly int _hitPointsToHeal;

        public event EventHandler<string> OnActionPerformed;

        public Heal(ShopItem shopItem, int hitPointsToHeal) {
            if (shopItem.Category != ShopItem.ItemCategory.Consumable) { 
                throw new ArgumentException($"{shopItem.Name} is not a consumable.");
            }
            _shopItem = shopItem;
            _hitPointsToHeal = hitPointsToHeal;

        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName=(actor is Player)? "You" : actor.Name;
            string targetName = (target is Player) ? "yourself" : target.Name;
            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal>1?"s":"")}.");
            target.Heal(_hitPointsToHeal);
        }

        private void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
