using UnityEngine;

namespace Objects.Interactable
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        private int count = 0;
        public void Interact()
        {
            count++;
            Debug.Log("Interact" + count);
        }
    }
}
