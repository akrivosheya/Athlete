using UnityEngine;

using Managers;

namespace Controlling
{
    public class RaceEnd : MonoBehaviour
    {
        [SerializeField] private Race.Runner _runner;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && ManagersService.States.CurrentState != StatesManager.GameStates.Finish)
            {
                _runner.GotOffTrack = true;
                ManagersService.Race.SetResults(_runner);
                ManagersService.Level.LoadPreviousScene();
            }
        }
    }
}
