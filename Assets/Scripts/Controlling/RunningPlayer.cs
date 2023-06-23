using System.Collections;
using UnityEngine;

using Race;
using Managers;

namespace Controlling
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(BodyCalculator))]
    [RequireComponent(typeof(Runner))]
    [RequireComponent(typeof(RunnerScanner))]
    public class RunningPlayer : RunningObject
    {
        [SerializeField] private WatchingCamera _camera;
        [SerializeField] private Vector3 _slowDownCameraOffset;
        [SerializeField] private float _slowDownSpeed = 4f;
        [SerializeField] private float _rotationSpeed = 1f;
        [SerializeField] private float _gravityForce = -9.8f;
        [SerializeField] private float _cameraSpeed = 1f;
        [SerializeField] private float _cameraFinalYPosition = 2f;
        [SerializeField] private float _cameraStopRange = 0.2f;
        private CharacterController _controller;
        private BodyCalculator _body;
        private Runner _runner;
        private RunnerScanner _runnerScanner;
        private bool _isStopped = false;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _body = GetComponent<BodyCalculator>();
            _runner = GetComponent<Runner>();
            _runnerScanner = GetComponent<RunnerScanner>();
        }

        void Update()
        {
            if(!Managers.ManagersService.States.CurrentStateIsRunnable || _isStopped)
            {
                return;
            }
            var axis = Input.GetAxis("Horizontal");
            transform.Rotate(0, axis * _rotationSpeed * Time.deltaTime, 0);

            if(Input.GetKeyDown(KeyCode.R))
            {
                _body.DecreaseEffort();
            }

            float speed = 0f;
            if(_runnerScanner.TryGetRunner(_runner.Road, out BodyCalculator otherBody))
            {
                var otherBodySpeed = otherBody.GetSpeed();
                speed = _body.GetSpeed(otherBodySpeed, Input.GetKeyDown(KeyCode.Space));
            }
            else
            {
                speed = _body.GetSpeed(Input.GetKeyDown(KeyCode.Space));
            }

            var movement = new Vector3(0, _gravityForce, speed);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _controller.Move(movement);
        }

        public override void Stop()
        {
            _isStopped = true;

            _camera.transform.parent = null;
            var cameraPosition = transform.position + transform.TransformDirection(_slowDownCameraOffset);
            _camera.transform.position = cameraPosition;
            _camera.StartWatching();

            StartCoroutine(SlowDown());
        }

        public override void Die()
        {
            _isStopped = true;

            _camera.transform.parent = null;
            var cameraPosition = transform.position + transform.TransformDirection(Vector3.up);
            _camera.transform.position = cameraPosition;
            _camera.StartWatching();
            _runner.GotOffTrack = true;

            StartCoroutine(ShowDeadBody());
        }

        private IEnumerator SlowDown()
        {
            float speed = _body.GetSpeed();
            float slowDownCoefficient = _slowDownSpeed * Time.deltaTime;
            var movement = Vector3.zero;
            movement.y = _gravityForce;

            while(speed > slowDownCoefficient)
            {
                speed -= slowDownCoefficient;
                movement.z = speed;
                var fixedMovement = transform.TransformDirection(movement * Time.deltaTime);
                _controller.Move(fixedMovement);
                
                yield return null;

                slowDownCoefficient = _slowDownSpeed * Time.deltaTime;
            }

            ManagersService.Race.SetResults(_runner);
            ManagersService.Level.LoadPreviousScene();
        }

        private IEnumerator ShowDeadBody()
        {
            var finalCameraPosition = transform.position + Vector3.up * _cameraFinalYPosition;
            var movement = new Vector3(0, _cameraSpeed, 0);

            while(Vector3.Magnitude(_camera.transform.position - finalCameraPosition) > _cameraStopRange)
            {
                var fixedMovement = movement * Time.deltaTime;
                _camera.transform.Translate(fixedMovement, Space.World);
                
                yield return null;
            }

            ManagersService.Race.SetResults(_runner);
            ManagersService.Level.LoadPreviousScene();
        }
    }
}
