using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using System.Data;
//using System.IO;
//using System.Diagnostics;

namespace Diplomaster
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
            //MessageBox.Show(Process.EnableRaisingEvents.ToString());
            Global.DocSchema = SQL.LoadSchema("Договор");

            Application.Run(new Splash_Screen());
        }
    }
}