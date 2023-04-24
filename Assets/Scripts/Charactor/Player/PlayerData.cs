using UnityEngine;
using UnityEngine.Serialization;

namespace Charactor
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move")] 
        public float MoveSpeed;

        [Header("Jump")] 
        public float JumpSpeed;

        [Header("Descend")] 
        public float ClampSpeed;
        public float GravityMultiplier;
        
        [Header("Shoot")]
        public float ShootInterval;
        public float MaxAngleOffest;
    }
}