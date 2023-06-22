using UnityEngine;

namespace ScriptableObjects
{
    public abstract class RoadDirection : ScriptableObject
    {
        public abstract Vector3 GetDirection(Vector3 point);
    }
}
