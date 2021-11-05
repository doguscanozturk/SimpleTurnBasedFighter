using EventMediators;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class GameOverPanel : Panel
    {
        [SerializeField] private Text result;
        
        public void OnReturnHomeClicked()
        {
            UIEvents.TriggerReturnCharacterSelectionClicked();
            Hide();
        }

        public void PreparePanel(bool didHeroesWin)
        {
            result.text = didHeroesWin ? "<color=green>YOU WIN!</color>" : "<color=red>YOU LOST!</color>";
        }
    }
}