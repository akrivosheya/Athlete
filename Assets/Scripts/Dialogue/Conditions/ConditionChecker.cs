using System.Collections.Generic;

namespace Dialogue.Conditions
{
    public class ConditionChecker : IConditionChecker
    {
        private List<string> _conditions;

        public ConditionChecker(List<string> conditions)
        {
            _conditions = conditions;
        }

        public bool IsMet()
        {
            foreach(var condition in _conditions)
            {
                if(!Managers.ManagersService.Conditions.CheckCondition(condition))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
