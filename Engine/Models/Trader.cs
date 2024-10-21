using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Trader:BaseNotificationClass
    {
        public string Name { set; get; }
        public ObservableCollection<ShopItem> Inventory { get; set; }
        public Trader(string name) { 
            Name = name;
            Inventory = new ObservableCollection<ShopItem>();
        }
        public void AddItemToInventory(ShopItem item) { 
                Inventory.Add(item);
        }
        public void RemoveItemFromInventory(ShopItem item) {
                Inventory.Remove(item);
        }
    }
}
