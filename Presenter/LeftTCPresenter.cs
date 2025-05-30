using MiniTC.Model;
using MiniTC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTC.Presenter
{
    public class LeftTCPresenter
    {
        private  View.PanelTC _view;
        private  DiskOperations _diskOps = new DiskOperations();
        
        public LeftTCPresenter(View.PanelTC view)
        {
            _view = view;

        }

        public void ShowDisks()
        {
            var disks = _diskOps.GetDisks();
            _view.SetDisks(disks);
        }

        public void ShowDirectory(string path)
        {
            var directory = _diskOps.GetDirectory(path);
            _view.SetDirectories(path);
        }

        public void OnDriveChanged(string selectedDrive)
        {
            if (!string.IsNullOrEmpty(selectedDrive))
            {
              
            }
        }
    }
}
