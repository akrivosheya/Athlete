using UnityEngine;

namespace Managers
{
    public class StatesManager : MonoBehaviour
    {
        public enum GameStates
        {
            None = 0,
            Loading = 1,
            Walking = 2,
            Pause = 3,
            Running = 4
        }

        public GameStates CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _previousState = _currentState;
                _currentState = value;
            }
        }
        public bool CurrentStateIsPlayable
        {
            get
            {
                return _currentState == GameStates.Walking || _currentState == GameStates.Running;
            }
        }
        
        private GameStates _currentState = GameStates.Walking;
        private GameStates _previousState = GameStates.None;

        public void RollbackState()
        {
            CurrentState = _previousState;
        }
    }
}
