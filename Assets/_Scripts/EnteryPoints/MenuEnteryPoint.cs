using Assets._Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {

        private Func<MenuController> _menuFactory;

        public MenuEnteryPoint(Func<MenuController> menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Start()
        {
            InitMenu();
        }

        public void Dispose()
        {

        }

        public void InitMenu()
        {
            MenuController menu = _menuFactory();
        }
    }
}
