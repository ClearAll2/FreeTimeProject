using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DM
{
    static class Program
    {
        //[DllImport("Shcore.dll")]
        //static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);

        // According to https://msdn.microsoft.com/en-us/library/windows/desktop/dn280512(v=vs.85).aspx
        //private enum DpiAwareness
        //{
        //    None = 0,
        //    SystemAware = 1,
        //    PerMonitorAware = 2
        //}
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex m_Mutex;
        [STAThread]
        static void Main()
        {
            m_Mutex = new Mutex(true, "DesktopMagic");

            if (m_Mutex.WaitOne(0, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //SetProcessDpiAwareness((int)DpiAwareness.SystemAware);
                Application.Run(new MainForm());
                m_Mutex.ReleaseMutex();
            }
            else
            {
                //MessageBox.Show("Desktop Magic is already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NativeMethods.PostMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_DMSHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
                
                return;
            }

        }
        
        
    }
}
