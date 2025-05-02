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
        List<Rectangle> rectangles = new List<Rectangle>();
        List<Emitter> emitters = new List<Emitter>();
        Rectangle rectangle1, rectangle2, rectangle3;
        Emitter emitter = new Emitter();
        private Human player;
        private DateTime lastParticleTime;
        private bool wPressed, sPressed, aPressed, dPressed;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter.CanvasWidth = picDisplay.Width;
            emitter.CanvasHeight = picDisplay.Height;
            rectangle1 = new Rectangle(picDisplay.Width/1.5f, picDisplay.Height/1.2f, 0);
            rectangle2 = new Rectangle(picDisplay.Width/1.5f, picDisplay.Height/3, 0);
            rectangle3 = new Rectangle(picDisplay.Width / 4, picDisplay.Height / 2, 0);
            emitter.CreateParticle = () =>
            {
                return new Particle(
                    rand.Next(picDisplay.Width),
                    rand.Next(picDisplay.Height))
                {
                    ColorParticle = rand.Next(2) == 0 ? Color.Yellow : Color.Red
                };
            };
            player = new Human(picDisplay.Width / 2, picDisplay.Height / 2);
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            rectangles.Add(rectangle1);
            rectangles.Add(rectangle2);
            rectangles.Add(rectangle3);
            emitter.Rectangles.AddRange(rectangles);
            emitters.Add(this.emitter);
            Timer timer = new Timer();
            timer.Interval = 20; // интервал в миллисекундах, например, 20мс -> 50 кадров в секунду
            timer.Tick += timer1_Tick; // связываем с обработчиком события
            timer.Start();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: wPressed = true; break;
                case Keys.S: sPressed = true; break;
                case Keys.A: aPressed = true; break;
                case Keys.D: dPressed = true; break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: wPressed = false; break;
                case Keys.S: sPressed = false; break;
                case Keys.A: aPressed = false; break;
                case Keys.D: dPressed = false; break;
            }
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float dx = 0;
            float dy = 0;

            if (wPressed) dy -= 1; // Вверх
            if (sPressed) dy += 1; // Вниз
            if (aPressed) dx -= 1; // Влево
            if (dPressed) dx += 1; // Вправо
            player.Move(dx, dy);

            emitter.X = (int)player.X; // Координата X эмиттера - центр игрока
            emitter.Y = (int)player.Y;

            // Генерация частиц раз в 5 секунд
            if ((DateTime.Now - lastParticleTime).TotalSeconds >= 5)
            {
                emitter.EmitParticles(1);
                lastParticleTime = DateTime.Now;
            }

            emitter.UpdateState();

            // Проверка столкновений
            foreach (var p in emitter.particles.ToList())
            {
                if (p.ColorParticle == Color.Red &&
                    !player.IsInProtectionZone(p.X, p.Y) &&
                    Math.Abs(p.X - player.X) < 30 &&
                    Math.Abs(p.Y - player.Y) < 70)
                {
                    player.TakeDamage(10);
                    emitter.particles.Remove(p);
                }
            }

            // Отрисовка
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
                player.Render(g);
            }
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
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
    }
}
