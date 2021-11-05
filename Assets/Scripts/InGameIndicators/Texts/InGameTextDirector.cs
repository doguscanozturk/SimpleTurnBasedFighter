using Data;
using Data.Containers;
using UnityEngine;
using Utility;

namespace InGameIndicators.Texts
{
    public static class InGameTextDirector
    {
        private static readonly UxDesignValues uxDesignValues;

        static InGameTextDirector()
        {
            uxDesignValues = DataContainers.UxDesignValues;
        }

        public static void DisplayDamage(int damage, Vector3 position)
        {
            var damageText = GetDamageIndicator();
            damageText.SetText(damage.ToString());
            var targetPosition = position + uxDesignValues.textTargetPositionOffset;
            damageText.PlayAnimation(position, targetPosition, uxDesignValues.damageTextLifetime);
        }

        public static void DisplayMessage(string message, Vector3 position)
        {
            var messageText = GetMessageText();
            messageText.SetText(message);
            var targetPosition = position + uxDesignValues.textTargetPositionOffset;
            messageText.PlayAnimation(position, targetPosition, uxDesignValues.messageTextLifetime);
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