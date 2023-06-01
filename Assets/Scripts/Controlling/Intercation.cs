using UnityEngine;

using Constraints;
using Objects.Interactable;

namespace Controlling
{
    public class Intercation : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _maxDistance = 3f;

        void Update()
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0));
            if(Physics.Raycast(ray, out RaycastHit hit, _maxDistance, LayerMask.GetMask(Layers.Interactable)))
            {
                if(hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    Messenger.Broadcast(Events.Interact);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}