using System;
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
        private int level;
        private ShopItem currentWeapon;
        private ShopItem currentConsumable;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public int CurrentHitPoints { get => currentHitPoints; set { currentHitPoints = value;OnPropertyChanged(); } }

        public int MaximumHitPoints { get => maximumHitPoints; set { maximumHitPoints = value; OnPropertyChanged(); } }

        public int Gold { get => gold; set { gold = value; OnPropertyChanged(); } }

        public int Level { get => level;protected set { 
                level = value; 
                OnPropertyChanged();
            } }
        public ShopItem CurrentWeapon { get => currentWeapon;
            set {
                if (currentWeapon != null)
                {
                  currentWeapon.Action.OnActionPerformed-=RaiseActionPerformedEvent;
                }
                currentWeapon = value;
                if (currentWeapon != null) { 
                        currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            } }

        public ShopItem CurrentConsumable { get => currentConsumable;
            set {
                if (currentConsumable != null) {
                    currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                currentConsumable = value;
                if (currentConsumable!=null)
                {
                    currentConsumable.Action.OnActionPerformed+= RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ShopItem> Inventory { get; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; }
        public List<ShopItem> Weapons=>Inventory.Where(i=>i.Category==ShopItem.ItemCategory.Weapon).ToList();
        
        public List<ShopItem> Consumables=>Inventory.Where(i=>i.Category==ShopItem.ItemCategory.Consumable).ToList();
        public bool isDead=>CurrentHitPoints<=0;
        public bool HasConsumable => Consumables.Any();
        #endregion
        public event EventHandler OnKilled;
        public event EventHandler<string> OnActionPerformed;
        protected LivingEntity(string name,int maximumHitPoints,int currentHitPoints,int gold,int level=1) {
                this.name = name;
                this.maximumHitPoints = maximumHitPoints;
                this.currentHitPoints = currentHitPoints;
                this.gold = gold;
                this.Level = level;
                Inventory = new ObservableCollection<ShopItem>();   
                GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void UseCurrentWeaponOn(LivingEntity target) {
            CurrentWeapon.PerformActionOn(this,target);
        }

        public void UseCurrentConsumable() { 
            CurrentConsumable.PerformActionOn(this,this);
            RemoveItemFromInventory(CurrentConsumable);
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
        private void RaiseActionPerformedEvent(object sender, string result) { 
            OnActionPerformed?.Invoke(this,result);
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
            OnPropertyChanged(nameof(Consumables));
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
            OnPropertyChanged(nameof(Consumables));
        }
    }
}
