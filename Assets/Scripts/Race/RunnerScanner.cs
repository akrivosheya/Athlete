using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constraints;

namespace Race
{
    public class RunnerScanner : MonoBehaviour
    {
        [SerializeField] private Vector3 _leftOffset = new Vector3(-2f, 0, 0);
        [SerializeField] private Vector3 _forwardOffset = new Vector3(0, 0, 2f);
        [SerializeField] private Vector3 _forwardBoxHalfExtents = new Vector3(1f, 1f, 1f);
        [SerializeField] private Vector3 _leftBoxHalfExtents = new Vector3(1f, 1f, 0.5f);
        [SerializeField] private float _maxDistance = 1f;

        public bool CanRunToFirstRoad()
        {
            return !(IsRunnerInBox(Vector3.right, Vector3.left, _leftBoxHalfExtents, 1) || IsRunnerInBox(Vector3.back, Vector3.forward, _forwardBoxHalfExtents, 1));
        }

        public bool TryGetRunner(int road, out BodyCalculator runner)
        {
            var globalDirection = transform.TransformDirection(Vector3.forward);
            var globalOffset = transform.TransformDirection(Vector3.back);
            if(Physics.BoxCast(transform.position + globalOffset, _forwardBoxHalfExtents, globalDirection, out RaycastHit hit, transform.rotation,
            _maxDistance))
            {
                var foundRunner = hit.collider.GetComponent<Runner>();
                if(foundRunner != null && foundRunner.Road == road)
                {
                    if(foundRunner.TryGetComponent<BodyCalculator>(out BodyCalculator body))
                    {
                        runner = body;
                        return true;
                    }
                }
            }
            runner = default(BodyCalculator);
            return false;
        }

        private bool IsRunnerInBox(Vector3 offset, Vector3 direction, Vector3 halfExtents, int road)
        {
            var globalDirection = transform.TransformDirection(direction);
            var globalOffset = transform.TransformDirection(offset);
            if(Physics.BoxCast(transform.position + globalOffset, halfExtents, globalDirection, out RaycastHit hit, transform.rotation,
            _maxDistance * 2))
            {
                var foundRunner = hit.collider.GetComponent<Runner>();
                /*if(foundRunner != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }*/
                if(foundRunner != null && foundRunner.Road == road)
                {
                    return true;
                }
            }
            return false;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var offset = Vector3.zero;
            offset.z += _forwardBoxHalfExtents.z;
            var globalDirection = transform.TransformDirection(offset);
            var boxPosition = transform.position + globalDirection;
            Gizmos.DrawWireCube(boxPosition, _forwardBoxHalfExtents * 2);

            Gizmos.color = Color.green;
            offset = Vector3.zero;
            offset.x -= _leftBoxHalfExtents.x;
            globalDirection = transform.TransformDirection(offset);
            boxPosition = transform.position + globalDirection;
            Gizmos.DrawWireCube(boxPosition, _leftBoxHalfExtents * 2);
        }
    }
}
