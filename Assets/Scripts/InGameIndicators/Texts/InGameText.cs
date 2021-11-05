using Commons;
using UnityEngine;

namespace InGameIndicators.Texts
{
    public abstract class InGameText : BasicMono
    {
        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] protected TextMesh textMesh;
        [SerializeField] protected int sortingOrder;

        public abstract void PlayAnimation(Vector3 startPosition, Vector3 targetPosition, float duration);

        public void SetText(string text)
        {
            textMesh.text = text;
        }

        private void Awake()
        {
            meshRenderer.sortingOrder = sortingOrder;
        }
    }
}