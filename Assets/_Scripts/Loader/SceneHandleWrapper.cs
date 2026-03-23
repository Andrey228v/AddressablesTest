using Eflatun.SceneReference;
using System;

namespace Assets._Scripts.Loader
{
    [Serializable]
    public struct SceneHandleWrapper
    {
        public string _sceneName; // Надо ли это ? ....
        public SceneReference _scene;

    }
}
