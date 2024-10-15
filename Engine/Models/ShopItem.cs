using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class ShopItem
    {
        public int ItemTypeID { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }

        public ShopItem(int itemTypeID,string name,int price) { 
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
        }
        public ShopItem Clone() { 
            return new ShopItem(ItemTypeID, Name, Price);
        }
    }
}
