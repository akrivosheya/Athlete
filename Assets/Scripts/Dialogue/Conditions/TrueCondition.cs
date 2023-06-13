namespace Dialogue.Conditions
{
    public class TrueCondition : IConditionChecker
    {
        public TrueCondition(){}

        public bool IsMet()
        {
            return true;
        }
    }
}
