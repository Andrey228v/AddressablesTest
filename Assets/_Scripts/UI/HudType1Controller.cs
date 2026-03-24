using Assets._Scripts.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI
{
    public class HudType1Controller : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        [Inject]
        public void Constructor(LoadManager loadManager, List<SceneGroupHandle> scensGroups)
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
        }


    }
}
