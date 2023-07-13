using System.Collections.Generic;

using Managers;

namespace Dialogue.Conditions
{
    public class DeadChecker : IConditionChecker
    {
        public DeadChecker(List<string> empty)
        {
        }

        public bool IsMet()
        {
            return ManagersService.Race.IsDead;
        }
    }
}
