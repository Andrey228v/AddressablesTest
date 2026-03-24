using Assets._Scripts.EnteryPoints;
using Assets._Scripts.UI;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Assets._Scripts.Installers
{
    public class HUDInstaller : LifetimeScope
    {
        [SerializeField] private HudType1Controller _hudT1Prefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<HUDEnteryPointType1>();


            builder.RegisterFactory<HudType1Controller>(container => () =>
            {
                return container.Instantiate(_hudT1Prefab);
            }, Lifetime.Singleton);

        }
    }
}
