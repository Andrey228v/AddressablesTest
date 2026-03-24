using Cysharp.Threading.Tasks;
using Eflatun.SceneReference;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


namespace Assets._Scripts.Loader
{
    public class LoadManager : IDisposable
    {
        //List который содержит все загруженные сцены.
        private Dictionary<string, AsyncOperationHandle> _loadsScenes; // ???
        private LoadScreenView _loadScreenView;

        public LoadManager(LoadScreenView loadScreenView)
        {
            _loadsScenes = new Dictionary<string, AsyncOperationHandle>();
            _loadScreenView = loadScreenView;
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
            await PrepareToTransition();
            await HideLoadScreen();

        }

        private async UniTask ShowLoadScreen()
        {
            _loadScreenView.Show();
        }

        private async UniTask UnloadCurrentContent()
        {
            foreach (string key in _loadsScenes.Keys)
            {

                var handle = _loadsScenes[key];

                var progress = Progress.Create<float>(p =>
                {
                    Debug.Log($"Прогресс выгрузки: {p * 100:F0}%");
                });

                await Addressables.UnloadSceneAsync(handle).ToUniTask(progress);
                // фильтр по сценам, которые надо или не надо выгружать...
                // тут необходимо ожидать выгрузку сцены

                //await handle

                //Addressables.Release(handle); // ??? ошибка вылетает из-за того что уничтожает тоже самое ...
            }

            await Resources.UnloadUnusedAssets();

            //System.GC.Collect(); // надо ли это - пока не знаю. Тестим...

            Debug.Log("Уровень полностью выгружен");

        }

        private async UniTask LoadNewContent(SceneGroupHandle sceneGroup)
        {
            foreach(SceneWrapper sceneWrapper in sceneGroup.scenesNames)
            {
                SceneReference scene = sceneWrapper.scene;
                AsyncOperationHandle handle = Addressables.LoadSceneAsync(scene.Address, LoadSceneMode.Additive);

                await handle.ToUniTask();

                _loadsScenes.Add(sceneWrapper.Name, handle);
            }
        }

        private async UniTask PrepareToTransition()
        {

        }

        private async UniTask HideLoadScreen()
        {
            _loadScreenView.Hide();
        }
    }
}
