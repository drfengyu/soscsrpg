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
    public class Player : LivingEntity
    {
        private int experiencePoints;
        private string characterClass;
        
        private int level;

        
        public string CharacterClass { get => characterClass; set { characterClass = value; OnPropertyChanged(nameof(CharacterClass)); } }
        
        public int ExperiencePoints { get => experiencePoints; set { experiencePoints = value; OnPropertyChanged(nameof(ExperiencePoints)); } }
        public int Level { get => level; set { level = value; OnPropertyChanged(nameof(Level)); } }
        

        
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public Player()
        {
            
            Quests = new ObservableCollection<QuestStatus>();
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
