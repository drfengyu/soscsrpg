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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AduSkin.Controls.Metro;
using Engine.EventArgs;
using Engine.ViewModels;
namespace SOSCSRPG
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Session gameSession;
        public MainWindow()
        {
            InitializeComponent();
            gameSession = new Session();
            gameSession.OnMessageRaised+= OnMessageRaised;
            DataContext=gameSession;
        }

        private void OnMessageRaised(object sender, MessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            gameSession.MoveNorth();
        }

        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            gameSession.MoveSouth();
        }

        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            gameSession.MoveWest();
        }

        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            gameSession.MoveEast();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            gameSession.AttackCurrentMonster();
        }
    }
}
