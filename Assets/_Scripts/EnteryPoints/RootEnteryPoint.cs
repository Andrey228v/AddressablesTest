using Assets._Scripts.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class RootEnteryPoint : IStartable
    {
        private LoadManager _loadManager;

        public RootEnteryPoint(LoadManager loadManager) 
        {
            _loadManager = loadManager;
        }

        public void Start()
        {
            
        }
    }
}
