using System;
using Attributes;

namespace EventMediators
{
    public static class UIEvents
    {
        public static event Action<HeroAttributes[]> OnBattleClicked;
        public static event Action OnReturnCharacterSelectionClicked;

        public static void TriggerBattleClicked(HeroAttributes[] heroes)
        {
            OnBattleClicked?.Invoke(heroes);
        }
        
        public static void TriggerReturnCharacterSelectionClicked()
        {
            OnReturnCharacterSelectionClicked?.Invoke();
        }
    }
}