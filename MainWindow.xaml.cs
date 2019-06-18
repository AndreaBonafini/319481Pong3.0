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
using System.Windows.Threading;

namespace Pong3._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainGame mg = new MainGame();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome in Pong.\nThis pong requires two players\n" +
                "For Player 1 on the left side of the field press W to move Up and S to move down\n" +
                "For Player 2 on the right side of the field press ArrowUP to move Up and ArrowDown to move down\n" +
                "To stop the game press the key Escape\n" +
                "To resume the game press the key Space\n" +
                "To close the game press Alt+F4","Welcome");
            mg.Hide();
            this.Show();
            this.Hide();
            mg.Show();
            mg.timer.Start();
        }
    }
}