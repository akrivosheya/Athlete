using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIKeys _keys;
        [SerializeField] private UIMenu _menu;

        void Awake()
        {
            Messenger.AddListener(Events.Interact, OnInteract);
            Messenger.AddListener(Events.Paused, OnPaused);
            Messenger.AddListener(Events.Unpaused, OnUnpaused);
        }
        
        void OnDestroy()
        {
            Messenger.RemoveListener(Events.Interact, OnInteract);
            Messenger.RemoveListener(Events.Paused, OnPaused);
            Messenger.RemoveListener(Events.Unpaused, OnUnpaused);
        }

        private void OnInteract()
        {
            _keys.ShowInteraction();
        }

        private void OnPaused()
        {
            _menu.Show();
        }

        private void OnUnpaused()
        {
            _menu.Hide();
        }
    }
}
