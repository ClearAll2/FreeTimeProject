﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DM
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
            m_Mutex = new Mutex(true, "DesktopMagic");

            if (m_Mutex.WaitOne(0, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
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
