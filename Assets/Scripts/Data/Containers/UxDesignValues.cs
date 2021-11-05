using UnityEngine;

namespace Data.Containers
{
    [CreateAssetMenu(fileName = "UxDesignValues", menuName = "DataContainer/UxDesignValues")]
    public class UxDesignValues : ScriptableObject
    {
        [Header("UI")]
        public float activateHeroInfoPopupDuration;
        public float gameOverPanelOpeningDelay;

        [Header("In-game Texts")]
        public float damageTextLifetime;
        public float messageTextLifetime;
        public float levelUpTextDelay;
        public Vector3 textTargetPositionOffset;
        public Vector3 experienceTextStartOffset;

        [Header("In-game")]
        public float clickDetectionDuration;
    }
}