using System.Collections.Generic;
using UnityEngine;

using ScriptableObjects;

namespace Managers
{
    public class RaceManager : MonoBehaviour
    {
        public int Race { get; set; }

        [SerializeField] private LevelsDataSO _levels;
        [SerializeField] private float[] _categoriesTimes;
        public float Seconds { get; private set; }
        public bool HasRunRace { get; private set; }
        public bool IsDisqualified { get; private set; }
        public bool IsDead { get; private set; }

        public void ClearResults()
        {
            Seconds = 0;
            HasRunRace = false;
            IsDisqualified = false;
            IsDead = false;
        }

        public void SetResults(Race.Runner runner)
        {
            Seconds = GetSum(runner.Times);
            IsDisqualified = runner.IsDisqualified;
            IsDead = runner.GotOffTrack;
            HasRunRace = !(IsDisqualified || IsDead);

            var level = _levels.GetLevelData(Race);
            if((!level.IsCompleted && HasRunRace) || level.Time > Seconds)
            {
                level.IsCompleted = true;
                level.Time = Seconds;
                level.Category = CheckCategory(Seconds);
                //_levels.SaveLevels();
            }

            ManagersService.Conditions.AddCondition(Constraints.Conditions.RunRace);
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

        private int CheckCategory(float seconds)
        {
            for(int i = 0; i < _categoriesTimes.Length; i++)
            {
                if(seconds < _categoriesTimes[i])
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
