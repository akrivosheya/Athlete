using UnityEngine;

namespace Race
{
    public abstract class RunningObject : MonoBehaviour
    {
        public abstract void Stop();
        public abstract void Die();
    }
}
