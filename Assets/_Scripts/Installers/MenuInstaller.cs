using Assets._Scripts.EnteryPoints;
using Assets._Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Scripts.Installers
{
    public class MenuInstaller : LifetimeScope
    {
        [SerializeField] private MenuController _menuController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEnteryPoint>();

            builder.RegisterFactory<MenuController>(container => () =>
            {
                return container.Instantiate(_menuController);
            }, Lifetime.Singleton);
        }
    }
}
