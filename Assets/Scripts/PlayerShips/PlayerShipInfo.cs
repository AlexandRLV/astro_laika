using System;
using Player;
using UnityEngine;

namespace PlayerShips
{
    [Serializable]
    public class PlayerShipInfo
    {
        [SerializeField] public string Name;
        [SerializeField] public GameObject PreviewPrefab;
        [SerializeField] public PlayerController GamePrefab;
        [SerializeField] public int Cost;
    }
}