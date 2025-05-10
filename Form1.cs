using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Частицы.Emitter;

namespace Частицы
{
    public partial class Form1 : Form
    {
        Emitter emitter = new Emitter();
        private Snake snake;
        private DateTime lastParticleTime;
        Random rand = new Random();
        private bool isGameOver = false;
        public enum GameDifficulty { Easy, Medium, Hard }
        private GameDifficulty currentDifficulty;

        // Исправлено: Добавляем сохранение сложности в свойство
        public string Difficulty { get; private set; }
        private bool wPressed, aPressed, sPressed, dPressed; // Добавлено: Для управления в Hard режиме
        private float baseSpeed = 3f;
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        public Form1(string difficulty)
        {
            Difficulty = difficulty;

            // Исправленный вариант инициализации currentDifficulty
            if (difficulty == "Medium")
                currentDifficulty = GameDifficulty.Medium;
            else if (difficulty == "Hard")
                currentDifficulty = GameDifficulty.Hard;
            else
                currentDifficulty = GameDifficulty.Easy;
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter.CanvasWidth = picDisplay.Width;
            emitter.CanvasHeight = picDisplay.Height;
            baseSpeed = 3f;

            emitter.CreateParticle = () =>
            {
                var particle = new Particle(
                    rand.Next(20, picDisplay.Width - 20),
                    rand.Next(20, picDisplay.Height - 20))
                {
                    Radius = 10,
                    Life = 200
                };

                int type = rand.Next(21);
                if (type < 4) 
                {
                    particle.ColorParticle = Color.Yellow;
                }
                else if (type < 8) 
                {
                    particle.ColorParticle = Color.Red;
                }
                else if (type <12)
                {
                    particle.ColorParticle = Color.LimeGreen;
                }
                else if (type < 16)
                {
                    particle.ColorParticle = Color.Cyan;
                }
                else 
                {
                    particle.ColorParticle = Color.Coral;
                    particle.Life = 50;
                }

                return particle;
            };

            snake = new Snake(picDisplay.Width / 2, picDisplay.Height / 2);
            switch (Difficulty) // Добавлено: Устанавливаем скорость в зависимости от сложности
            {
                case "Medium":
                    baseSpeed = 4.5f; // Добавлено: чуть быстрее, чем лёгкий
                    break;
                case "Hard":
                    baseSpeed = 3f; // Добавлено: скорость как в легком, изначально
                    snake.Speed = baseSpeed; // Добавлено: устанавливаем начальную скорость
                    break;
            }
            snake.Speed = baseSpeed;
            Timer timer = new Timer();
            timer.Interval = 20;
            timer.Tick += timer1_Tick;
            timer.Start();
            if (currentDifficulty == GameDifficulty.Hard)
            {
                this.KeyPreview = true;
                this.KeyDown += Form1_KeyDown;
                this.KeyUp += Form1_KeyUp;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (currentDifficulty != GameDifficulty.Hard && keyData == Keys.Space && !isGameOver)
            {
                BoostSnake();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BoostSnake()
        {
            if (snake.Score >= 5 && snake.Body.Count > 5) // Проверяем, что очков и сегментов достаточно
            {
                snake.Score -= 5;
                richTextBox1.Text = snake.Score.ToString();

                // Уменьшаем змейку на 5 сегментов
                for (int i = 0; i < 5 && snake.Body.Count > 1; i++)
                {
                    snake.RemoveTailSegment();
                }

                snake.Speed = snake.Speed * 2;
                snake.IsBoosted = true;

                // Запускаем таймер для отключения ускорения через 3 секунды
                Timer boostTimer = new Timer();
                boostTimer.Interval = 3000;
                boostTimer.Tick += (s, e) =>
                {
                    snake.Speed = baseSpeed;
                    snake.IsBoosted = false;
                    boostTimer.Stop();
                    boostTimer.Dispose();
                };
                boostTimer.Start();
            }
            else
            {
                string message = snake.Score < 5 ?
                    "Недостаточно очков для ускорения!" :
                    "Змейка слишком коротка для ускорения!";

                MessageBox.Show(message, "Внимание",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentDifficulty == GameDifficulty.Hard)
            {
                switch (e.KeyCode)
                {
                    case Keys.W: wPressed = true; break;
                    case Keys.S: sPressed = true; break;
                    case Keys.A: aPressed = true; break;
                    case Keys.D: dPressed = true; break;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentDifficulty == GameDifficulty.Hard)
            {
                switch (e.KeyCode)
                {
                    case Keys.W: wPressed = false; break;
                    case Keys.S: sPressed = false; break;
                    case Keys.A: aPressed = false; break;
                    case Keys.D: dPressed = false; break;
                }
            }
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            string text = "Игра окончена! ";
            bool selfCollision = false;

            if (currentDifficulty == GameDifficulty.Hard)
            {
                // Управление WASD
                float dx = 0, dy = 0;
                if (wPressed) dy = -1;
                if (sPressed) dy = 1;
                if (aPressed) dx = -1;
                if (dPressed) dx = 1;

                if (dx != 0 || dy != 0)
                {
                    snake.SetTarget(new PointF(
                        snake.Body[0].X + dx * snake.Speed,
                        snake.Body[0].Y + dy * snake.Speed
                    ));
                }
            }
            else
            {
                // Обычное управление мышкой
                snake.SetTarget(picDisplay.PointToClient(Cursor.Position));
            }

            // Добавлено: Проверка столкновений в зависимости от уровня
            if (currentDifficulty == GameDifficulty.Medium && snake.CheckSelfCollision())
            {
                GameOver("Игра окончена! Вы столкнулись с собой.\nВаш счет: ");
                return;
            }
            else if (currentDifficulty == GameDifficulty.Hard && snake.CheckSelfCollision())
            {
                GameOver("Игра окончена! Вы столкнулись с собой.\nВаш счет: ");
                return;
            }

            if (snake.CheckBorderCollision(picDisplay.Width, picDisplay.Height))
            {
                text += "Вы врезались в стену.\nВаш счет: ";
                GameOver(text);
                return;
            }

            snake.Move(); //  Перемещаем змейку после обновления направления

            // Генерация частиц раз в секунду
            if ((DateTime.Now - lastParticleTime).TotalMilliseconds >= 500)
            {
                emitter.EmitParticles(1);
                lastParticleTime = DateTime.Now;
            }

            emitter.UpdateState(snake);

            // Проверка столкновений
            foreach (var p in emitter.particles.ToList())
            {
                if (snake.CheckCollision(p))
                {
                    emitter.particles.Remove(p);
                    snake.ProcessParticleCollision(p, emitter);
                    richTextBox1.Text = snake.Score.ToString();

                    if (p.ColorParticle == Color.Red)
                    {
                        text += "Вы съели грязное яблоко.\nВаш счет: ";
                        GameOver(text);
                        return;
                    }
                }
            }

            if (snake.Body.Count < 5)
            {
                text += "Змейка слишком коротка, чтобы выжить! \nВаш счет: ";
                GameOver(text);
                return;
            }

            if (snake.Score >= 100 && isGameOver == false)
            {
                text += "Вы победили и стали обладателем самой большой змеи! \nВаш счет: ";
                GameOver(text);
                return;
            }

            // Отрисовка
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
                snake.Render(g);
            }
            picDisplay.Invalidate();
        }
        private void GameOver(string text)
        {
            isGameOver = true;
            timer1.Stop();
            MessageBox.Show(text + snake.Score,
                           "Game Over",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation);
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbDirection1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void butRestart_Click(object sender, EventArgs e)
        {
            isGameOver = false;
            snake = new Snake(picDisplay.Width / 2, picDisplay.Height / 2);
            emitter.particles.Clear();
            richTextBox1.Text = "0";
            baseSpeed = 3f;
            if (Difficulty == "Medium")
            {
                baseSpeed = 4.5f;
            }
            else if (Difficulty == "Hard")
            {
                baseSpeed = 3f;
            }
            snake.Speed = baseSpeed;
            timer1.Start();
        }
    }
}
