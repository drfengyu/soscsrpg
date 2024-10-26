﻿using Engine.Factories;
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
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public string Name { set; get; }
        public string Description {  get; }

        public string ImageName { get; }

        public List<Quest> QuestsAvailableHere { get; } = new List<Quest>();

        public List<MonsterEncounter> MonstersHere { get; } = new List<MonsterEncounter>();
        public Trader TraderHere { get; set; }

        public Location(int xCoordinate, int yCoordinate, string name, string description, string imageName) { 
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;

        }

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
            //Total the percentages of all monsters at this location
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);
            //Select a random number between 1 and the total(in case the total chances is not 100).
            int randomNumber=RandomNumberGenerator.NumberBetween(1,totalChances);
            //Loop through the monster list,
            //adding the monster's percentage chance of appearing to the runningtotal varible.
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if (randomNumber <= runningTotal) { 
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }
            //If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }
    }
}
