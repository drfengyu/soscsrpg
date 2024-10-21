using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<ShopItem> shopItems=new List<ShopItem>();
        static ItemFactory() { 
            //shopItems = new List<ShopItem>();
            shopItems.Add(new Weapon(1001,"Pointy Stick",1,1,2));
            shopItems.Add(new Weapon(1002, "Rusty Sword", 5, 1, 3));

            shopItems.Add(new ShopItem(9001,"Snake fang",1));
            shopItems.Add(new ShopItem(9002,"Snakeskin",2));
            shopItems.Add(new ShopItem(9003,"Rat tail",1));
            shopItems.Add(new ShopItem(9004,"Rat fur",2));
            shopItems.Add(new ShopItem(9005,"Spider fang",1));
            shopItems.Add(new ShopItem(9006,"Spider silk",2));
        }
        public static ShopItem CreateShopItem(int itemTypeID) {
            ShopItem shopItem = shopItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);
            if (shopItem!=null)
            {
                if (shopItem is Weapon) { 
                    return (shopItem as Weapon).Clone();
                }
                return shopItem.Clone();
            }
            return null;
        }
    }
}
