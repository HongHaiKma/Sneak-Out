using UnityEngine;

namespace Game
{
    public static class HashDictionary
    {
        public static readonly int Speed = Animator.StringToHash("speed");
        public static readonly int Attack = Animator.StringToHash("attack");
        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Walk = Animator.StringToHash("walk");
        public static readonly int Run = Animator.StringToHash("run");
        public static readonly int Sleep = Animator.StringToHash("sleep");
        public static readonly int Open = Animator.StringToHash("open");
        public static readonly int Awake = Animator.StringToHash("awake");
        public static readonly int Jump = Animator.StringToHash("jump");
        public static readonly int Victory = Animator.StringToHash("victory");
    }
}