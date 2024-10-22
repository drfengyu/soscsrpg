﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class LivingEntity:BaseNotificationClass
    {
        #region Properties
        private string name;
        private int currentHitPoints;
        private int maximumHitPoints;
        private int gold;

        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public int CurrentHitPoints { get => currentHitPoints; set { currentHitPoints = value;OnPropertyChanged(nameof(CurrentHitPoints)); } }

        public int MaximumHitPoints { get => maximumHitPoints; set { maximumHitPoints = value; OnPropertyChanged(nameof(MaximumHitPoints)); } }

        public int Gold { get => gold; set { gold = value; OnPropertyChanged(nameof(Gold)); } }
        public ObservableCollection<ShopItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }
        public List<ShopItem> Weapons=>Inventory.Where(i=>i is Weapon).ToList();
        public bool isDead=>CurrentHitPoints<=0;
        #endregion
        public event EventHandler OnKilled;
        protected LivingEntity(string name,int maximumHitPoints,int currentHitPoints,int gold) {
                this.name = name;
                this.maximumHitPoints = maximumHitPoints;
                this.currentHitPoints = currentHitPoints;
                this.gold = gold;
                Inventory = new ObservableCollection<ShopItem>();   
                GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void TakeDamage(int hitPointsOfDamage) {
                CurrentHitPoints -= hitPointsOfDamage;
            if (isDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this,new System.EventArgs());
        }

        public void Heal(int hitPointsToHeal) { 
            CurrentHitPoints += hitPointsToHeal;
            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints= MaximumHitPoints;
            }
        }

        public void CompletelyHeal() { 
               CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int rewardgold) { 
            this.Gold += rewardgold;
        }

        public void SpendGold(int paygold) {
            if (paygold > Gold) {
                throw new Exception("{Name} doesn't have enough gold to spend {gold} gold");
            }
            Gold -= paygold;

        }

        public void AddItemToInventory(ShopItem item) { 
            Inventory.Add(item);
            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else {
                if (!GroupedInventory.Any(i => i.Item.ItemTypeID == item.ItemTypeID)) { 
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));

                }
                GroupedInventory.First(i => i.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
            OnPropertyChanged(nameof(Weapons));
        }
        public void RemoveItemFromInventory(ShopItem item) { 
            Inventory.Remove(item);
            GroupedInventoryItem groupedInventoryItemToRemove =item.IsUnique?GroupedInventory.FirstOrDefault(i=>i.Item==item):GroupedInventory.FirstOrDefault(i=>i.Item.ItemTypeID==item.ItemTypeID);
            if (groupedInventoryItemToRemove != null) {
                if (groupedInventoryItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else { 
                    groupedInventoryItemToRemove.Quantity--;
                }
            }
            OnPropertyChanged(nameof(Weapons));
        }
    }
}
