using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(Collider))]
    public class TimePoint : MonoBehaviour
    {
        [SerializeField] private RaceJudge _raceJudge;
        [SerializeField] private int _index;

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Runner>(out Runner runner))
            {
                if((runner.RunPoints - _index) % 2 == 0)
                {
                    _raceJudge.UpdateRunner(runner);
                }
            }
        }
    }
}
