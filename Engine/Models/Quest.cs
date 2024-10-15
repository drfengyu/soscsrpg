using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 任务列表
    /// </summary>
    public class Quest
    {
        public int ID { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public List<ItemQuantity> ItemsToComplete { get; set; }

        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardItems { get; set; }
        public Quest(int id, string name,
            string description,List<ItemQuantity> itemsToComplete,
            int rewardExperiencePoints,int rewardGold,List<ItemQuantity> rewardItems) {
            ID = id;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }


    }
}
