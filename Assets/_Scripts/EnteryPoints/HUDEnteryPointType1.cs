using Assets._Scripts.UI;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class HUDEnteryPointType1 : IStartable, IDisposable
    {

        private Func<HudType1Controller> _HUDType1Factory;

        public HUDEnteryPointType1(Func<HudType1Controller> hudType1Factory)
        {
            _HUDType1Factory = hudType1Factory;
        }

        public void Start()
        {
            InitHUD();
        }

        public void Dispose()
        {

        }

        public void InitHUD()
        {
            HudType1Controller hud = _HUDType1Factory();
        }
    }
}
