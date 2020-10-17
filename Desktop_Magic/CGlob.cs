using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace DM
{
    public class CGlob 
    {
        public static int Speed;
        public static int Number;
        public static int Type;
        public static int Amount;
        public static int Size;
        public static bool Smooth;
        //public static bool Custom;
        public static string Path;
        public CGlob()
        {
            RegistryKey r = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data", true);
            //r = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ClearAll\\DesktopMagic\\Data");
            if (r.GetValue("DM3") == null)
            {
                Speed = 10;
            }
            else
                Speed = (int)r.GetValue("DM3");
            if (r.GetValue("DM4") == null)
            {
                Number = 5;
            }
            else
            {
                Number = (int)r.GetValue("DM4");
            }
            if (r.GetValue("DM5") == null)
            {
                Amount = 20;
            }
            else
            {
                Amount = (int)r.GetValue("DM5");
            }
            if (r.GetValue("DM6") == null)
            {
                Size = 50;
            }
            else
            {
                Size = (int)r.GetValue("DM6");
            }
            if (r.GetValue("DMT") == null)
            {
                Type = 2;
            }
            else
                Type = (int)r.GetValue("DMT");

            if (r.GetValue("CustomFile") == null)
            {
                //Custom = false;
                Path = "";
            }
            else
            {
                //Custom = true;
                Path = (string)r.GetValue("CustomFile");
            }
            //r.Close();
            if (r.GetValue("DM7") == null)
            {
                Smooth = false;
            }
            else
            {
                Smooth = true;
            }
        }
    }
}
