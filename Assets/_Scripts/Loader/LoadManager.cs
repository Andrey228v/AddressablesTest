using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Assets._Scripts.Loader
{
    public class LoadManager : IDisposable
    {
        //List который содержит все загруженные сцены.
        private List<AsyncOperationHandle> _loadsScenes; // ???

        public void Dispose()
        {
            foreach (var handle in _loadsScenes) 
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }

            _loadsScenes.Clear();
        }

        public async void LoadScene(SceneGroupHandle sceneGroup)
        {

            await ShowLoadScreen();
            await UnloadCurrentContent();
            await LoadNewContent(sceneGroup);
            await PrepareToTransition();
            await HideLoadScreen();

        }

        private async UniTask ShowLoadScreen()
        {

        }

        private async UniTask UnloadCurrentContent()
        {
            foreach (var loadedSceneHandle in _loadsScenes)
            {

                var progress = Progress.Create<float>(p =>
                {
                    Debug.Log($"Прогресс выгрузки: {p * 100:F0}%");
                });

                await Addressables.UnloadSceneAsync(loadedSceneHandle).ToUniTask(progress);
                // фильтр по сценам, которые надо или не надо выгружать...
                // тут необходимо ожидать выгрузку сцены

                //await handle

                Addressables.Release(loadedSceneHandle); // ???
            }

            await Resources.UnloadUnusedAssets();

            System.GC.Collect(); // надо ли это - пока не знаю. Тестим...

            Debug.Log("Уровень полностью выгружен");

        }

        private async UniTask LoadNewContent(SceneGroupHandle sceneGroup)
        {
            foreach(var scene in sceneGroup.scenes)
            {
                //AsyncOperationHandle
                await Addressables.LoadSceneAsync(scene).ToUniTask();
            }

        }

        private async UniTask PrepareToTransition()
        {

        }

        private async UniTask HideLoadScreen()
        {

        }


    }
}
