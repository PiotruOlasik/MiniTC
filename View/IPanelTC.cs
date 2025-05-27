using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.View
{
    internal interface IPanelTC
    {
        string currentPath { get;}    
        List<string> currentPathContent { get;}
        
        List<string> drives { get;} 

        void SetDisks(List<string> drives);      
        

    }
}
