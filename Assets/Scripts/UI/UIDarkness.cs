using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIDarkness : MonoBehaviour
    {
        [SerializeField] private Image _darkness;
        [SerializeField] private float _untransperancy = 1f;
        [SerializeField] private float _darknessSpeed = 1f;

        void Start()
        {
            var color = _darkness.color;
            color.a = _untransperancy;
            _darkness.color = color;

            ClearDarkness();
        }
        
        public void MakeDarkness()
        {
            StartCoroutine(ChangeTransperancy(1, Events.MadeDarkness));
        }

        public void ClearDarkness()
        {
            StartCoroutine(ChangeTransperancy(-1, Events.ClearedDarkness));
        }

        private IEnumerator ChangeTransperancy(float mul, string eventName)
        {
            var color = _darkness.color;
            float alpha = color.a;

            do
            {
                alpha += mul * _darknessSpeed * Time.deltaTime;
                color.a = alpha;
                _darkness.color = color;

                yield return null;
            } while( !(alpha < 0 || alpha > 1f) );

            Messenger.Broadcast(eventName);
        }
    }
}
