using UnityEngine;

namespace Services.SoundsSystem
{
    [CreateAssetMenu(fileName = "Sounds Data")]
    public class SoundsData : ScriptableObject
    {
        [SerializeField] public EffectContainer[] effectClips;
        [SerializeField] public MusicContainer[] musicClips;
    }
}