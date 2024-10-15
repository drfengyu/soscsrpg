using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class QuestFactory
    {
        private static readonly List<Quest> quests = new List<Quest>();
        static QuestFactory()
        {
            List<ItemQuantity> itemsToComplete=new List<ItemQuantity>();
            List<ItemQuantity> rewardItems=new List<ItemQuantity>();
            itemsToComplete.Add(new ItemQuantity(9001,5));
            rewardItems.Add(new ItemQuantity(1002,1));

            quests.Add(new Quest(1,"Clear the herb garden",
                "Defect the snakes in the Herbalist's garden",
                itemsToComplete,25,10,rewardItems));


        }
        public static Quest GetQuestByID(int id) { 
            return quests.FirstOrDefault(x => x.ID == id);
        }
    }
}
