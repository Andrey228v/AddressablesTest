using Assets._Scripts.EnteryPoints;
using Assets._Scripts.Loader;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BootInstaller : LifetimeScope
{
    [SerializeField] private List<SceneGroupHandle> _sceneGroupHandle;


    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<LoadManager>(Lifetime.Singleton);

        builder.RegisterEntryPoint<RootEnteryPoint>();   
    }


}
