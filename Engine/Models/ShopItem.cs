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

        public int MininumDamage { get; }
        public int MaxinumDamage { get; }

        public ShopItem(ItemCategory category,int itemTypeID,string name,int price,bool isUnique = false,int minDamage = 0,int maxDamage = 0) {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            MininumDamage = minDamage;
            MaxinumDamage = maxDamage;
        }
        public ShopItem Clone() { 
            return new ShopItem(Category,ItemTypeID, Name, Price,IsUnique,MininumDamage,MaxinumDamage);
        }
    }
}
