using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIKeys _keys;
        [SerializeField] private UIMenu _menu;
        [SerializeField] private UIDialogue _dialogue;
        [SerializeField] private UIDarkness _darkness;

        void Awake()
        {
            Messenger.AddListener(Events.Interact, OnInteract);
            Messenger.AddListener(Events.Paused, OnPaused);
            Messenger.AddListener(Events.Unpaused, OnUnpaused);
            Messenger.AddListener(Events.ChangedText, OnChangedText);
            Messenger.AddListener(Events.StartedDialogue, OnStartedDialogue);
            Messenger.AddListener(Events.EndDialogue, OnEndDialogue);
            Messenger.AddListener(Events.LoadScene, OnLoadScene);
        }
        
        void OnDestroy()
        {
            Messenger.RemoveListener(Events.Interact, OnInteract);
            Messenger.RemoveListener(Events.Paused, OnPaused);
            Messenger.RemoveListener(Events.Unpaused, OnUnpaused);
            Messenger.RemoveListener(Events.ChangedText, OnChangedText);
            Messenger.RemoveListener(Events.StartedDialogue, OnStartedDialogue);
            Messenger.RemoveListener(Events.EndDialogue, OnEndDialogue);
            Messenger.RemoveListener(Events.LoadScene, OnLoadScene);
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

        private void OnChangedText()
        {
            _dialogue.ChangeDialogue();
        }

        private void OnStartedDialogue()
        {
            _dialogue.StartDialogue();
        }

        private void OnEndDialogue()
        {
            _dialogue.EndDialogue();
        }

        private void OnLoadScene()
        {
            _darkness.MakeDarkness();
        }
    }
}
