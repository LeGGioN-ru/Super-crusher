using System;
using UnityEngine;

[Serializable]
public static class ShopAnimationController
{
    public static class State
    {
        public static readonly int Show = Animator.StringToHash(nameof(Show));
        public static readonly int Hide = Animator.StringToHash(nameof(Hide));
    }
}
