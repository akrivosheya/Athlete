using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName="RoundDirectionSO")]
    public class RoundDirectionSO : RoadDirection
    {
        override public Vector3 GetDirection(Vector3 point)
        {
            var direction = Vector3.zero;
            direction.x = -1f;
            direction.z = -(point.x * direction.x) / point.z;
            return direction.normalized;
        }
    }
}
