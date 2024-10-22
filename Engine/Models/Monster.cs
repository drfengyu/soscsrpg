﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster:LivingEntity
    {
        public string ImageName { get; set; }
        
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        
        public int RewardExperiencePoints { get; set; }
        
        public Monster(string name, string imageName, int maximumHitPoints,int currentHitPoints,int minimumDamage,int maxmumDamage, int rewardExperiencePoints, int gold):base(name,maximumHitPoints,currentHitPoints,gold) //base is the parent class
        {
            
            ImageName = string.Format("/Engine;component/Images/Monsters/{0}",imageName);
           
            
            MinimumDamage = minimumDamage;
            MaximumDamage = maxmumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            
           
        }
    }
}
