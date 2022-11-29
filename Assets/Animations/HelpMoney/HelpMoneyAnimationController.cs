using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class HelpMoneyAnimationController
{
    public static class State
    {
        public static readonly int Show = Animator.StringToHash(nameof(Show));
        public static readonly int Hide = Animator.StringToHash(nameof(Hide));
    }
}
