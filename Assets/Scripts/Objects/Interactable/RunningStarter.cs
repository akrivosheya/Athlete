using UnityEngine;

namespace Objects.Interactable
{
    public class RunningStarter : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _runningScene;

        public void Interact()
        {
            Managers.ManagersService.Level.LoadScene(_runningScene, Managers.StatesManager.GameStates.Start);
        }
    }
}
