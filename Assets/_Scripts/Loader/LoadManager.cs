using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Assets._Scripts.Loader
{
    public class LoadManager : IDisposable
    {
        //List который содержит все загруженные сцены.
        private Dictionary<string, AsyncOperationHandle> _loadsScenes; // ???
        private LoadScreenView _loadScreenView;
        private AsyncOperationGroup _asyncOperationGroup;
        private IProgress<float> _progress;

        public LoadManager(LoadScreenView loadScreenView)
        {
            _loadsScenes = new Dictionary<string, AsyncOperationHandle>();
            _loadScreenView = loadScreenView;
            _asyncOperationGroup = new AsyncOperationGroup();
            _progress = _loadScreenView.CreateProgressReporter();
        }

        public void Dispose()
        {
            foreach (string key in _loadsScenes.Keys) 
            {
                var handle = _loadsScenes[key];

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }

            _loadsScenes.Clear();
        }

        public async UniTask LoadScene(SceneGroupHandle sceneGroup)
        {

            await ShowLoadScreen();
            await UnloadCurrentContent();
            
            await LoadNewContent(sceneGroup);
            PrepareToTransition(sceneGroup);
            await HideLoadScreen();

        }

        private async UniTask ShowLoadScreen()
        {
            _loadScreenView.Show();
        }

        private async UniTask UnloadCurrentContent()
        {
            List<string> keys = _loadsScenes.Keys.ToList(); // Копия. Оказывается напрямую удалять нельзя ......
            _loadScreenView.SetStatus("Выгрузка");

            foreach (string key in keys)
            {
                var handle = _loadsScenes[key];

                await Addressables.UnloadSceneAsync(handle).ToUniTask(_progress);

                //await Addressables.UnloadSceneAsync(handle).ToUniTask();
                _loadsScenes.Remove(key);

                //_asyncOperationGroup
            }

            _asyncOperationGroup.ReleaseAll();
            await Resources.UnloadUnusedAssets();
        }

        private async UniTask LoadNewContent(SceneGroupHandle sceneGroup)
        {

            foreach (SceneWrapper sceneWrapper in sceneGroup.scenesNames)
            {
                SceneReference scene = sceneWrapper.scene;
                AsyncOperationHandle handle = Addressables.LoadSceneAsync(scene.Address, LoadSceneMode.Additive);

                //var progress = Cysharp.Threading.Tasks.Progress.Create<float>(value =>
                //{
                //    // Этот код выполнится КАЖДЫЙ раз, когда вызовут progress.Report()
                //    _loadingBar.value = value;        // Обновить слайдер
                //    _loadingText.text = $"{value * 100:F0}%"; // Обновить текст
                //    Debug.Log($"Прогресс: {value * 100:F0}%"); // Лог
                //});

                _loadsScenes.Add(sceneWrapper.Name, handle);
                _asyncOperationGroup.Add(handle);

                _loadScreenView.SetStatus($"Загрузка сцены: {sceneWrapper.Name}");

                await UniTask.Delay(1000);
            }

            await _asyncOperationGroup.WhenAll(_progress);
            //await UniTask.WhenAll(loadTasks);

            // Дополнительная задержка для завершения внутренних процессов
            //await UniTask.Delay(2000);

            Debug.Log("1)_____WhenAll_______");

            //int a = 5;

            //await UniTask.WaitUntil(() =>
            //    _loadsScenes.Values.All(h => h.IsDone && h.Result.Scene.isLoaded && a > 6));

            //await UniTask.Delay(2000);

            Debug.Log("2)_____WAIT_UNTIL_______");
        }

        private void PrepareToTransition(SceneGroupHandle sceneGroup)
        {
            _loadScreenView.SetStatus($"Переход");
            string activeSceneName = sceneGroup.FindSceneNameByType(SceneType.Active);

            SceneReference sceneRef = sceneGroup.FindScene(activeSceneName).scene;
            Scene scene = SceneManager.GetSceneByName(sceneRef.Name);

            SceneManager.SetActiveScene(scene);

            Debug.Log("3)_____SetActiveScene_______");
        }

        private async UniTask HideLoadScreen()
        {
            _loadScreenView.Hide();

            Debug.Log("4)_____HideLoadScreen_______");
        }
    }
}
