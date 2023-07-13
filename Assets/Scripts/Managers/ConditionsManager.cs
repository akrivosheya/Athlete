using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ConditionsManager : MonoBehaviour
    {
        private Dictionary<string, bool> _conditions = new Dictionary<string, bool>();

        public void Reset()
        {
            _conditions.Clear();
        }

        public bool CheckCondition(string condition)
        {
            if(!_conditions.ContainsKey(condition))
            {
                return false;
            }
            return _conditions[condition];
        }

        public void AddCondition(string condition)
        {
            if(!_conditions.ContainsKey(condition))
            {
                _conditions.Add(condition, true);
            }
            else
            {
                _conditions[condition] = true;
            }
        }
    }
}
