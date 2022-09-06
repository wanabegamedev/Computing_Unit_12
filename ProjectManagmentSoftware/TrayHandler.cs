using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Forms;
namespace ProjectManagmentSoftware
{
    static public class TrayHandler
    {
        static NotifyIcon notifyIcon;


        static Window currentWindow;


        //creates try icon
        public static NotifyIcon CreateNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "hi";
            notifyIcon.BalloonTipText = "hi";
            notifyIcon.Icon = new System.Drawing.Icon("Icon.ico");
            notifyIcon.Visible = true;
            notifyIcon.Click += OnTrayClick;

            return notifyIcon;


        }


        //opens program and destroys tray icon
        public static void OnTrayClick(object sender, EventArgs e)
        {

            currentWindow.Show();
            notifyIcon.Dispose();

        }

        public static void GetWindow(Window window)
        {
            currentWindow = window;
        }
    }
}
