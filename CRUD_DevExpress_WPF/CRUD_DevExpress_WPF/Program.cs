using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CRUD_DevExpress_WPF
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VBSQLHelper.SQLHelper.SERVER_NAME = "DESKTOP-FDFMV04";
            VBSQLHelper.SQLHelper.DATABASE = "CRUD_STUDENT";
            VBSQLHelper.SQLHelper.USERNAME_DB = "sa";
            VBSQLHelper.SQLHelper.PASSWORD_DB = "aziz1234";
            VBSQLHelper.SQLHelper.ConnectString(); // nhớ hàm này nhé.
            Application.Run(new Form1());
        }
    }
}
