using Engine.Models;
using Engine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SOSCSRPG
{
    /// <summary>
    /// TraderScreen.xaml 的交互逻辑
    /// </summary>
    public partial class TraderScreen : Window
    {
        public Session Session=>DataContext as Session;
        public TraderScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            ShopItem item=((FrameworkElement)sender).DataContext as ShopItem;
            if (item != null) {
                if (Session.CurrentPlayer.Gold >= item.Price)
                {
                    Session.CurrentPlayer.Gold -= item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(item);
                    Session.CurrentPlayer.AddItemToInventory(item);
                }
                else
                {
                    MessageBox.Show("You don't have enough gold to buy that!");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            ShopItem item=((FrameworkElement)sender).DataContext as ShopItem;
            if (item!=null)
            {
                Session.CurrentPlayer.Gold+=item.Price;
                Session.CurrentTrader.AddItemToInventory(item);
                Session.CurrentPlayer.RemoveItemFromInventory(item);
            }
        }
    }
}
