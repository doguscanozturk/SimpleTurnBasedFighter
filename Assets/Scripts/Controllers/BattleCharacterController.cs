using System;
using Commons;
using Data.Containers;
using GameplayElements.Characters;
using UnityEngine;
using Utility;

namespace Controllers
{
    public class BattleCharacterController
    {
        public static event Action<BattleCharacter> OnButtonDownOnBattleCharacter;
        public static event Action<BattleCharacter> OnButtonUpOnBattleCharacter;

        private readonly Collider2D[] hitResults;
        private readonly UxDesignValues uxDesignValues;
        private readonly BasicTimer basicTimer;

        private bool isActive;

        public BattleCharacterController(UxDesignValues uxDesignValues)
        {
            this.uxDesignValues = uxDesignValues;
            hitResults = new Collider2D[1];
            basicTimer = new BasicTimer();
        }

        public void TriggerActivate(bool isActive)
        {
            this.isActive = isActive;
        }

        public void UpdateFrame(float deltaTime)
        {
            basicTimer.UpdateFrame(deltaTime);
            
            if (!isActive) return;

            if (Input.GetMouseButtonDown(0))
            {
                var battleCharacter = TryOverlapBattleCharacter(Input.mousePosition);
                if (battleCharacter != null)
                {
                    OnButtonDownOnBattleCharacter?.Invoke(battleCharacter);
                }

                basicTimer.StartTimer(uxDesignValues.clickDetectionDuration, null);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (basicTimer.TimerDuration <= 0)
                {
                    return;
                }
                
                var battleCharacter = TryOverlapBattleCharacter(Input.mousePosition);
                if (battleCharacter != null)
                {
                    OnButtonUpOnBattleCharacter?.Invoke(battleCharacter);
                }
            }
        }

        private BattleCharacter TryOverlapBattleCharacter(Vector3 mousePosition)
        {
            var worldPosition = CameraUtility.ScreenToWorldPoint(mousePosition);
            var hitCount = Physics2D.OverlapPointNonAlloc(worldPosition, hitResults);

            if (hitCount > 0 && hitResults[0].CompareTag("BattleCharacter"))
            {
                return hitResults[0].GetComponentInParent<BattleCharacter>();
            }

            return null;
        }
    }
}