using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class ShopItem
    {
        public int ItemTypeID {  get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }

        public ShopItem(int itemTypeID,string name,int price,bool isUnique = false) {
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }
        public ShopItem Clone() { 
            return new ShopItem(ItemTypeID, Name, Price,IsUnique);
        }
    }
}
