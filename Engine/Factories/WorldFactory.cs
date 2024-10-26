using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    /// <summary>
    /// 20240928 增加地点的任务
    /// </summary>
    public static class WorldFactory
    {
        public static World CreateWorld() { 
            World world = new World();
            
            
            world.AddLocation(
                -2, -1,
                "Farmer's Field",
                "There are rows of corn growing here, with giant rats hiding between them.",
                "FarmFields.png");
            world.LocationAt(-2, -1).AddMonster(2,100);

            world.AddLocation(
                -1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "Farmhouse.png");
            world.LocationAt(-1, -1).TraderHere = TraderFactory.GetTraderByName("Farmer Ted");

            world.AddLocation(0, -1, "Home", "This is your home", "Home.png");

            world.AddLocation(-1, 0, "Trading Shop", "The shop of Susan, the trader.", "Trader.png");
            world.LocationAt(-1, 0).TraderHere = TraderFactory.GetTraderByName("Susan");

            world.AddLocation(0, 0, "Town square", "You see a fountain here.", "TownSquare.png");
            world.AddLocation(1, 0, "Town Gate", "There is a gate here, protecting the town from giant spiders.", "TownGate.png");

            world.AddLocation(2, 0, "Spider Forest", "The trees in this forest are covered with spider webs.", "SpiderForest.png");
            world.LocationAt(2, 0).AddMonster(3,100);

            world.AddLocation(0, 1, "Herbalist's hut", "You see a small hut, with plants drying from the roof.", "HerbalistsHut.png");
            world.LocationAt(0, 1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));
            world.LocationAt(0,1).TraderHere= TraderFactory.GetTraderByName("Pete the Herbalist");

            world.AddLocation(0, 2, "Herbalist's garden", "There are many plants here, with snakes hiding behind them.", "HerbalistsGarden.png");            
            world.LocationAt(0, 2).AddMonster(1,100);
            return world;
        }
    }
}
