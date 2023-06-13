using UnityEngine;

using Managers;

namespace Controlling
{
    public class PauseGame : MonoBehaviour
    {
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                StatesManager States = ManagersService.States;
                if(States.CurrentState == StatesManager.GameStates.Pause)
                {
                    States.RollbackState();
                    Messenger.Broadcast(Events.Unpaused);
                }
                else if(States.CurrentStateCanPause)
                {
                    States.CurrentState = StatesManager.GameStates.Pause;
                    Messenger.Broadcast(Events.Paused);
                }
            }
        }
    }
}
