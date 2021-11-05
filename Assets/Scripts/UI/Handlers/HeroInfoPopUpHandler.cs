using Attributes;
using Commons;
using Data.Containers;
using UI.PopUps;
using UnityEngine;
using Utility;

namespace UI.Handlers
{
    public class HeroInfoPopUpHandler
    {
        public bool IsShowingPopUp => heroInfoPopup.IsShowing;

        private readonly BasicTimer basicTimer;
        private readonly HeroInfoPopup heroInfoPopup;
        private readonly UxDesignValues uxDesignValues;

        private HeroAttributes focusHeroHeroAttributes;
        private Vector3 focusedHeroScreenPosition;

        public HeroInfoPopUpHandler(RectTransform parent, UxDesignValues uxDesignValues)
        {
            this.uxDesignValues = uxDesignValues;
            basicTimer = new BasicTimer();
            heroInfoPopup = PrefabInstantiator.Instantiate<HeroInfoPopup>(parent);
        }

        public void UpdateFrame(float deltaTime)
        {
            basicTimer.UpdateFrame(deltaTime);
        }

        public void SetAsLastSibling()
        {
            heroInfoPopup.ThisRectTransform.SetAsLastSibling();
        }

        public void Cancel()
        {
            basicTimer.Cancel();
        }

        public void StartTimer(HeroAttributes heroHeroAttributes, Vector3 screenPosition)
        {
            focusHeroHeroAttributes = heroHeroAttributes;
            focusedHeroScreenPosition = screenPosition;
            basicTimer.StartTimer(uxDesignValues.activateHeroInfoPopupDuration, HeroPopupActivated);
        }

        private void HeroPopupActivated()
        {
            heroInfoPopup.Fill(focusHeroHeroAttributes);

            var size = heroInfoPopup.ThisRectTransform.sizeDelta;
            var rightEndPositionX = focusedHeroScreenPosition.x + size.x;
            var heroInfoPopUpPosition = rightEndPositionX > Screen.width
                ? focusedHeroScreenPosition + (Vector3.left * size.x)
                : focusedHeroScreenPosition;

            heroInfoPopup.ThisRectTransform.position = heroInfoPopUpPosition;
            heroInfoPopup.Show();
        }
    }
}