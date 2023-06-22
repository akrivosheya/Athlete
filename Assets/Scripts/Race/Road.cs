using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ScriptableObjects;

namespace Race
{
    [RequireComponent(typeof(Collider))]
    public class Road : MonoBehaviour
    {
        public int RoadIndex => _roadIndex;
        [SerializeField] private RoadDirection _direction;
        [SerializeField] private int _roadIndex;

        public Vector3 GetDirection(Vector3 point)
        {
            var localDirection = _direction.GetDirection(transform.InverseTransformPoint(point));
            return transform.TransformDirection(localDirection);
        }
    }
}
