using System;
using UnityEngine;

[Serializable]
public static class EduactionPanelAnimationController
{
    public static class State
    {
        public readonly static int Show = Animator.StringToHash(nameof(Show));
        public readonly static int Hide = Animator.StringToHash(nameof(Hide));
    }
}
