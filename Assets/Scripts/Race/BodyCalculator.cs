using UnityEngine;

namespace Race
{
    public class BodyCalculator : MonoBehaviour
    {
        //public float LacticAcid { get; private set; } = 0f;
        //public float Effort { get; private set; } = 50f;
        [Range(0, 100f)] public float LacticAcid = 0f;
        [Range(50, 100)] public float Effort = 50f;
        public float HalfEffort { get => (_maxEffort + _minEffort) / 2; }
        [SerializeField] private RunningObject _object;
        [SerializeField] private float _kEffort = 0.05f;
        [SerializeField] private float _maxEffort = 100f;
        [SerializeField] private float _minEffort = 50f;
        [SerializeField] private float _effortRange = 1f;
        [SerializeField] private float _maxLacticAcid = 100f;
        [SerializeField] private float _minSpeed = 4f;
        [SerializeField] private float _maxSpeed = 8f;
        [SerializeField] private float _effortIncrease = 1f;
        [SerializeField] private float _effortDecrease = 4f;
        [SerializeField] private float _stamina = 100;
        [SerializeField] private float _staminaDecrease = 8f;

        public float GetSpeed(float currentMaxSpeed, bool madeEffort)
        {
            Effort += (madeEffort) ? _effortIncrease : 0;
            Effort -= _effortDecrease * (LacticAcid / _maxLacticAcid) * Time.deltaTime;
            float currentMaxEffort = (currentMaxSpeed >= _maxSpeed) ? _maxEffort : GetEffort(currentMaxSpeed);
            Effort = Mathf.Clamp(Effort, _minEffort, currentMaxEffort);
            float speed = GetSpeed();
            //Debug.Log(speed);
            LacticAcid += (Effort - _minEffort) * _kEffort * Time.deltaTime;
            _stamina -= (LacticAcid >= _maxLacticAcid && Effort >= HalfEffort) ? GetStaminaDecrease() : 0;
            if(_stamina <= 0)
            {
                _object.Die();
            }
            LacticAcid = Mathf.Clamp(LacticAcid, 0, _maxLacticAcid);

            return speed;
        }

        public bool CanRunFaster(float currentMaxSpeed, bool madeEffort)
        {
            float effort = Effort;
            effort += (madeEffort) ? _effortIncrease : 0;
            effort -= _effortDecrease * (LacticAcid / _maxLacticAcid) * Time.deltaTime;
            float currentMaxEffort = GetEffort(currentMaxSpeed);
            return effort > currentMaxEffort - _effortRange;
        }

        public float GetSpeed(bool madeEffort)
        {
            return GetSpeed(_maxSpeed, madeEffort);
        }

        public float GetSpeed()
        {
            float _lacticAcidImpact = GetLacticAcidImpactCoefficient();
            float limitSpeedManitude = (_maxSpeed - _minSpeed) * _lacticAcidImpact;
            return _minSpeed + (Effort - _minEffort) * (limitSpeedManitude / (_maxEffort - _minEffort));
        }

        public void DecreaseEffort()
        {
            Effort -= _effortDecrease;
            Effort = Mathf.Clamp(Effort, _minEffort, _maxEffort);
        }

        private float GetEffort(float speed)
        {
            float _lacticAcidImpact = GetLacticAcidImpactCoefficient();
            float limitSpeedManitude = (_maxSpeed - _minSpeed) * _lacticAcidImpact;
            return (speed - _minSpeed) * (_maxEffort - _minEffort) / limitSpeedManitude + _minEffort;
        }

        private float GetLacticAcidImpactCoefficient()
        {
            return (LacticAcid >= _maxLacticAcid / 2) ? _maxLacticAcid / 2 / LacticAcid : 1;
        }

        private float GetStaminaDecrease()
        {
            return _staminaDecrease * (Effort - HalfEffort) / (_maxEffort - HalfEffort) * Time.deltaTime;
        }
    }
}
