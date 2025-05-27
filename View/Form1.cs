using MiniTC.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTC.View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var leftPanelTC = new PanelTC();
            var rightPanelTC = new PanelTC();
            //var model = new MainModel();

            this.Controls.Add(leftPanelTC);
            this.Controls.Add(rightPanelTC);

            var presenter = new MainPresenter(leftPanelTC, rightPanelTC);
        }
    }
}
