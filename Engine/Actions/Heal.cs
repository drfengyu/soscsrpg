using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class Heal :BaseAction,IAction
    {
        
        public readonly int _hitPointsToHeal;

        

        public Heal(ShopItem itemInUse, int hitPointsToHeal):base(itemInUse) {
            if (itemInUse.Category != ShopItem.ItemCategory.Consumable) { 
                throw new ArgumentException($"{itemInUse.Name} is not a consumable.");
            }
            
            _hitPointsToHeal = hitPointsToHeal;

        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName=(actor is Player)? "You" : actor.Name;
            string targetName = (target is Player) ? "yourself" : target.Name;
            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal>1?"s":"")}.");
            target.Heal(_hitPointsToHeal);
        }

       
    }
}
