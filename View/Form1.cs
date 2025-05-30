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

            var leftPresenter = new LeftTCPresenter(paneltc1);
            var rightPresenter= new LeftTCPresenter(paneltc2);
            //var model = new MainModel();

            // Set the presenters in the panels
            paneltc1.SetPresenter(leftPresenter);
            paneltc2.SetPresenter(rightPresenter);

            // Initialize the drives
            leftPresenter.ShowDisks();
            rightPresenter.ShowDisks();

            var mainPresenter = new MainPresenter(paneltc1, paneltc2);
        }
    }
}
