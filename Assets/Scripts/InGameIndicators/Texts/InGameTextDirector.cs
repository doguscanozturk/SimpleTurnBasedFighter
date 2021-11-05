using Data;
using UnityEngine;
using Utility;

namespace InGameIndicators.Texts
{
    public static class InGameTextDirector
    {
        public static void DisplayDamage(int damage, Vector3 position)
        {
            var damageText = GetDamageIndicator();
            damageText.SetText(damage.ToString());
            var targetPosition = position + DataContainers.UxDesignValues.textTargetPositionOffset;
            damageText.PlayAnimation(position, targetPosition, DataContainers.UxDesignValues.damageTextLifetime);
        }

        public static void DisplayMessage(string message, Vector3 position)
        {
            var messageText = GetMessageText();
            messageText.SetText(message);
            var targetPosition = position + DataContainers.UxDesignValues.textTargetPositionOffset;
            messageText.PlayAnimation(position, targetPosition, DataContainers.UxDesignValues.messageTextLifetime);
        }

        private static DamageInGameText GetDamageIndicator()
        {
            return PrefabInstantiator.Instantiate<DamageInGameText>();
        }

        private static MessageInGameText GetMessageText()
        {
            return PrefabInstantiator.Instantiate<MessageInGameText>();
        }
    }
}