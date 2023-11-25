using System;
using UnityEngine;

namespace Services.SoundsSystem
{
    [Serializable]
    public class MusicContainer
    {
        [SerializeField] public MusicType musicType;
        [SerializeField] public AudioClip clip;
    }
}