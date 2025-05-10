using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Частицы
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var formDifficulty = new FormDifficulty())
            {
                if (formDifficulty.ShowDialog() == DialogResult.OK)
                {
                    // Запускаем Form1 с выбранной сложностью
                    Application.Run(new Form1(formDifficulty.SelectedDifficulty));
                }
            }
        }
    }
}
