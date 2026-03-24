using Eflatun.SceneReference;
using System;

namespace Assets._Scripts.Loader
{
    public enum SceneType
    {
        Active,
        NoMain,
        Menu,
        HUD,
        Player,
    }

    [Serializable]
    public struct SceneWrapper
    {   
        
        public SceneReference scene;
        public string Name => scene.Name;
        public SceneType type;
    }
}
