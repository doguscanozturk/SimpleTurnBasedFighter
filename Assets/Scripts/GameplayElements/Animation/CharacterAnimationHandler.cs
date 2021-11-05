using System;
using DG.Tweening;
using UnityEngine;

namespace GameplayElements.Animation
{
    public class CharacterAnimationHandler
    {
        private readonly IAnimatableCharacter animatableCharacter;

        private Sequence attackSequence;
        private Tweener deathTween;

        public CharacterAnimationHandler(IAnimatableCharacter animatableCharacter, Action onDamageDoneCallback)
        {
            this.animatableCharacter = animatableCharacter;
            PrepareAttackSequence(animatableCharacter.AnimationSettings, onDamageDoneCallback);
        }

        public void Clear()
        {
            attackSequence.Kill();
            attackSequence = null;

            if (deathTween != null && deathTween.active)
            {
                deathTween.Kill();
                deathTween = null;
            }
        }

        public void TriggerAttackAnimation()
        {
            attackSequence.Restart();
        }

        public void TriggerDeathAnimation(AnimationSettings.Death settings)
        {
            deathTween = animatableCharacter.AnimationTransform.DOScale(settings.endScale, settings.scaleDuration);
        }

        private void PrepareAttackSequence(AnimationSettings.Attack settings, Action onDamageDoneCallback)
        {
            var startPosition = animatableCharacter.AnimationTransform.position;
            attackSequence = DOTween.Sequence();
            attackSequence.SetAutoKill(false);
            attackSequence.Pause();

            attackSequence.SetDelay(settings.startDelay);
            attackSequence.Append(animatableCharacter.AnimationTransform
                .DOMove(startPosition + settings.approachPositionOffset, settings.approachDuration));

            attackSequence.Append(animatableCharacter.AnimationTransform
                .DOLocalRotate(settings.approachRotation, settings.approachDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(onDamageDoneCallback.Invoke));

            attackSequence.Append(animatableCharacter.AnimationTransform
                .DOLocalRotate(Vector3.zero, settings.returnDuration)
                .SetEase(Ease.OutQuad));

            var turnBackStartSecond = settings.startDelay + (settings.approachDuration * 2);
            attackSequence.Insert(turnBackStartSecond, animatableCharacter.AnimationTransform
                .DOMove(startPosition, settings.returnDuration));
        }
    }
}