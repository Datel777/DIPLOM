using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;



namespace Diplomaster
{
    class Global
    {
        public static Color ColorDefault = SystemColors.Window;
        public static Color ColorInactive = SystemColors.InactiveCaption;
        public static Color ColorReadOnly = SystemColors.Control;
        public static Color ColorDefaultBackground = SystemColors.Control;
        public static Color ColorError = Color.LightCoral;
        public static Color ColorSelected = SystemColors.ActiveCaption;
        public static Color ColorTextNormal = SystemColors.WindowText;
        //public static Color ColorTextFile = Color.Red;
        public static DateTime MinDate = new DateTime(1753, 1, 1);
        //public static Font HidedNodeFont = new Font("Microsoft Sans Serif", 8.25, FontStyle.Bold);
        public static string LoadNodeName = "<<LOAD DATA>>";
        //public static string ConnectionString = @"Data Source = ..\..\Database.sdf";
        //public static string ConnectionString = @"Data Source = ..\..\..\connection.udl";
        public static string EmptyStageText = "Этап ----";
        public static string StageTextPrefix = "Этап №";

        public static string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\самсунг\Documents\МГИУ\DIPLOM-git\DIPLOM\Diplomaster\Diplomaster\DataBase.mdf;Integrated Security=True";
        //public static string ConnectionString = @"Server=(LocalDB)\MSSQLLocalDB; Integrated Security=true ;AttachDbFileName=C:\Users\самсунг\Documents\МГИУ\DIPLOM-git\DIPLOM\Diplomaster\Diplomaster\DataBase.mdf";


        private static string lastdir = "C:";
        private static string[] docschem;

        public static string LastDirectoryPath
        {
            get { return lastdir; }
            set { lastdir = value; }
        }

        public static string[] DocSchema
        {
            get { return docschem; }
            set { docschem = value; }
        }

        //Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\самсунг\Documents\МГИУ\DIPLOM-git\DIPLOM\Diplomaster\Diplomaster\DataBase.mdf;Integrated Security=True
    }
}
