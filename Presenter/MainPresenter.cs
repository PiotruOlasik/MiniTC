using MiniTC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTC.Presenter
{
    public class MainPresenter
    {
        private View.PanelTC _view1;
        private View.PanelTC _view2;

        public MainPresenter(View.PanelTC view1, View.PanelTC view2)
        {
            _view1 = view1;
            _view2 = view2;
            // _model = model; // Zakomentowane - model nie jest jeszcze zaimplementowany
        }
    }
}