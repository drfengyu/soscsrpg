using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class LivingEntity:BaseNotificationClass
    {
        private string name;
        private int currentHitPoints;
        private int maximumHitPoints;
        private int gold;

        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public int CurrentHitPoints { get => currentHitPoints; set { currentHitPoints = value;OnPropertyChanged(nameof(CurrentHitPoints)); } }

        public int MaximumHitPoints { get => maximumHitPoints; set { maximumHitPoints = value; OnPropertyChanged(nameof(MaximumHitPoints)); } }

        public int Gold { get => gold; set { gold = value; OnPropertyChanged(nameof(Gold)); } }
        public ObservableCollection<ShopItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }
        public List<ShopItem> Weapons=>Inventory.Where(i=>i is Weapon).ToList();

        protected LivingEntity() { 
                Inventory = new ObservableCollection<ShopItem>();
                GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void AddItemToInventory(ShopItem item) { 
            Inventory.Add(item);
            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else {
                if (!GroupedInventory.Any(i => i.Item.ItemTypeID == item.ItemTypeID)) { 
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));

                }
                GroupedInventory.First(i => i.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
            OnPropertyChanged(nameof(Weapons));
        }
        public void RemoveItemFromInventory(ShopItem item) { 
            Inventory.Remove(item);
            GroupedInventoryItem groupedInventoryItemToRemove = GroupedInventory.FirstOrDefault(i=>i.Item==item);
            if (groupedInventoryItemToRemove != null) {
                if (groupedInventoryItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else { 
                    groupedInventoryItemToRemove.Quantity--;
                }
            }
            OnPropertyChanged(nameof(Weapons));
        }
    }
}
