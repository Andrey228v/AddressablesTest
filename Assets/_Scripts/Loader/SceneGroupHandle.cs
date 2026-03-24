using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Scripts.Loader
{
    [Serializable]
    public struct SceneGroupHandle
    {
        public string groupName;
        public List<SceneWrapper> scenesNames;

        public string FindSceneNameByType(SceneType sceneType)
        {
            // под вопросом
            return scenesNames.FirstOrDefault(scene => scene.type == sceneType).Name;
        }
    }
}
