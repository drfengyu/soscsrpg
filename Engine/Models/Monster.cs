﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster:BaseNotificationClass
    {
        private int hitPoints;

        public string Name { get; set; }
        
        public string ImageName { get; set; }
        public int HitPoints { get => hitPoints; set { hitPoints = value; OnPropertyChanged(nameof(HitPoints)); } }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int MaximumHitPoints { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        public Monster(string name, string imageName, int maximumHitPoints,int hitPoints,int minimumDamage,int maxmumDamage, int rewardExperiencePoints, int rewardGold)
        {
            Name = name;
            ImageName = string.Format("/Engine;component/Images/Monsters/{0}",imageName);
            MaximumHitPoints = maximumHitPoints;
            HitPoints = hitPoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maxmumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            Inventory = new ObservableCollection<ItemQuantity>() ;
        }
    }
}
