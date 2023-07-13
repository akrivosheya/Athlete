using System.Collections.Generic;

using Managers;

namespace Dialogue.Conditions
{
    public class DisqualifiedChecker : IConditionChecker
    {
        public DisqualifiedChecker(List<string> empty)
        {
        }

        public bool IsMet()
        {
            return ManagersService.Race.IsDisqualified;
        }
    }
}
