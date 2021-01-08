using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM_Updater
{
    static class Program
    {

        [DllImport("Shcore.dll")]
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);

        // According to https://msdn.microsoft.com/en-us/library/windows/desktop/dn280512(v=vs.85).aspx
        private enum DpiAwareness
        {
            None = 0,
            SystemAware = 1,
            PerMonitorAware = 2
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static Mutex m_Mutex;
        private static String _fileName = "";
        public static String FileName
        {
            set
            {
                _fileName = value;
            }
            get
            {
                return _fileName;
            }
        }
        [STAThread]
        static void Main(String[] args)
        {
            m_Mutex = new Mutex(true, "DMUpdater");
            if (args.Length > 0)
            {
                _fileName = args[0];
            }
            if (m_Mutex.WaitOne(0, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SetProcessDpiAwareness((int)DpiAwareness.SystemAware);
                Application.Run(new Updater());
            }
            else
            {
                //MessageBox.Show("DM Updater is already running!", "Hey man!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
