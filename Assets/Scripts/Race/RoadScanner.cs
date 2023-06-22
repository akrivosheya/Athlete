using UnityEngine;

using Constraints;
using Managers;

namespace Race
{
    [RequireComponent(typeof(Runner))]
    public class RoadScanner : MonoBehaviour
    {
        public Vector3 CurrentRoadDirection { get; private set; }
        [SerializeField] private RaceJudge _judge;
        [SerializeField] private float _maxDistance = 100f;
        private Runner _runner;

        void Start()
        {
            _runner = GetComponent<Runner>();
        }

        void Update()
        {
            var ray = new Ray(transform.position, Vector3.down);
            int roadIndex = _runner.Road;
            if(Physics.Raycast(ray, out RaycastHit hit, _maxDistance, LayerMask.GetMask(Layers.Road)))
            {
                var road = hit.collider.GetComponent<Road>();
                roadIndex = road.RoadIndex;
                CurrentRoadDirection = road.GetDirection(transform.position);
            }
            else
            {
                Debug.Log("No road");
                roadIndex = 0;
            }
            if(roadIndex != _runner.Road)
            {
                //Debug.Log(_runner.Road + " " + roadIndex);
                _runner.Road = roadIndex;
                if(ManagersService.States.CurrentState == StatesManager.GameStates.Running)
                {
                    _judge.CheckRoad(_runner);
                }
            }
        }
    }
}
