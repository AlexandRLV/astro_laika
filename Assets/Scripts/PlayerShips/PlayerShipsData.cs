using UnityEngine;

namespace PlayerShips
{
    [CreateAssetMenu(fileName = "Player Ships Data")]
    public class PlayerShipsData : ScriptableObject
    {
        [SerializeField] public PlayerShipInfo[] PlayerShips;
    }
}