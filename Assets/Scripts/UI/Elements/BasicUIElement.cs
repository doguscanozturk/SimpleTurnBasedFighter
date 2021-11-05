using UnityEngine;

namespace UI.Elements
{
    public abstract class BasicUIElement : MonoBehaviour
    {
        public GameObject ThisGameObject { get; private set; }
        public RectTransform ThisRectTransform { get; private set; }

        public bool IsShowing { get; protected set; }
        
        private void Awake()
        {
            ThisGameObject = gameObject;
            ThisRectTransform = (RectTransform) transform;
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public virtual void Show()
        {
            IsShowing = true;
            ThisGameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            IsShowing = false;
            ThisGameObject.SetActive(false);
        }
    }
}