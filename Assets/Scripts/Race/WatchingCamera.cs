using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class WatchingCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        public void StartWatching()
        {
            StartCoroutine(Watch());
        }

        private IEnumerator Watch()
        {
            while(true)
            {
                transform.LookAt(_target);
                yield return null;
            }
        }
    }
}
