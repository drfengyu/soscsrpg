using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Functions;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class AttackWithWeapon:IAction
    {
        private readonly ShopItem weapon;
        private readonly int maxDamage;
        private readonly int minDamage;
        public event EventHandler<string> OnActionPerformed;
        public AttackWithWeapon(ShopItem shopItem, int maxDamage, int minDamage) {
            if (weapon.Category!=ShopItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }
            if (minDamage<0)
            {
                throw new ArgumentException("minimumDamage must be 0 or larger");
            }
            if (maxDamage < minDamage) { 
                    throw new ArgumentException("maximumDamage must be larger than minimumDamage");
            }
            this.weapon = shopItem;
            this.maxDamage = maxDamage;
            this.minDamage = minDamage;
        }
        public void Execute(LivingEntity actor, LivingEntity target) { 
            int damage=RandomNumberGenerator.NumberBetween(minDamage, maxDamage);
            string actorName=(actor is Player)?"You":$"{actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"{target.Name.ToLower()}";
            if (damage == 0)
            {
                ReportResult($"{actorName} missed {targetName}");

            }
            else { 
                ReportResult($"{actorName} hit {targetName} for {damage} point{(damage>1?"s":"")}.");
                target.TakeDamage(damage);
            }
        }
        private void ReportResult(string message) {
            OnActionPerformed?.Invoke(this,message);
        }

        
    }
}
