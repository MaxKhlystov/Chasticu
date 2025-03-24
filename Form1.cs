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
        Emitter emitter;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };
            rectangles.Add(new Rectangle
            {
                X = 100,
                Y = 100,
                Width = 200,
                Height = 50,
                Color = Color.Red
            });

            rectangles.Add(new Rectangle
            {
                X = 300,
                Y = 300,
                Width = 150,
                Height = 100,
                Color = Color.Blue
            });

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
                    rect.Draw(g);
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
