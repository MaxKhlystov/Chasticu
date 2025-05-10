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
    public partial class FormDifficulty : Form
    {
        public string SelectedDifficulty { get; private set; } = "Easy";
        public FormDifficulty()
        {
            InitializeComponent();
        }
        private void btnEasy_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = "Easy";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = "Medium";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = "Hard";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormDifficulty_Load(object sender, EventArgs e)
        {

        }

        private void btnEasy_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnMedium_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnHard_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
