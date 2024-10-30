using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Functions;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class AttackWithWeapon
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
        public void Excute(LivingEntity actor, LivingEntity target) { 
            int damage=RandomNumberGenerator.NumberBetween(minDamage, maxDamage);
            if (damage == 0)
            {
                ReportResult($"You missed the {target.Name.ToLower()}");

            }
            else { 
                ReportResult($"You hit the {target.Name.ToLower()} for {damage} damage");
            }
        }
        private void ReportResult(string message) {
            OnActionPerformed?.Invoke(this,message);
        }
    }
}
