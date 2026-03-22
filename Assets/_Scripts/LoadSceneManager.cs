using Assets._Scripts.Enemy;
using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Assets._Scripts
{

    public class LoadSceneManager : MonoBehaviour
    {
        [SerializeField] private AssetReference _playerPrefab;
        [SerializeField] private AssetReference _enemyPrefab;
        [SerializeField] private SceneReference _level_1;
        [SerializeField] private SceneReference _level2;
        

        private AsyncOperationHandle _handle;
        private Scene _activeScene;

        private async void Start()
        {
            AsyncOperationHandle h1 = Addressables.LoadSceneAsync(_level_1.Name, LoadSceneMode.Additive);
            _activeScene = SceneManager.GetSceneByName(_level_1.Name);

            if (h1.IsDone)
            {
                SceneManager.SetActiveScene(_activeScene);
            }

            await PlayerInit();
            await EnemyInit();

            await UniTask.Delay(1000);

            AsyncOperationHandle h2 =  Addressables.LoadSceneAsync(_level2.Name, LoadSceneMode.Additive);

            AsyncOperationHandle h3 = Addressables.UnloadSceneAsync(h1);

            _activeScene = SceneManager.GetSceneByName(_level2.Name);

            if (h2.IsDone)
            {
                SceneManager.SetActiveScene(_activeScene);
            }

            //Debug.Log(_playerPrefab.IsValid());
            //if (_playerPrefab.IsValid())
            //{
            //    _playerPrefab.ReleaseAsset();
            //}

            //if (_playerPrefab.IsValid())
            //{
            //    _enemyPrefab.ReleaseAsset();
            //}
        }

        private void OnDestroy()
        {
            if (_playerPrefab.IsValid())
            {
                _playerPrefab.ReleaseAsset();
            }

            if (_playerPrefab.IsValid())
            {
                _enemyPrefab.ReleaseAsset();
            }
        }

        private async UniTask PlayerInit()
        {
            _handle = _playerPrefab.LoadAssetAsync<GameObject>();

            await _handle.Task;

            Debug.Log(_handle.Result);
            Debug.Log(_handle.Result.ToString());

            GameObject player = Instantiate((GameObject)_handle.Result, transform);

            await UniTask.Delay(1000);
        }

        private async UniTask EnemyInit()
        {
            GameObject enemyObj = await _enemyPrefab.InstantiateAsync().Task;
        }
    }
}
