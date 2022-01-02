using System;
using Helper;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<EGameState> OnBeforeStateChanged;
        public static event Action<EGameState> OnAfterStateChanged;
        
        public EGameState State { get; private set; }

        private void Start() => ChangeState(EGameState.Starting);

        public void ChangeState(EGameState newState)
        {
            if(State == newState) return;
            
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState)
            {
                case EGameState.Starting:
                    HandleStarting();
                    break;
                case EGameState.Win:
                    HandleWin();
                    break;
                case EGameState.Lose:
                    HandleLose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            
            OnAfterStateChanged?.Invoke(newState);
            Debug.Log($"New state: {newState}");
        }

        private void HandleStarting()
        {
            
        }

        private void HandleWin()
        {
            
        }

        private void HandleLose()
        {
            
        }
    }
}