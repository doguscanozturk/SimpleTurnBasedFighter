using GameplayElements.Animation;
using UnityEngine;

namespace Data.Containers
{
    [CreateAssetMenu(fileName = "CharacterAnimationSettings", menuName = "DataContainer/CharacterAnimationSettings")]
    public class CharacterAnimationSettings : ScriptableObject
    {
        public AnimationSettings.Attack heroAttack;
        public AnimationSettings.Attack enemyAttack;
        public AnimationSettings.Death death;
    }
}