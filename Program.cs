using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniTC.View;

namespace MiniTC
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //var leftView = new View.PanelTC();
            //var rightView = new View.PanelTC();
           // var presenter = new Presenter.MainPresenter(leftView, rightView);
            //Application.Run();
            Application.Run(new View.Form1());
        }
    }
}