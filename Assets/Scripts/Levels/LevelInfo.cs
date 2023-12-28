using System;
using Missions;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public class LevelInfo
    {
        [SerializeField] public int Id;
        [SerializeField] public string LevelName;
        [SerializeField] public string SceneToLoad;
        [SerializeField] public AudioClip LevelMusic;
        [SerializeField] public MissionData LevelMission;
    }
}