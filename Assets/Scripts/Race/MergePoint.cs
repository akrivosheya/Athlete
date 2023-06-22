using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(Collider))]
    public class MergePoint : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Runner>(out Runner runner))
            {
                if(!runner.CanChangeRoad)
                {
                    runner.CanChangeRoad = true;
                }
            }
        }
    }
}
