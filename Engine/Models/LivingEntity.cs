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
        public List<ShopItem> Weapons=>Inventory.Where(i=>i is Weapon).ToList();

        protected LivingEntity() { 
                Inventory = new ObservableCollection<ShopItem>();
        }

        public void AddItemToInventory(ShopItem item) { 
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }
        public void RemoveItemFromInventory(ShopItem item) { 
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }
    }
}
