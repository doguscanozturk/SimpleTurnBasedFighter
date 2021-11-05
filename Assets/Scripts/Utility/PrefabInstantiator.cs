using System;
using Data;
using GameplayElements;
using GameplayElements.Characters;
using InGameIndicators.Texts;
using UI.Elements;
using UI.Panels;
using UI.PopUps;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utility
{
    public static class PrefabInstantiator
    {
        public static T Instantiate<T>(Transform parent = null)
        {
            var type = typeof(T);

            if (type == typeof(Canvas)) return Instantiate<T>(DataContainers.AssetReferences.canvasPrefab, parent);
            if (type == typeof(BattlePanel)) return Instantiate<T>(DataContainers.AssetReferences.battlePanelPrefab, parent);
            if (type == typeof(CharacterSelectionPanel)) return Instantiate<T>(DataContainers.AssetReferences.characterSelectionPanelPrefab, parent);
            if (type == typeof(GameOverPanel)) return Instantiate<T>(DataContainers.AssetReferences.gameOverPanelPrefab, parent);
            if (type == typeof(Hero)) return Instantiate<T>(DataContainers.AssetReferences.heroPrefab, parent);
            if (type == typeof(Enemy)) return Instantiate<T>(DataContainers.AssetReferences.enemyPrefab, parent);
            if (type == typeof(Battleground)) return Instantiate<T>(DataContainers.AssetReferences.battlegroundPrefab, parent);
            if (type == typeof(HeroInfoPopup)) return Instantiate<T>(DataContainers.AssetReferences.heroInfoPopUpPrefab, parent);
            if (type == typeof(HeroUIElement)) return Instantiate<T>(DataContainers.AssetReferences.heroUIElementPrefab, parent);
            if (type == typeof(DamageInGameText)) return Instantiate<T>(DataContainers.AssetReferences.damageTextPrefab, parent);
            if (type == typeof(MessageInGameText)) return Instantiate<T>(DataContainers.AssetReferences.messageTextPrefab, parent);
            
            throw new Exception($"Requested type {type} couldn't instantiated!");
        }

        public static T Instantiate<T>(GameObject prefab, Transform parent = null)
        {
            return Object.Instantiate(prefab, parent).GetComponent<T>();
        }
    }
}