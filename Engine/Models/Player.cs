using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 202410150940增加Quests
    /// </summary>
    public class Player : BaseNotificationClass
    {
        private int experiencePoints;
        private int hitPoints;
        private string characterClass;
        private string name;
        private int level;
        private int gold;

        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string CharacterClass { get => characterClass; set { characterClass = value; OnPropertyChanged(nameof(CharacterClass)); } }
        public int HitPoints { get => hitPoints; set { hitPoints = value; OnPropertyChanged(nameof(HitPoints)); } }
        public int ExperiencePoints { get => experiencePoints; set { experiencePoints = value; OnPropertyChanged(nameof(ExperiencePoints)); } }
        public int Level { get => level; set { level = value; OnPropertyChanged(nameof(Level)); } }
        public int Gold { get => gold; set { gold = value; OnPropertyChanged(nameof(Gold)); } }

        public ObservableCollection<ShopItem> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public List<ShopItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        public Player()
        {
            Inventory = new ObservableCollection<ShopItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }
        public void AddItemToInventory(ShopItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(ShopItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
