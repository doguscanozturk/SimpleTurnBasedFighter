using System;
using Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Elements
{
    public class HeroUIElement : BasicUIElement, IPointerUpHandler, IPointerDownHandler
    {
        public static event Action<HeroUIElement> OnHeroUIElementPointerUp;
        public static event Action<HeroUIElement> OnHeroUIElementPointerDown;
        
        [SerializeField] private Outline outline;
        [SerializeField] private new Text name;
        [SerializeField] private GameObject lockImage;
        private bool isUnlocked;

        public HeroAttributes HeroAttributes { get; private set; }

        public void UpdateSettings(HeroAttributes heroHeroAttributes, bool isUnlocked)
        {
            HeroAttributes = heroHeroAttributes;
            name.text = heroHeroAttributes.name;
            this.isUnlocked = isUnlocked;
            SetLockImage(isUnlocked);
        }
        
        public void ToggleOutline()
        {
            outline.enabled = !outline.enabled;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isUnlocked) return;

            OnHeroUIElementPointerUp?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isUnlocked) return;

            OnHeroUIElementPointerDown?.Invoke(this);
        }

        private void SetLockImage(bool isUnlocked)
        {
            lockImage.SetActive(!isUnlocked);
        }
    }
}