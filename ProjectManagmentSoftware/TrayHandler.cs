using System;
using System.Windows;
using System.Windows.Forms;
namespace ProjectManagmentSoftware
{
    static public class TrayHandler
    {
        static NotifyIcon notifyIcon;


        static Window currentWindow;

        public static NotifyIcon CreateNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = Project.projectName;
            //BalloonTip wont work on newer versions of Windows
            notifyIcon.BalloonTipText = "hi";
            try
            {
                notifyIcon.Icon = new System.Drawing.Icon("Icon.ico");

            }
            catch (Exception ex)
            {
                return null;
            }
            notifyIcon.Visible = true;
            //allows application to be reopened
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
