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
            world.AddLocation(new Location() {
                XCoordinate=-2,
                YCoordinate=-1,
                Name="Farmer's Field",
                Description= "There are rows of corn growing here, with giant rats hiding between them.",
                ImageName= "/Engine;component/Images/Locations/FarmFields.png"
            });
            world.LocationAt(-2, -1).AddMonster(2,100);
            world.AddLocation(new Location()
            {
                XCoordinate = -1,
                YCoordinate = -1,
                Name = "Farmer's House",
                Description = "This is the house of your neighbor, Farmer Ted.",
                ImageName = "/Engine;component/Images/Locations/Farmhouse.png"
            });
            world.LocationAt(-1, -1).TraderHere = TraderFactory.GetTraderByName("Farmer Ted");
            world.AddLocation(new Location()
            {
                XCoordinate = 0,
                YCoordinate = -1,
                Name = "Home",
                Description = "This is your home",
                ImageName = "/Engine;component/Images/Locations/Home.png"
            });
            world.AddLocation(new Location()
            {
                XCoordinate = -1,
                YCoordinate = 0,
                Name = "Trading Shop",
                Description = "The shop of Susan, the trader.",
                ImageName = "/Engine;component/Images/Locations/Trader.png"
            });
            world.LocationAt(-1, 0).TraderHere = TraderFactory.GetTraderByName("Susan");
            world.AddLocation(new Location()

            {
                XCoordinate = 0,
                YCoordinate = 0,
                Name = "Town square",
                Description = "You see a fountain here.",
                ImageName = "/Engine;component/Images/Locations/TownSquare.png"
            });
            world.AddLocation(new Location()
            {
                XCoordinate = 1,
                YCoordinate = 0,
                Name = "Town Gate",
                Description = "There is a gate here, protecting the town from giant spiders.",
                ImageName = "/Engine;component/Images/Locations/TownGate.png"
            });
            world.AddLocation(new Location()
            {
                XCoordinate = 2,
                YCoordinate = 0,
                Name = "Spider Forest",
                Description = "The trees in this forest are covered with spider webs.",
                ImageName = "/Engine;component/Images/Locations/SpiderForest.png"
            });
            world.LocationAt(2, 0).AddMonster(3,100);
            world.AddLocation(new Location()
            {
                XCoordinate = 0,
                YCoordinate = 1,
                Name = "Herbalist's hut",
                Description = "You see a small hut, with plants drying from the roof.",
                ImageName = "/Engine;component/Images/Locations/HerbalistsHut.png"
            });
            world.LocationAt(0, 1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));
            world.LocationAt(0,1).TraderHere= TraderFactory.GetTraderByName("Pete the Herbalist");
            world.AddLocation(new Location()
            {
                XCoordinate = 0,
                YCoordinate = 2,
                Name = "Herbalist's garden",
                Description = "There are many plants here, with snakes hiding behind them.",
                ImageName = "/Engine;component/Images/Locations/HerbalistsGarden.png"
            });
            world.LocationAt(0, 2).AddMonster(1,100);
            return world;
        }
    }
}
