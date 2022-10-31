using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class CleanerAnimationController
{
    public static class State
    {
        public static readonly int Clean = Animator.StringToHash(nameof(Clean));
    }
}
