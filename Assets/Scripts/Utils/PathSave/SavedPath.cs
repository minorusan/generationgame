using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utils.PathSave
{
    [SerializableAttribute]
    public struct GenerationPath
    {
        public Color color;
        public Vector2[] path;
    }

    [SerializableAttribute]
    public struct LevelMaps
    {
        public string levelName;
        public GenerationPath[] maps;
    }

    [CreateAssetMenu(fileName = "Positions map", menuName = "Positions map")]
    public class SavedPath : ScriptableObject
    {

        public LevelMaps[] maps;
    }

}
