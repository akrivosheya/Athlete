using UnityEngine;

using Managers;
using Constraints;

namespace Objects.Interactable
{
    public class RunningStarter : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _name = "Start";
        [SerializeField] private string _scene = "Walking";
        [SerializeField] private string _runningScene;

        public void Interact()
        {
            if(!ManagersService.Conditions.CheckCondition(Conditions.HasRegistrated) || ManagersService.Conditions.CheckCondition(Conditions.RunRace))
            {
                ManagersService.Dialogue.StartDialogue(_name, _scene);
            }
            else
            {
                ManagersService.Level.LoadScene(_runningScene, StatesManager.GameStates.Start);
            }
        }
    }
}
