using System.Collections.Generic;

namespace Dialogue.Behaviours
{
    public class ConditionAdder : IBehaviour
    {
        private List<string> _conditions;

        public ConditionAdder(List<string> conditions)
        {
            _conditions = conditions;
        }

        public void DoBehaviour()
        {
            foreach(var condition in _conditions)
            {
                Managers.ManagersService.Conditions.AddCondition(condition);
            }
        }
    }
}
