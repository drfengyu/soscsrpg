using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster:BaseNotificationClass
    {
        public string Name { get; set; }
        
        public string ImageName { get; set; }
        public int HitPoints { get; set; }
        public int MaximumHitPoints { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        public Monster(string name, string imageName, int hitPoints,int maximumHitPoints, int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            HitPoints = hitPoints;
            ImageName = string.Format("./Engine;component/Images/Monsters/{0}",imageName);
            MaximumHitPoints = maximumHitPoints;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            Inventory = new ObservableCollection<ItemQuantity>() ;
        }
    }
}
