using Engine.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class ShopItem
    {
        public enum ItemCategory { 
            Miscellaneous,
            Weapon
        }
        public ItemCategory Category { get; }
        public int ItemTypeID {  get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }

        public AttackWithWeapon Action { get; set; }

        public ShopItem(ItemCategory category,int itemTypeID,string name,int price,bool isUnique = false,AttackWithWeapon action=null) {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
        }
        public void PerformActionOn(LivingEntity actor,LivingEntity target)
        {
            Action?.Excute(actor, target);
        }
        public ShopItem Clone() { 
            return new ShopItem(Category,ItemTypeID, Name, Price,IsUnique,Action);
        }
    }
}
