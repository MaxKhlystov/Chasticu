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
        Emitter emitter;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            rectangle1 = new Rectangle(picDisplay.Width/2, picDisplay.Height/4, 0);
            rectangle2 = new Rectangle(picDisplay.Width/4, picDisplay.Height/2, 0);
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
                var p = new Particle(emitter.X, emitter.Y, 0)  
                {
                    ColorParticle = emitter.ColorFrom
                };

                p.OnRectangleOverlap = (particle) =>  
                {
                    particle.ColorParticle = Color.Red;
                    particle.Life = 0;
                };

                return p;  
            };
            rectangles.Add(rectangle1);
            rectangles.Add(rectangle2);
            emitter.Rectangles.AddRange(rectangles);
            emitters.Add(this.emitter);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                foreach (var rect in rectangles)
                {
                    rect.Render(g);
                }
                emitter.Render(g);
            }
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
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
