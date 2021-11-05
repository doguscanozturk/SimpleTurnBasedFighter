using DG.Tweening;
using UnityEngine;

namespace InGameIndicators.Texts
{
    public class MessageInGameText : InGameText
    {
        public override void PlayAnimation(Vector3 startPosition, Vector3 targetPosition, float duration)
        {
            thisTransform.position = startPosition;
            thisTransform.localScale = Vector3.zero;

            thisTransform.DOScale(Vector3.one, duration / 3);
            thisTransform.DOMove(targetPosition, duration).OnComplete(() =>
            {
                Destroy(thisGameObject);
            });
        }
    }
}