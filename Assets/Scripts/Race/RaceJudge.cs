using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Managers;
using Constraints;

namespace Race
{
    public class RaceJudge : MonoBehaviour
    {
        [SerializeField] private float _minWaitTimeSeconds = 2f;
        [SerializeField] private float _maxWaitTimeSeconds = 4f;
        [SerializeField] private int _maxPoints = 4;
        private float _beginningTime = 0f;

        void Awake()
        {
            Messenger.AddListener(Events.LoadedScene, OnLoadedScene);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener(Events.LoadedScene, OnLoadedScene);
        }

        public void UpdateRunner(Runner runner)
        {
            float currentTime = Time.time;
            int runPoints = runner.RunPoints;
            runner.Times.Add(currentTime - GetRunTime(runner) - _beginningTime);
            if(runner.tag == Tags.Player)
            {
                Messenger.Broadcast(Events.PlayerRunTimePoint);
            }
            runner.RunPoints++;

            if(runner.RunPoints >= _maxPoints)
            {
                runner.GetComponent<RunningObject>().Stop();
                if(runner.tag == Tags.Player)
                {
                    ManagersService.States.CurrentState = StatesManager.GameStates.Finish;
                }
            }
        }

        public void CheckRoad(Runner runner)
        {
            if(Managers.ManagersService.States.CurrentState == StatesManager.GameStates.Finish)
            {
                return;
            }
            if(runner.Road == 0 || !runner.CanChangeRoad)
            {
                Debug.Log("Disqualified");
                runner.IsDisqualified = true;
            }
        }

        private void OnLoadedScene()
        {
            StartCoroutine(DoCounting());
        }

        private float GetRunTime(Runner runner)
        {
            float allTime = 0;

            foreach(var time in runner.Times)
            {
                allTime += time;
            }

            return allTime;
        }

        private IEnumerator DoCounting()
        {
            Debug.Log("Start");

            yield return new WaitForSeconds(Random.Range(_minWaitTimeSeconds, _maxWaitTimeSeconds));

            Debug.Log("Prepare");

            yield return new WaitForSeconds(Random.Range(_minWaitTimeSeconds, _maxWaitTimeSeconds));

            Debug.Log("Go");
            ManagersService.States.CurrentState = StatesManager.GameStates.Running;
            _beginningTime = Time.time;
        }
    }
}
