using Assets._Scripts.Loader;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _startGameButtonL1;
        [SerializeField] private Button _startGameButtonL2;

        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        [Inject]
        public void Constructor(LoadManager loadManager, List<SceneGroupHandle> scensGroups)
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
        }

        private void Start()
        {
            _startGameButtonL1.onClick.AddListener(() => StartGame(1));
            _startGameButtonL2.onClick.AddListener(() => StartGame(2));
        }

        private void OnDisable()
        {
            _startGameButtonL1.onClick.RemoveAllListeners();
            _startGameButtonL2.onClick.RemoveAllListeners();
        }

        private async void StartGame(int level)
        {
            Hide();

            if (level == 1)
            {
                await _loadManager.LoadScene(_scensGroups[1]);
            }
            else if (level == 2)
            {
                await _loadManager.LoadScene(_scensGroups[2]);
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
