using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIKeys _keys;

        void Awake()
        {
            Messenger.AddListener(Events.Interact, OnInteract);
        }
        
        void OnDestroy()
        {
            Messenger.RemoveListener(Events.Interact, OnInteract);
        }

        private void OnInteract()
        {
            _keys.ShowInteraction();
        }
    }
}
