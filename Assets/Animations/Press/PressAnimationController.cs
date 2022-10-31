using System;
using UnityEngine;

[Serializable]
public static class PressAnimationController
{
    public static class State
    {
        public static readonly int Back = Animator.StringToHash(nameof(Back));
    }
}
