using UnityEngine;

namespace GameplayElements.Animation
{
    public interface IAnimatableCharacter
    {
        Transform AnimationTransform { get; }
        
        AnimationSettings.Attack AnimationSettings { get; }
    }
}