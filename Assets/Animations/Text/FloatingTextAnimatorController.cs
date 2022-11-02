using System;
using UnityEngine;

[Serializable]
public static class FloatingTextAnimatorController
{
    public static class State
    {
        public static readonly int Incarnate = Animator.StringToHash(nameof(Incarnate));
    }
}
