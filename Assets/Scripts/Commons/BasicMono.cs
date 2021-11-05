using UnityEngine;

namespace Commons
{
    public class BasicMono : MonoBehaviour
    {
        [Header("BasicMono")] 
        [SerializeField] protected Transform thisTransform;
        [SerializeField] protected GameObject thisGameObject;

        public Transform ThisTransform => thisTransform;
        public GameObject ThisGameObject => thisGameObject;

        [ContextMenu("GetBasicReferences")]
        protected void GetBasicReferences()
        {
            thisTransform = transform;
            thisGameObject = gameObject;
        }
    }
}