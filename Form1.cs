using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter.CanvasWidth = picDisplay.Width;
            emitter.CanvasHeight = picDisplay.Height;

            emitter.CreateParticle = () =>
            {
                var particle = new Particle(
                    rand.Next(20, picDisplay.Width - 20),
                    rand.Next(20, picDisplay.Height - 20))
                {
                    Radius = 10,
                    Life = 200
                };

                int type = rand.Next(10);
                if (type < 5) // 50% - желтые
                {
                    particle.ColorParticle = Color.Yellow;
                }
                else if (type < 9) // 40% - красные
                {
                    particle.ColorParticle = Color.Red;
                }
                else // 10% - зеленые (живут меньше)
                {
                    particle.ColorParticle = Color.LimeGreen;
                    particle.Life = 1000;
                }

                return particle;
            };

            snake = new Snake(picDisplay.Width / 2, picDisplay.Height / 2);

            Timer timer = new Timer();
            timer.Interval = 20;
            timer.Tick += timer1_Tick;
            timer.Start();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;
            
            if (snake.CheckBorderCollision(picDisplay.Width, picDisplay.Height) && isGameOver == false)
            {
                GameOver();
                return;
            }

            snake.SetTarget(picDisplay.PointToClient(Cursor.Position));
            snake.Move();

            // Генерация частиц раз в секунду
            if ((DateTime.Now - lastParticleTime).TotalSeconds >= 1)
            {
                emitter.EmitParticles(1);
                lastParticleTime = DateTime.Now;
            }

            emitter.UpdateState();

            // Проверка столкновений
            foreach (var p in emitter.particles.ToList())
            {
                if (snake.CheckCollision(p))
                {
                    emitter.particles.Remove(p);
                    snake.ProcessParticleCollision(p, emitter);
                    richTextBox1.Text = snake.Score.ToString();
                }
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
        private void GameOver()
        {
            isGameOver = true;
            timer1.Stop();
            MessageBox.Show("Игра окончена! Вы врезались в стену.\nВаш счет: " + snake.Score,
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
            timer1.Start();
        }
    }
}
