using UnityEngine;

namespace Missions
{
    public class MissionData : ScriptableObject
    {
        [SerializeField] public int RewardScoresForFullMission;
        [SerializeField] public MissionStage[] Stages;
    }
}