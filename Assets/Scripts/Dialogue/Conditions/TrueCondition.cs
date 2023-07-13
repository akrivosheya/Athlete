using System.Collections.Generic;

namespace Dialogue.Conditions
{
    public class TrueCondition : IConditionChecker
    {
        public TrueCondition(List<string> emptyParams){}

        public bool IsMet()
        {
            return true;
        }
    }
}
