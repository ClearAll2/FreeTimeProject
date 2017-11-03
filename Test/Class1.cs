using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNotepad
{
    public class Glob
    {
        public static string search;
        public static string replace;
        public static int length;
        public static int start;
        public static int index;

        public Glob()
        {
            search = "";
            replace = "";
            length = 0;
            start = 0;
            index = 0;
        }
    }
}
