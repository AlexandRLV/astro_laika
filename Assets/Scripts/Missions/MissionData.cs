using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "Mission Data")]
    public class MissionData : ScriptableObject
    {
        [SerializeField] public int RewardScoresForFullMission;
        [SerializeField] public MissionStage[] Stages;
    }
}