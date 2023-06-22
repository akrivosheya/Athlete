using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName="StraightDirectionSO")]
    public class StraightDirectionSO : RoadDirection
    {
        [SerializeField] private Vector3 _localDirection;

        override public Vector3 GetDirection(Vector3 point)
        {
            return _localDirection;
        }
    }
}
