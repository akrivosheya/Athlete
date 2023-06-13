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
            Running = 4,
            Dialogue = 5
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
        public bool CurrentStateCanPause
        {
            get
            {
                return _currentState == GameStates.Walking || _currentState == GameStates.Running ||
                _currentState == GameStates.Dialogue;
            }
        }
        public bool CurrentStateIsPause
        {
            get
            {
                return _currentState == GameStates.Pause;
            }
        }
        
        private GameStates _currentState = GameStates.None;
        private GameStates _previousState = GameStates.None;

        public void RollbackState()
        {
            CurrentState = _previousState;
        }

        public void SwitchState()
        {
            if(CurrentState == GameStates.Walking)
            {
                CurrentState = GameStates.Running;
            }
            else if(CurrentState == GameStates.Running)
            {
                CurrentState = GameStates.Walking;
            }
        }
    }
}
