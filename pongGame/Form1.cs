using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace pongGame
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(50, 170, 10, 60);
        Rectangle player2 = new Rectangle(550, 170, 10, 60);

        Rectangle ball = new Rectangle(250, 195, 10, 10);

        Rectangle player1Goal = new Rectangle(0, 150, 10, 100);
        Rectangle player2Goal = new Rectangle(590, 150, 10, 100);

        Rectangle arenaTop = new Rectangle(0, -10, 600, 15);
        Rectangle arenaBottom = new Rectangle(0, 360, 600, 15);
        Rectangle arenaLeft = new Rectangle(-10, 0, 15, 400);
        Rectangle arenaRight = new Rectangle(595, 0, 15, 400);

        Rectangle player1Right = new Rectangle(60, 170, 3, 60);
        Rectangle player1Left = new Rectangle(49, 170, 3, 60);
        Rectangle player1Up = new Rectangle(50, 169, 10, 3);
        Rectangle player1Down = new Rectangle(50, 230, 10, 3);

        Rectangle player2Right = new Rectangle(560, 170, 3, 60);
        Rectangle player2Left = new Rectangle(549, 170, 3, 60);
        Rectangle player2Up = new Rectangle(550, 169, 10, 3);
        Rectangle player2Down = new Rectangle(550, 230, 10, 3);

        Stopwatch accelTimer = new Stopwatch();
        Stopwatch decelTimer = new Stopwatch();

        int player1Score = 0;
        int player2Score = 0;

        int playerXSpeed = 2;
        int playerYSpeed = 2;
        int ballXSpeed = 0;
        int ballYSpeed = 0;

        int ballMaxSpeed = 7;

        int p1XaccelRate = 0;
        int p1YaccelRate = 0;
        int p2XaccelRate = 0;
        int p2YaccelRate = 0;
        int accelSpeed = 5;
        int maxAccel = 3;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        Pen whitePen = new Pen(Color.White);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //checkifplayer3stopsmoving

                case Keys.W:
                    wDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p1YaccelRate = 0;

                    break;
                case Keys.S:
                    sDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p1YaccelRate = 0;

                    break;
                case Keys.D:
                    dDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p1XaccelRate = 0;

                    break;
                case Keys.A:
                    aDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p1XaccelRate = 0;

                    //checkifplayer2stopsmoving

                    break;
                case Keys.Up:
                    upArrowDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p2YaccelRate = 0;

                    break;
                case Keys.Down:
                    downArrowDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p2YaccelRate = 0;

                    break;
                case Keys.Left:
                    leftArrowDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p2XaccelRate = 0;

                    break;
                case Keys.Right:
                    rightArrowDown = false;

                    accelTimer.Stop();
                    accelTimer.Reset();

                    p2XaccelRate = 0;

                    break;
            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //move ball 
            if (ballXSpeed > ballMaxSpeed)
            {
                ballXSpeed = ballMaxSpeed;
            }

            if (ballYSpeed > ballMaxSpeed)
            {
                ballYSpeed = ballMaxSpeed;
            }

            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;


            //simulatingfriction
            decelTimer.Start();

            if (ballXSpeed > 0 && decelTimer.ElapsedMilliseconds % 20 == 0)
            {
                ballXSpeed--;
            }

            if (ballYSpeed > 0 && decelTimer.ElapsedMilliseconds % 20 == 0)
            {
                ballYSpeed--;
            }

            if (ballXSpeed < 0 && decelTimer.ElapsedMilliseconds % 20 == 0)
            {
                ballXSpeed++;
            }

            if (ballYSpeed < 0 && decelTimer.ElapsedMilliseconds % 20 == 0)
            {
                ballYSpeed++;
            }


            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.IntersectsWith(arenaTop) || ball.IntersectsWith(arenaBottom))
            {
                ballYSpeed += 1;
                ballYSpeed *= -1;
            }

            if (ball.IntersectsWith(arenaLeft) || ball.IntersectsWith(arenaRight))
            {
                ballXSpeed += 1;
                ballXSpeed *= -1;
            }

            //checkingifballoutofbounds&iftrueresetball
            if (ball.Y < -10 || ball.Y < this.Height - this.Height - 15 || ball.X < -10 || ball.X > 600)
            {
                ball.X = 300;
                ball.Y = 195;

                ballXSpeed = 0;
                ballYSpeed = 0;

                ballLostLabel.Text = "BALL LOST";
            }
            else if (decelTimer.ElapsedMilliseconds % 100 == 0)
            {
                ballLostLabel.Text = null;
            }


            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerYSpeed + p1YaccelRate;
                player1Up.Y -= playerYSpeed + p1YaccelRate;
                player1Down.Y -= playerYSpeed + p1YaccelRate;
                player1Right.Y -= playerYSpeed + p1YaccelRate;
                player1Left.Y -= playerYSpeed + p1YaccelRate;

                accelTimer.Start();

                if (p1YaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p1YaccelRate <= maxAccel)
                {
                    p1YaccelRate++;
                }
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerYSpeed + p1YaccelRate;
                player1Up.Y += playerYSpeed + p1YaccelRate;
                player1Down.Y += playerYSpeed + p1YaccelRate;
                player1Right.Y += playerYSpeed + p1YaccelRate;
                player1Left.Y += playerYSpeed + p1YaccelRate;

                accelTimer.Start();

                if (p1YaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p1YaccelRate <= maxAccel)
                {
                    p1YaccelRate++;
                }
            }

            if (dDown == true && player1.X < 590)
            {
                player1.X += playerXSpeed + p1XaccelRate;
                player1Up.X += playerXSpeed + p1XaccelRate;
                player1Down.X += playerXSpeed + p1XaccelRate;
                player1Right.X += playerXSpeed + p1XaccelRate;
                player1Left.X += playerXSpeed + p1XaccelRate;

                accelTimer.Start();

                if (p1XaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p1XaccelRate <= maxAccel)
                {
                    p1XaccelRate++;
                }
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerXSpeed + p1XaccelRate;
                player1Up.X -= playerXSpeed + p1XaccelRate;
                player1Down.X -= playerXSpeed + p1XaccelRate;
                player1Right.X -= playerXSpeed + p1XaccelRate;
                player1Left.X -= playerXSpeed + p1XaccelRate;

                accelTimer.Start();

                if (p1XaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p1XaccelRate <= maxAccel)
                {
                    p1XaccelRate++;
                }
            }


            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerYSpeed + p2YaccelRate;
                player2Up.Y -= playerYSpeed + p2YaccelRate;
                player2Down.Y -= playerYSpeed + p2YaccelRate;
                player2Right.Y -= playerYSpeed + p2YaccelRate;
                player2Left.Y -= playerYSpeed + p2YaccelRate;

                accelTimer.Start();

                if (p2YaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p2YaccelRate <= maxAccel)
                {
                    p2YaccelRate++;
                }
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerYSpeed + p2YaccelRate;
                player2Up.Y += playerYSpeed + p2YaccelRate;
                player2Down.Y += playerYSpeed + p2YaccelRate;
                player2Right.Y += playerYSpeed + p2YaccelRate;
                player2Left.Y += playerYSpeed + p2YaccelRate;

                accelTimer.Start();

                if (p2YaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p2YaccelRate <= maxAccel)
                {
                    p2YaccelRate++;
                }
            }

            if (rightArrowDown == true && player2.X < 590)
            {
                player2.X += playerXSpeed + p2XaccelRate;
                player2Up.X += playerXSpeed + p2XaccelRate;
                player2Down.X += playerXSpeed + p2XaccelRate;
                player2Right.X += playerXSpeed + p2XaccelRate;
                player2Left.X += playerXSpeed + p2XaccelRate;

                accelTimer.Start();

                if (p2XaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p2XaccelRate <= maxAccel)
                {
                    p2XaccelRate++;
                }
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerXSpeed + p2XaccelRate;
                player2Up.X -= playerXSpeed + p2XaccelRate;
                player2Down.X -= playerXSpeed + p2XaccelRate;
                player2Right.X -= playerXSpeed + p2XaccelRate;
                player2Left.X -= playerXSpeed + p2XaccelRate;

                accelTimer.Start();

                if (p2XaccelRate <= 10 && accelTimer.ElapsedMilliseconds % accelSpeed == 0 && p2XaccelRate <= maxAccel)
                {
                    p2XaccelRate++;
                }
            }

            //check if ball hits either player and which side. If it does change the direction 
            //andapplyspeed 

            //player1intersections
            if (player1Up.IntersectsWith(ball))
            {
                ballYSpeed += playerYSpeed + p1YaccelRate + 5;
                ballYSpeed *= -1;
                p1YaccelRate = 1;
            }

            if (player1Down.IntersectsWith(ball))
            {
                ballYSpeed += playerYSpeed + p1YaccelRate + 5;
                p1YaccelRate = 1;
            }

            if (player1Left.IntersectsWith(ball))
            {
                ballXSpeed += playerXSpeed + p1XaccelRate + 5;
                ballXSpeed *= -1;
                p1XaccelRate = 1;
            }

            if (player1Right.IntersectsWith(ball))
            {
                ballXSpeed += playerXSpeed + p1XaccelRate + 5;
                p1XaccelRate = 1;
            }

            if (player2.IntersectsWith(ball))
            {
                ballXSpeed += playerXSpeed + p2XaccelRate + 5;
                ballYSpeed += playerYSpeed + p2YaccelRate + 5;
                ballXSpeed *= -1;
                ballYSpeed *= -1;
            }


            //player2intersections
            if (player2Up.IntersectsWith(ball))
            {
                ballYSpeed += playerYSpeed + p2YaccelRate + 5;
                ballYSpeed *= -1;
                p2YaccelRate = 1;
            }

            if (player2Down.IntersectsWith(ball))
            {
                ballYSpeed += playerYSpeed + p2YaccelRate + 5;
                p2YaccelRate = 1;
            }

            if (player2Left.IntersectsWith(ball))
            {
                ballXSpeed += playerXSpeed + p2XaccelRate + 5;
                ballXSpeed *= -1;
                p2XaccelRate = 1;
            }

            if (player2Right.IntersectsWith(ball) && ballXSpeed < ballMaxSpeed)
            {
                ballXSpeed += playerXSpeed + p2XaccelRate + 5;
                p2XaccelRate = 1;
            }

            if (player2.IntersectsWith(ball))
            {
                ballXSpeed += playerXSpeed + p2XaccelRate + 5;
                ballYSpeed += playerYSpeed + p2YaccelRate + 5;
                ballXSpeed *= -1;
                ballYSpeed *= -1;
            }

            //check if ball hitsgoaland if true add 1 to score of other player  
            //if (ball.IntersectsWith(player1Goal))
            //{

            //    player2Score++;
            //    p2ScoreLabel.Text = $"Red: {player2Score}";

            //    ball.X = 300;
            //    ball.Y = 195;

            //    player1.Y = 170;
            //    player1Up.Y = 169;
            //    player1Down.Y = 230;
            //    player1Right.Y = 170;
            //    player1Left.Y = 170;

            //    player1.X = 20;
            //    player1Up.X = 20;
            //    player1Down.X = 20;
            //    player1Right.X = 30;
            //    player1Left.X = 20;

            //    player2.Y = 170;
            //    player2Up.Y = 170;
            //    player2Down.Y = 230;
            //    player2Right.Y = 170;
            //    player2Left.Y = 170;

            //    player2.X = 550;
            //    player2Up.X = 550;
            //    player2Down.X = 550;
            //    player2Right.X = 560;
            //    player2Left.X = 550;

            //    ballXSpeed = 0;
            //    ballYSpeed = 0;
            //}

            //if (ball.IntersectsWith(player2Goal))
            //{

            //    player1Score++;
            //    p1ScoreLabel.Text = $"Blue: {player1Score}";

            //    ball.X = 300;
            //    ball.Y = 195;

            //    player1.Y = 170;
            //    player1Up.Y = 169;
            //    player1Down.Y = 230;
            //    player1Right.Y = 170;
            //    player1Left.Y = 170;

            //    player1.X = 20;
            //    player1Up.X = 20;
            //    player1Down.X = 20;
            //    player1Right.X = 30;
            //    player1Left.X = 20;

            //    player2.Y = 170;
            //    player2Up.Y = 170;
            //    player2Down.Y = 230;
            //    player2Right.Y = 170;
            //    player2Left.Y = 170;

            //    player2.X = 550;
            //    player2Up.X = 550;
            //    player2Down.X = 550;
            //    player2Right.X = 560;
            //    player2Left.X = 550;

            //    ballXSpeed = 0;
            //    ballYSpeed = 0;
            //}

            // check score and stop game if either player is at 3 
            //if (player1Score == 3)
            //{
            //    gameTimer.Enabled = false;
            //    winLabel.Visible = true;
            //    winLabel.Text = "Blue  Wins!!";
            //}
            //else if (player2Score == 3)
            //{
            //    gameTimer.Enabled = false;
            //    winLabel.Visible = true;
            //    winLabel.Text = "Red  Wins!!";
            //}

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, player1Goal);
            e.Graphics.FillRectangle(whiteBrush, player2Goal);

            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);

            e.Graphics.FillRectangle(yellowBrush, ball);

            e.Graphics.DrawLine(whitePen, 300, 0, 300, 999);
            e.Graphics.FillRectangle(greenBrush, arenaTop);
            e.Graphics.FillRectangle(greenBrush, arenaBottom);
            e.Graphics.FillRectangle(greenBrush, arenaLeft);
            e.Graphics.FillRectangle(greenBrush, arenaRight);

            e.Graphics.FillRectangle(whiteBrush, player1Right);
            e.Graphics.FillRectangle(whiteBrush, player1Left);
            e.Graphics.FillRectangle(whiteBrush, player1Up);
            e.Graphics.FillRectangle(whiteBrush, player1Down);

            e.Graphics.FillRectangle(whiteBrush, player2Right);
            e.Graphics.FillRectangle(whiteBrush, player2Left);
            e.Graphics.FillRectangle(whiteBrush, player2Up);
            e.Graphics.FillRectangle(whiteBrush, player2Down);
        }
    }
}
