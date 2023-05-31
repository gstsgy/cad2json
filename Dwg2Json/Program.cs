using Dwg2Json.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dwg2Json
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new FormMain());
           
            //Application.Run(new Form2(new JsonPolyline()));
        }
    }
}
