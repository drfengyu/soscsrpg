using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GroupedInventoryItem:BaseNotificationClass
    {
        private ShopItem item;
        private int quantity;

        public ShopItem Item { get => item; set { item = value;OnPropertyChanged(nameof(Item)); } }
        public int Quantity { get => quantity; set { quantity = value;OnPropertyChanged(nameof(Quantity)); } }
        public GroupedInventoryItem(ShopItem item, int quantity) { 
               Item = item;
               Quantity = quantity;
        }
    }
}
