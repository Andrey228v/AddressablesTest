using Assets._Scripts.Loader;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class RootEnteryPoint : IStartable
    {
        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        public RootEnteryPoint(LoadManager loadManager, List<SceneGroupHandle> scensGroups) 
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
        }

        public async void Start()
        {
            await _loadManager.LoadScene(_scensGroups[0]);
        }
    }
}
