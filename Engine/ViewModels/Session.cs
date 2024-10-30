using Engine.EventArgs;
using Engine.Factories;
using Engine.Functions;
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
    public class Session:BaseNotificationClass
    {
        public event EventHandler<MessageEventArgs> OnMessageRaised;

        private Location currentLocation;
        private Monster currentMonster;
        private Trader currentTrader;
        private Player currentPlayer;

        public Player CurrentPlayer { get => currentPlayer;
            set {
                if (currentPlayer!=null)
                {
                    currentPlayer.OnActionPerformed-= OnCurrentPlayerPerformedAction;
                    currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    currentPlayer.OnKilled-=OnCurrentPlayerKilled;
                }
                currentPlayer = value;
                if (currentPlayer != null) { 
                    currentPlayer.OnActionPerformed += OnCurrentPlayerPerformedAction;
                    currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    currentPlayer.OnKilled += OnCurrentPlayerKilled;    
                }
            } }

       

        public Location CurrentLocation { get => currentLocation;
            set { 
                currentLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));
                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
                CurrentTrader=CurrentLocation.TraderHere;
            }
        }

        public Monster CurrentMonster { get => currentMonster;
            set {
                if (currentMonster != null) { 
                        currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }
                currentMonster = value;
                if (CurrentMonster != null)
                {
                    CurrentMonster.OnKilled += OnCurrentMonsterKilled;
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here.");
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
                
            }
        }

        

        public Trader CurrentTrader { get => currentTrader;
            set
            {
                currentTrader = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            } }
        

        public bool HasMonster => CurrentMonster != null;

        public bool HasTrader => CurrentTrader != null;

        public bool HasLocationToNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        public bool HasLocationToSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        public bool HasLocationToWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
        public bool HasLocationToEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        public World CurrentWorld { get; }
        

        public Session() {
            CurrentPlayer = new Player("Xiao Yi","Fighter",0,10,10,10);
            
            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateShopItem(1001));
            }
            CurrentWorld =WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0,0);
            
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
                    RaiseMessage("");
                    RaiseMessage($"you found a quest: {quest.Name}");
                    RaiseMessage(quest.Description);
                    RaiseMessage("Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateShopItem(itemQuantity.ItemID).Name}");
                    }
                    RaiseMessage("And you will receive:");
                    RaiseMessage($"{quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"{quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems) {
                        RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateShopItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void CompleteQuestsAtLocation() {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete=CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID
                && !q.IsCompleted);
                if (questToComplete != null) {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete)) {
                        //Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                            {
                                for (int i = 0; i < itemQuantity.Quantity; i++) {
                                    CurrentPlayer.RemoveItemFromInventory(
                                        CurrentPlayer.Inventory.First(item=>item.ItemTypeID==itemQuantity.ItemID));
                                }
                            }
                            RaiseMessage("");
                            RaiseMessage($"You complete the '{quest.Name}' quest.");
                            //Give the player the quest rewards
                            CurrentPlayer.AddExperience(quest.RewardExperiencePoints);
                            RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points.");
                            CurrentPlayer.ReceiveGold(quest.RewardGold);
                            RaiseMessage($"You receive {quest.RewardGold} gold.");
                            
                            foreach (ItemQuantity itemQuantity in quest.RewardItems) 
                            {
                                ShopItem rewardItem= ItemFactory.CreateShopItem(itemQuantity.ItemID);
                                CurrentPlayer.AddItemToInventory(rewardItem);
                                RaiseMessage($"You receive a {rewardItem.Name}.");
                            }
                            //Mark the Quest as completed
                            questToComplete.IsCompleted= true;
                        
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        

        public void AttackCurrentMonster()
        {
            if (CurrentPlayer.CurrentWeapon==null)
            {
                RaiseMessage("You must select a weapon first.to attack");
                return;
            }
            //Determine damage to monster
            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);
            
            if (CurrentMonster.isDead)
            {
               
                GetMonsterAtLocation();
            }
            else { 
                //If monster is still alive,let the monster attack
                int damageToPlayer=RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);
                if (damageToPlayer == 0) { 
                    RaiseMessage($"The {CurrentMonster.Name} attacks,but misses you.");
                }
                else
                {
                    //CurrentPlayer.CurrentHitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
               
            }
                    
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"You have been killed.");
            int LostExperiencePoints=RandomNumberGenerator.NumberBetween(5, 100);
            CurrentPlayer.AddExperience(-LostExperiencePoints);
            RaiseMessage($"You lost {LostExperiencePoints} experience points.");
            int LostGold =CurrentPlayer.Gold/RandomNumberGenerator.NumberBetween(2, 10);
            CurrentPlayer.ReceiveGold(-LostGold);
            RaiseMessage($"You lost {LostGold} gold.");
            CurrentLocation = CurrentWorld.LocationAt(0, -1);//Player's home
            CurrentPlayer.CompletelyHeal();
            //CurrentPlayer.CurrentHitPoints = CurrentPlayer.Level * 10; //Completely heal the player
        }
        private void OnCurrentPlayerPerformedAction(object sender, string result) { 
                RaiseMessage(result);
        }
        private void OnCurrentMonsterKilled(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);
            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);
            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            foreach (ShopItem shopItem in CurrentMonster.Inventory)
            {
                CurrentPlayer.AddItemToInventory(shopItem);
                RaiseMessage($"You receive one {shopItem.Name}.");
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs e)
        {
            RaiseMessage($"You leveled up to level {CurrentPlayer.Level}!");
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new MessageEventArgs(message));
        }
    }
}
