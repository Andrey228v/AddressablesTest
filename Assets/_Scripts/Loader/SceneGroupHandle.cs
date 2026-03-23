using System;
using System.Collections.Generic;

namespace Assets._Scripts.Loader
{
    [Serializable]
    public struct SceneGroupHandle
    {
        public string groupName;
        public List<SceneWrapper> scenesNames;
    }
}
