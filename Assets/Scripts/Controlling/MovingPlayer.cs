using UnityEngine;

using Managers;

namespace Controlling
{
    [RequireComponent(typeof(CharacterController))]
    public class MovingPlayer : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _speedIncreasing = 2f;
        [SerializeField] private float _gravityForce = -9.8f;
        private CharacterController _controller;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            if(!ManagersService.States.CurrentStateIsPlayable)
            {
                return;
            }
            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(xAxis, 0, zAxis);
            float finalSpeed = (Input.GetKey(KeyCode.LeftShift)) ? _speed * _speedIncreasing : _speed;
            movement *= finalSpeed;
            movement = Vector3.ClampMagnitude(movement, finalSpeed);
            movement.y = _gravityForce;
            movement *= Time.deltaTime;
            _controller.Move(transform.TransformDirection(movement));
        }
    }
}