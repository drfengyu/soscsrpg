using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.ViewModels
{
    /// <summary>
    /// 202410150952 检查当前位置任务可用状态
    /// </summary>
    public class GameSession:BaseNotificationClass
    {
        private Location currentLocation;

        public Player CurrentPlayer { get; set;  }
        public Location CurrentLocation { get => currentLocation;
            set { 
                currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                GivePlayerQuestsAtLocation();
            }
        }

        

        public bool HasLocationToNorth { 
            get { return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null; }
        }
        public bool HasLocationToSouth
        {
            get { return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate-1) != null; }
        }
        public bool HasLocationToWest
        {
            get { return CurrentWorld.LocationAt(CurrentLocation.XCoordinate-1, CurrentLocation.YCoordinate) != null; }
        }
        public bool HasLocationToEast
        {
            get { return CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null; }
        }



        public World CurrentWorld { set; get; }
        public GameSession() {
            CurrentPlayer = new Player
            {
                Name = "Scott",
                Gold = 1000000,
                CharacterClass = "Fighter",
                HitPoints = 10,
                ExperiencePoints = 0,
                Level = 1,
                
            };

            //CurrentLocation = new Location
            //{
            //    Name = "Home",
            //    XCoordinate = 0,
            //    YCoordinate = -1,
            //    Description = "This is your House.",
            //    ImageName = "/Engine;component/Images/Locations/Home.png"
            //};


            
            CurrentWorld=WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0,0);
            CurrentPlayer.Inventory.Add(ItemFactory.CreateShopItem(1001));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateShopItem(1002));
            CurrentPlayer.Inventory.Add(ItemFactory.CreateShopItem(1002));
        }

        

        public void MoveNorth() {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }
        public void MoveWest() {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }

        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere) {
                if (!CurrentPlayer.Quests.Any(q=>q.PlayerQuest.ID==quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }
    }
}
