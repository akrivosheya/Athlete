using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Race;
using Formatters;

namespace UI
{
    public class UIIndicators : MonoBehaviour
    {
        [SerializeField] private BodyCalculator _body;
        [SerializeField] private Runner _runner;
        [SerializeField] private Slider _lacticAcidIndicator;
        [SerializeField] private Slider _effortIndicator;
        [SerializeField] private Text _coachScream;
        [SerializeField] private float _waitSeconds = 2f;

        void Awake()
        {
            Messenger.AddListener(Events.PlayerRunTimePoint, OnPlayerRunTimePoint);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener(Events.PlayerRunTimePoint, OnPlayerRunTimePoint);
        }

        void Start()
        {
            _coachScream.text = "";
        }

        void Update()
        {
            _lacticAcidIndicator.value = _body.LacticAcid;
            _effortIndicator.value = _body.Effort;
        }

        private void OnPlayerRunTimePoint()
        {
            var time = _runner.Times[_runner.Times.Count - 1];
            _coachScream.text = TimeFormatter.GetTimeFormat(time) + '!';

            StartCoroutine(HideCouchScream());
        }

        private IEnumerator HideCouchScream()
        {
            yield return new WaitForSeconds(_waitSeconds);

            _coachScream.text = "";
        }
    }
}
