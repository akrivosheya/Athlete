using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class RaceManager : MonoBehaviour
    {
        public float Seconds { get; private set; }
        public bool HasRunRace { get; private set; }
        public bool IsDisqualified { get; private set; }

        public void ClearResults()
        {
            Seconds = 0;
            HasRunRace = false;
            IsDisqualified = false;
        }

        public void SetResults(Race.Runner runner)
        {
            Seconds = GetSum(runner.Times);
            IsDisqualified = runner.IsDisqualified;
            HasRunRace = true;
        }

        private float GetSum(List<float> times)
        {
            float accamulator = 0;
            foreach(var time in times)
            {
                accamulator += time;
            }
            return accamulator;
        }
    }
}
