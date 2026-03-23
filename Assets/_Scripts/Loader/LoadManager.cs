using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._Scripts.Loader
{
    public class LoadManager
    {
        //List который содержит все загруженные сцены.
        private List<AsyncOperationHandle> _loadsScenes; // ???

        public async void LoadScene(SceneGroupHandle sceneGroup)
        {

            await ShowLoadScreen();
            await UnloadCurrentContent();
            await LoadNewContent();
            await PrepareToTransition();
            await HideLoadScreen();

        }

        public async UniTask ShowLoadScreen()
        {

        }

        public async UniTask UnloadCurrentContent()
        {
            foreach (var loadedScene in _loadsScenes)
            {
                AsyncOperationHandle handle =  Addressables.UnloadSceneAsync(loadedScene);

                await handle.ToUniTask();
                // фильтр по сценам, которые надо или не надо выгружать...
                // тут необходимо ожидать выгрузку сцены

                //await handle

                // освободить память...
            }
        }

        public async UniTask LoadNewContent()
        {

        }

        public async UniTask PrepareToTransition()
        {

        }

        public async UniTask HideLoadScreen()
        {

        }

    }
}
