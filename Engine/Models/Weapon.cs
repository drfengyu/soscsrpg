using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon:ShopItem
    {
        public int MininumDamage { get; }
        public int MaxinumDamage { get; }

        public Weapon(int itemTypeID,string name,int price,int minDamage,int maxDamage):base(itemTypeID,name,price,true) {
            MaxinumDamage = minDamage;
            MininumDamage = minDamage;
        }
        public new Weapon Clone() {
            return new Weapon(ItemTypeID, Name, Price, MininumDamage, MaxinumDamage);
        }
    }
}
