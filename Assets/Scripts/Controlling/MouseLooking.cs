using UnityEngine;

using Managers;

namespace Controlling
{
    public class MouseLooking : MonoBehaviour
    {
        public enum LookingMode
        {
            Horizontal = 0,
            Vertical = 1
        }

        [SerializeField] private LookingMode _mode;
        [SerializeField] private float _rotationSensitivity = 5f;
        [SerializeField] private float _maxVerticalRotation = 45f;
        [SerializeField] private float _minVerticalRotation = -45f;
        private float rotationX = 0;

        void Update()
        {
            if(!ManagersService.States.CurrentStateIsPlayable)
            {
                return;
            }
            if(_mode == LookingMode.Horizontal)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * _rotationSensitivity, 0);
            }
            else if(_mode == LookingMode.Vertical)
            {
                rotationX -= Input.GetAxis("Mouse Y") * _rotationSensitivity;
                rotationX = Mathf.Clamp(rotationX, _minVerticalRotation, _maxVerticalRotation);
                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
        }
    }
}