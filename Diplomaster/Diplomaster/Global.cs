using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Diplomaster
{
    class Global
    {
        public static Color ColorDefault = SystemColors.Window;
        public static Color ColorInactive = SystemColors.InactiveCaption;
        public static Color ColorReadOnly = SystemColors.Control;
        public static Color ColorError = Color.LightCoral;
        public static Color ColorTextNormal = SystemColors.WindowText;
        //public static Color ColorTextFile = Color.Red;
        public static DateTime MinDate = new DateTime(1753, 1, 1);
        public static string LoadNodeName = "<<LOAD DATA>>";
        //public static string ConnectionString = @"Data Source = ..\..\Database.sdf";
        //public static string ConnectionString = @"Data Source = ..\..\..\connection.udl";

        public static string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\самсунг\Documents\МГИУ\DIPLOM-git\DIPLOM\Diplomaster\Diplomaster\DataBase.mdf;Integrated Security=True";
        //public static string ConnectionString = @"Server=(LocalDB)\MSSQLLocalDB; Integrated Security=true ;AttachDbFileName=C:\Users\самсунг\Documents\МГИУ\DIPLOM-git\DIPLOM\Diplomaster\Diplomaster\DataBase.mdf";


        private static string lastdir = "C:";

        public static string LastDirectoryPath
        {
            get { return lastdir; }
            set { lastdir = value; }
        }

        //Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\самсунг\Documents\МГИУ\DIPLOM-git\DIPLOM\Diplomaster\Diplomaster\DataBase.mdf;Integrated Security=True
    }
}
