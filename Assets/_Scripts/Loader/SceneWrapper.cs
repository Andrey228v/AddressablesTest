using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string sceneName;
        public SceneType sceneType;
    }
}
