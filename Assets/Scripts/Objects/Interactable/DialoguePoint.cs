using UnityEngine;

using Managers;

namespace Objects.Interactable
{
    public class DialoguePoint : MonoBehaviour, IInteractable
    {
        private string _scene;
        private string _name;

        void Start()
        {
            _scene = ManagersService.Level.CurrentSceneName;
            _name = gameObject.name;
        }

        public void Interact()
        {
            ManagersService.Dialogue.StartDialogue(_name, _scene);
        }
    }
}
