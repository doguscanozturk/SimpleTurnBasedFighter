using UnityEngine;

namespace Data.Containers
{
    [CreateAssetMenu(fileName = "AssetReferences", menuName = "DataContainer/AssetReferences")]
    public class AssetReferences : ScriptableObject
    {
        [Header("UI Prefabs")] 
        public GameObject canvasPrefab;
        public GameObject characterSelectionPanelPrefab;
        public GameObject battlePanelPrefab;
        public GameObject gameOverPanelPrefab;
        public GameObject heroUIElementPrefab;
        public GameObject heroInfoPopUpPrefab;
        
        [Header("Gameplay Prefabs")]
        public GameObject heroPrefab;
        public GameObject enemyPrefab;
        public GameObject battlegroundPrefab;

        [Header("Indicator Prefabs")] 
        public GameObject damageTextPrefab;
        public GameObject messageTextPrefab;
    }
}