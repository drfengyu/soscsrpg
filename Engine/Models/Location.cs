using Engine.Factories;
using Engine.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    /// <summary>
    /// 202410150926新增地点的可用任务
    /// </summary>
    public class Location
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }

        public string ImageName { set; get; }

        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();

        public List<MonsterEncounter> MonstersHere { get; set; } = new List<MonsterEncounter>();

        public void AddMonster(int monsterID, int chanceOfEncountering) {
            if (MonstersHere.Exists(m=>m.MonsterID==monsterID))
            {
                MonstersHere.First(m => m.MonsterID == monsterID).ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                MonstersHere.Add(new MonsterEncounter(monsterID,chanceOfEncountering));
            }
        }

        public Monster GetMonster() {
            if (!MonstersHere.Any())
            {
                return null;
            }
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);

            int randomNumber=RandomNumberGenerator.NumberBetween(1,totalChances);

            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if (randomNumber <= runningTotal) { 
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }
    }
}
