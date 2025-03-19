using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Частицы
{
    public partial class Form1 : Form
    {

        List<Particle> particles = new List<Particle>();
        private int MousePositionX = 0;
        private int MousePositionY = 0;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
        private void UpdateState()
        {
            //по клику взрывы как фейверк
            /*foreach (var particle in particles)
            {
                {
                    var directionInRadians = particle.Direction / 180 * Math.PI;
                    particle.X += (float)(particle.Speed * Math.Cos(directionInRadians));
                    particle.Y -= (float)(particle.Speed * Math.Sin(directionInRadians));
                }
            }*/
            foreach (var particle in particles)
            {
                particle.Life -= 1; 
                if (particle.Life < 0)
                {
                    particle.Life = 20 + Particle.rand.Next(100);
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                }
                else
                {
                    var directionInRadians = particle.Direction / 180 * Math.PI;
                    particle.X += (float)(particle.Speed * Math.Cos(directionInRadians));
                    particle.Y -= (float)(particle.Speed * Math.Sin(directionInRadians));
                }
            }
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < 500) 
                {
                    var particle = new ParticleColorful();
                    particle.FromColor = Color.Yellow;
                    particle.ToColor = Color.FromArgb(0, Color.Magenta);
                    particle.X = MousePositionX;
                    particle.Y = MousePositionY;
                    particles.Add(particle);
                }
                else break; 
            }
        }

        private void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                Render(g);
            }
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            MousePositionX = e.X;
            MousePositionY = e.Y;
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            //по клику взрывы как фейверк
            /*for (var i = 0; i < 500; ++i)
            {
                var particle = new ParticleColorful();
                particle.FromColor = Color.Yellow;
                particle.ToColor = Color.FromArgb(0, Color.Magenta);
                particle.X = MousePositionX;
                particle.Y = MousePositionY;
                particles.Add(particle);
            }*/
        }
    }
}
