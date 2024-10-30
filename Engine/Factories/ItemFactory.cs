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
        private static readonly List<ShopItem> shopItems = new List<ShopItem>();
        static ItemFactory()
        {
            //shopItems = new List<ShopItem>();
            BuildWeapon(1001, "Pointy Stick", 10, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 20, 1, 3);
            BuildWeapon(1501,"Snake fangs",0,0,2);
            BuildWeapon(1502, "Rat claws", 0, 0, 2);
            BuildWeapon(1503, "Spider fangs", 0, 0, 4);

            BuildMiscellaneousItem(9001, "Snake fang", 1);
            BuildMiscellaneousItem(9002, "Snakeskin", 2);
            BuildMiscellaneousItem(9003, "Rat tail", 1);
            BuildMiscellaneousItem(9004, "Rat fur", 2);
            BuildMiscellaneousItem(9005, "Spider fang", 1);
            BuildMiscellaneousItem(9006, "Spider silk", 2);
        }
        public static ShopItem CreateShopItem(int itemTypeID)
        {
            return shopItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }

        public static void BuildMiscellaneousItem(int id, string name, int price)
        {
            shopItems.Add(new ShopItem(ShopItem.ItemCategory.Miscellaneous, id, name, price));
        }
        public static void BuildWeapon(int id, string name, int price, int minDamage, int maxDamage)
        {
            ShopItem weapon = new ShopItem(ShopItem.ItemCategory.Weapon, id, name, price, true);
            weapon.Action = new Actions.AttackWithWeapon(weapon,minDamage, maxDamage);
            shopItems.Add(weapon);
            
        }
    }
}
