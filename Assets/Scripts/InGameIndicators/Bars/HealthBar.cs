using UnityEngine;

namespace InGameIndicators.Bars
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float width;
        [Range(0f, 1f)] [SerializeField] private float fillAmount;

        [SerializeField] private Transform background;
        [SerializeField] private Transform healthPivot;
        [SerializeField] private Transform health;
        
        public void UpdateFillAmount(float fillAmount)
        {
            this.fillAmount = Mathf.Clamp01(fillAmount);
            healthPivot.localScale = new Vector3(fillAmount, 1, 1);
        }
        
        private void OnValidate()
        {
            var newBackgroundScale = new Vector3(width, background.localScale.y, background.localScale.z);
            background.localScale = newBackgroundScale;
            health.localScale = newBackgroundScale;
            
            health.localPosition = new Vector3(width / 2, health.localPosition.y, health.localPosition.z);
            healthPivot.localPosition = Vector3.left * (width / 2);
            UpdateFillAmount(fillAmount);
        }
    }
}