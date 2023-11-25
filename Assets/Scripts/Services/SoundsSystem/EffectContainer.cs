using System;
using UnityEngine;

namespace Services.SoundsSystem
{
    [Serializable]
    public class EffectContainer
    {
        [SerializeField] public SoundType type;
        [SerializeField] public AudioClip clip;
    }
}