using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Cryptography.Xml;
using System.Windows.Forms.Design.Behavior;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int r1, r2;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label lableScore;
        private int dirX, dirY;
        private int _width = 800;
        private int _height = 800;
        private int _sizeOfSides = 40;
        private int score = 0;
        private string move = "Right";
        public Form1()
        {
            InitializeComponent();
            this.Width = _width + 200;
            this.Height = _height + 50;
            dirX = 1;
            dirY = 0;
            lableScore = new Label();
            lableScore.Text = "Score: 0";
            lableScore.Location = new Point(810, 10);
            this.Controls.Add(lableScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(200, 200);
            snake[0].Size = new Size(40, 40);
            snake[0].BackColor = Color.Red;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(40, 40);
            _generateMap();
            _generateFruit();
            _eatFruit();
            timer1.Tick += new EventHandler(_update);
            timer1.Interval = 50;
            timer1.Start();
            this.KeyDown += new KeyEventHandler(DKP);
        }

        private void _generateFruit()
        {
            Random r = new Random();
            r1 = r.Next(0, 760);
            int temp1 = r1 % 40;
            r1 -= temp1;
            r2 = r.Next(0, 760);
            int temp2 = r2 % 40;
            r2 -= temp2;
            for (int i = 0; i < score; i++)
            {
                if (snake[i].Location != new Point(r1, r2))
                {
                    fruit.Location = new Point(r1, r2);
                }
                else
                {
                    r1 = r.Next(0, 760);
                     temp1 = r1 % 40;
                    r1 -= temp1;
                    r2 = r.Next(0, 760);
                     temp2 = r2 % 40;
                    r2 -= temp2;
                }
            }
            fruit.Location = new Point(r1, r2);
            this.Controls.Add(fruit);
        }
        private void _eatFruit()
        {
            if (snake[0].Location.X == r1 && snake[0].Location.Y == r2)
            {
                lableScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].BackColor = Color.Red;
                snake[score].Size = new Size(40, 40);
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
        }
        private void _generateMap()
        {
            for (int i = 0; i <= _width / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i, 0);
                pic.Size = new Size(1, _width);
                this.Controls.Add(pic);
            }
        }
        private void _eatItselve()
        {
            for (int _i = 1; _i < score; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                    {
                        this.Controls.Remove(snake[_j]);
                    }
                    score = score - (score - _i + 2);
                    lableScore.Text = "Score: " + ++score;

                    break;

                }
            }

        }
        private void checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                dirX = 1;
                dirY = 0;
                move = "Right";
                lableScore.Text = "Score: " + score;

            }
            if (snake[0].Location.X > 760)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                dirX = -1;
                dirY = 0;
                lableScore.Text = "Score: " + score;
                move = "Left";
            }
            if (snake[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                dirX = 0;
                dirY = 1;
                lableScore.Text = "Score: " + score;
                move = "Down";
            }
            if (snake[0].Location.Y > 760)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                dirX = 0;
                dirY = -1;
                lableScore.Text = "Score: " + score;
                move = "Up";
            }
        }
        //private void speedCheck()
        //{
        //    switch (score)
        //    {
        //        case 0:
        //            timer1.Interval = 500;
        //            break;
        //        case 10:
        //            timer1.Interval = 450;
        //            break;
        //        case 20:
        //            timer1.Interval = 400;
        //            break;
        //        case 30:
        //            timer1.Interval = 350;
        //            break;
        //        case 40:
        //            timer1.Interval = 300;
        //            break;
        //        case 50:
        //            timer1.Interval = 250;
        //            break;
        //        case 60:
        //            timer1.Interval = 200;
        //            break;
        //        case 70:
        //            timer1.Interval = 150;
        //            break;
        //        case 80:
        //            timer1.Interval = 100;
        //            break;
        //        case 90:
        //            timer1.Interval = 50;
        //            break;
        //        case 100:
        //            DialogResult dialog= new DialogResult();
        //            timer1.Stop();
        //            dialog=MessageBox.Show("You win!!","Do you whant to start again?",MessageBoxButtons.YesNo);
        //            if(dialog==DialogResult.Yes)
        //            {
        //                for(int i = 1 ; i <= score; i++) 
        //                {
        //                    this.Controls.Remove(snake[i]);
        //                } 
        //                score= 0;
        //                timer1.Start();
        //            }
        //            else
        //            {
        //                this.Close();
        //            }
        //            break;
        //    }
        //}
        private void _moveSnake()
        {
            _eatItselve();
            checkBorders();

            for (int i = score; i > 0; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * 40, snake[0].Location.Y + dirY * 40);

        }
        private void _update(Object myObject, EventArgs eventArgs)
        {
            //speedCheck();
            _eatFruit();
            _moveSnake();
        }
        private void DKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":

                    if (move != "Right" && move != "Left")
                    {
                        dirX = 1;
                        dirY = 0;
                        move = "Right";
                    }

                    break;
                case "Left":
                    if (move != "Right" && move != "Left")
                    {
                        dirX = -1;
                        dirY = 0;
                        move = "Left";
                    }
                    break;
                case "Up":
                    if (move != "Up" && move != "Down")
                    {
                        dirX = -0;
                        dirY = -1;
                        move = "Up";
                    }
                    break;
                case "Down":
                    if (move != "Up" && move != "Down")
                    {
                        dirX = 0;
                        dirY = 1;
                        move = "Down";
                    }
                    break;
                case "Escape":
                    DialogResult dialog = new DialogResult();
                    timer1.Stop();
                    dialog = MessageBox.Show("You quit", "Do you whant to start again?", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        for (int i = 1; i <= score; i++)
                        {
                            this.Controls.Remove(snake[i]);
                        }
                        score = 0;
                        timer1.Start();
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}