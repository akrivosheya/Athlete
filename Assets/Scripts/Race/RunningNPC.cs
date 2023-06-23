using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Managers;
using Race.RunningBehaviours;

namespace Race
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(BodyCalculator))]
    [RequireComponent(typeof(RoadScanner))]
    [RequireComponent(typeof(Runner))]
    [RequireComponent(typeof(RunnerScanner))]
    public class RunningNPC : RunningObject
    {
        [SerializeField] private string _behaviourName;
        [SerializeField] private float _slowDownSpeed = 4f;
        [SerializeField] private float _gravityForce = -9.8f;
        private CharacterController _controller;
        private BodyCalculator _body;
        private RoadScanner _scanner;
        private Runner _runner;
        private RunnerScanner _runnerScanner;
        private IRunningBehaviour _behaviour;
        private bool _isStopped = false;
        private bool _isOutrunning = false;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _body = GetComponent<BodyCalculator>();
            _scanner = GetComponent<RoadScanner>();
            _runner = GetComponent<Runner>();
            _runnerScanner = GetComponent<RunnerScanner>();
            _behaviour = Factory.Factory<IRunningBehaviour>.Instance.GetObject(_behaviourName, new List<string>());
        }

        void Update()
        {
            if(ManagersService.States.CurrentStateIsRunnable && !_isStopped && !_isOutrunning)
            {
                bool madeEffort = _behaviour.CalcuateEffort(_body);
                var direction = _scanner.CurrentRoadDirection;
                transform.LookAt(transform.position + direction);

                float speed = 0f;
                if(_runner.CanChangeRoad && _runner.Road != 1 && _runnerScanner.CanRunToFirstRoad())
                {
                    direction = transform.TransformDirection(Vector3.forward + Vector3.left / 2);
                    transform.LookAt(transform.position + direction);
                    speed = _body.GetSpeed(madeEffort);
                }
                else if(_runnerScanner.TryGetRunner(_runner.Road, out BodyCalculator otherBody))
                {
                    var otherBodySpeed = otherBody.GetSpeed();
                    if(_runner.CanChangeRoad && _runner.Road == 1 && _body.CanRunFaster(otherBodySpeed, madeEffort))
                    {
                        /*direction = transform.TransformDirection(Vector3.forward + Vector3.right / 2);
                        transform.LookAt(transform.position + direction);*/
                        StartCoroutine(StartOutrunning());
                        return;
                    }
                    speed = _body.GetSpeed(otherBodySpeed, madeEffort);
                }
                else
                {
                    speed = _body.GetSpeed(madeEffort);
                }

                var movement = new Vector3(0, _gravityForce, speed) * Time.deltaTime;
                _controller.Move(transform.TransformDirection(movement));
            }
        }

        public override void Stop()
        {
            _isStopped = true;

            StartCoroutine(SlowDown());
        }

        public override void Die()
        {
            _isStopped = true;
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
        }

        private IEnumerator StartOutrunning()
        {
            _isOutrunning = true;

            while(_runner.Road == 1 && !_isStopped)
            {
                bool madeEffort = _behaviour.CalcuateEffort(_body);
                var direction = _scanner.CurrentRoadDirection;
                transform.LookAt(transform.position + direction);

                direction = transform.TransformDirection(Vector3.forward + Vector3.right / 2);
                transform.LookAt(transform.position + direction);
                float speed = _body.GetSpeed(madeEffort);

                var movement = new Vector3(0, _gravityForce, speed) * Time.deltaTime;
                _controller.Move(transform.TransformDirection(movement));

                yield return null;
            }

            _isOutrunning = false;
        }
    }
}
