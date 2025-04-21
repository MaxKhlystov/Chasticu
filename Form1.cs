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
        Rectangle rectangle1;
        Rectangle rectangle2;
        Rectangle rectangle3;
        Emitter emitter;
        private Tank playerTank;
        private bool wPressed, sPressed, aPressed, dPressed;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            rectangle1 = new Rectangle(picDisplay.Width/1.5f, picDisplay.Height/1.2f, 0);
            rectangle2 = new Rectangle(picDisplay.Width/1.5f, picDisplay.Height/3, 0);
            rectangle3 = new Rectangle(picDisplay.Width / 4, picDisplay.Height / 2, 0);
            this.emitter = new Emitter 
            {
                Direction = 0,
                Spreading = 10,
                Speed = 10,
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };
            Action<Particle> onCollision = (particle) =>
            {
                particle.ColorParticle = Color.Red; 
                particle.Life = 0; 
            };
            this.emitter.CreateParticle = () =>
            {
                var gunPos = playerTank.GetGunPosition();
                var p = new Particle(gunPos.X, gunPos.Y, playerTank.Angle)
                {
                    ColorParticle = Color.Orange
                };

                // Направление частицы совпадает с направлением дула
                float speed = 15f;
                p.SpeedX = (float)Math.Cos(playerTank.Angle * Math.PI / 180) * speed;
                p.SpeedY = (float)Math.Sin(playerTank.Angle * Math.PI / 180) * speed;

                p.OnRectangleOverlap = (particle) =>
                {
                    p.Life = 0;
                };
                return p;
            };
            playerTank = new Tank(picDisplay.Width / 2, picDisplay.Height / 2, 0)
            {
                Speed = 3f
            };
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            rectangles.Add(rectangle1);
            rectangles.Add(rectangle2);
            rectangles.Add(rectangle3);
            emitter.Rectangles.AddRange(rectangles);
            emitters.Add(this.emitter);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: wPressed = true; break;
                case Keys.S: sPressed = true; break;
                case Keys.A: aPressed = true; break;
                case Keys.D: dPressed = true; break;
                case Keys.Space: // Выстрел по пробелу
                    emitter.EmitParticles(3); // Выпускаем 3 частицы за раз
                    break;
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
            // Управление танком
            if (wPressed) playerTank.MoveForward();
            if (sPressed) playerTank.MoveBackward();
            if (aPressed) playerTank.RotateLeft();
            if (dPressed) playerTank.RotateRight();

            // Обновление частиц
            emitter.UpdateState();

            // Проверка столкновений танка с частицами
            foreach (var particle in emitter.particles.ToList())
            {
                if (playerTank.Overlaps(particle, null))
                {
                    playerTank.TakeDamage(10);
                    particle.Life = 0;
                }
            }

            // Отрисовка
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);

                // Отрисовка прямоугольников
                foreach (var rect in rectangles)
                {
                    rect.Render(g);
                }

                // Отрисовка частиц
                emitter.Render(g);

                // Отрисовка танка
                playerTank.Render(g);
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            playerTank.AimAt(e.X, e.Y);

            emitter.TargetX = e.X;
            emitter.TargetY = e.Y;
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            emitter.EmitParticles(1);
            emitter.TargetX = e.X;
            emitter.TargetY = e.Y;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbDirection1_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbDirection1.Value;
            lblDirection1.Text = $"{tbDirection1.Value}°";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
