using System;
using UnityEngine;

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

        [Header("PrimaryAttack")]
        public float PrimaryInterval;
        public float PrimaryShakeMultiplier = 0.1f;
        public FireGroup[] PrimaryFireGroup;
    }

    [Serializable]
    public struct FireGroup
    {
        public Vector3 Offset;
        public float Angle;
        public float RandomAngle;
    }
}