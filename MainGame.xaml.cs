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
using System.Windows.Threading;

namespace Pong3._0
{
    /// <summary>
    /// Interaction logic for MainGame.xaml
    /// </summary>
    public partial class MainGame : Window
    {
        private Model Model = new Model();
        public DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random((int)DateTime.Now.Ticks);
        int delay = 50;
        int countTicks = 0;
        public MainGame()
        {
            InitializeComponent();
            DataContext = Model;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Stop();
            timer.Tick += GameTickCalculation;
        }

        private double angle = 155;
        private double speed = 7;
        private int padSpeed = 50;

        void GameTickCalculation(object sender, EventArgs e)
        {
            if (Model.BallYPosition <= 0)
                angle = angle + (180 - 2 * angle);
            if (Model.BallYPosition >= MainCanvas.ActualHeight - 20)
                angle = angle + (180 - 2 * angle);

            if (CheckCollision())
            {
                ChangeAngle();
                Model.changeBallDirection();
            }

            double radians = (Math.PI / 180) * angle;
            Vector vector = new Vector { X = Math.Sin(radians), Y = -Math.Cos(radians) };
            Model.BallXPosition += vector.X * speed;
            Model.BallYPosition += vector.Y * speed;

            if (Model.BallXPosition >= MainCanvas.ActualWidth - 10)
            {
                countTicks = 0;
                GameResetBallPosition();
                Model.LeftResult += 1;
                countTicks++;

                if (Model.LeftResult == 25)
                {
                    speed = 10;
                }
                if (Model.LeftResult == 50)
                {
                    speed = 15;
                }
            }
            if (Model.BallXPosition <= -10)
            {
                Model.RightResult += 1;
                GameResetBallPosition();
                if (Model.RightResult == 25)
                {
                    speed = 10;
                }
                if (Model.RightResult == 50)
                {
                    speed = 15;
                }
            }
        }

        private void GameResetBallPosition()
        {
            Model.BallXPosition = 380;
            Model.BallYPosition = 210;
        }

        private void ChangeAngle()
        {
            if (Model.IsBallDirectionRight)
                angle = 270 - ((Model.BallYPosition + 10) - (Model.RightPadPosition + 40));
            else
                angle = 90 + ((Model.BallYPosition + 10) - (Model.LeftPadPosition + 40));
        }

        private bool CheckCollision()
        {
            if (Model.IsBallDirectionRight)
                return Model.BallXPosition >= 760 && (Model.BallYPosition > Model.RightPadPosition - 20 && Model.BallYPosition < Model.RightPadPosition + 80);
            return Model.BallXPosition <= 20 && (Model.BallYPosition > Model.LeftPadPosition - 20 && Model.BallYPosition < Model.LeftPadPosition + 80);
        }

        private void MainWindow_OnKeyDown(object sender, KeyboardEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W)) Model.LeftPadPosition = verifyBounds(Model.LeftPadPosition, -padSpeed);
            if (Keyboard.IsKeyDown(Key.S)) Model.LeftPadPosition = verifyBounds(Model.LeftPadPosition, padSpeed);
            if (Keyboard.IsKeyDown(Key.Up)) Model.RightPadPosition = verifyBounds(Model.RightPadPosition, -padSpeed);
            if (Keyboard.IsKeyDown(Key.Down)) Model.RightPadPosition = verifyBounds(Model.RightPadPosition, padSpeed);
            if (Keyboard.IsKeyDown(Key.Escape)) timer.Stop();
            if (Keyboard.IsKeyDown(Key.Space)) timer.Start();
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.F4)) System.Windows.Application.Current.Shutdown();
        }

        private int verifyBounds(int position, int change)
        {
            position += change;

            if (position < 0)
                position = 0;
            if (position > (int)MainCanvas.ActualHeight - 90)
                position = (int)MainCanvas.ActualHeight - 90;
            return position;
        }
    }
}