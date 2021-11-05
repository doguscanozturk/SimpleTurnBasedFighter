using System;
using BattleSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class BattlePanel : Panel
    {
        [SerializeField] private Text battleStateText;
        
        private void OnEnable()
        {
            BattleManager.OnBattleStateUpdated += HandleBattleStateUpdated;
        }

        private void OnDisable()
        {
            BattleManager.OnBattleStateUpdated -= HandleBattleStateUpdated;
        }

        private void HandleBattleStateUpdated(BattleState state)
        {
            switch (state)
            {
                case BattleState.PlayerAttacking:
                    battleStateText.text = "<color=orange>PICK A HERO TO ATTACK!</color>";
                    break;
                case BattleState.EnemyAttacking:
                    battleStateText.text = "<color=red>ENEMY IS ATTACKING!</color>";
                    break;
                case BattleState.Ended:
                    battleStateText.text = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}