using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;

namespace WindowsFormsApplication4
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex m_Mutex;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            m_Mutex = new Mutex(true, "AutoShutdown");

            if (m_Mutex.WaitOne(0, false))
            {
                Application.Run(new Main(1));
            }
            else
            {
                RegistryKey r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\AS\\Data", true);
                if (r == null)
                {
                    Application.Run(new Main(1));
                }
                else
                {
                    if (r.GetValue("Kid") != null)
                    {
                        if ((int)r.GetValue("Kid") == 1)
                        {
                            if (MessageBox.Show("AutoRun Option is now active! \nAutoRun Option can not be used in this instance. \nDo you want to run?", "Did you know", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                Application.Run(new Main(2));
                            }
                            else
                            {
                                r.Close();
                                r.Dispose();
                                return;
                            }
                        }
                        else
                        {
                            Application.Run(new Main(1));
                        }
                    }
                    else
                    {
                        Application.Run(new Main(1));
                    }
                }
                r.Close();
                r.Dispose();
            }
            
        }
        
    }
}
