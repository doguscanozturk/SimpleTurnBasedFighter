using System;
using UnityEngine;

namespace GameplayElements.Animation
{
    public static class AnimationSettings
    {
        [Serializable]
        public struct Attack
        {
            public float startDelay;
            public float approachDuration;
            public Vector3 approachRotation;
            public Vector3 approachPositionOffset;
            public float returnDuration;
        }

        [Serializable]
        public struct Death
        {
            public float scaleDuration;
            public Vector3 endScale;
        }
    }
}