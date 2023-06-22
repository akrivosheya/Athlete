namespace Race.RunningBehaviours
{
    public class HalfEffortBehaviour : IRunningBehaviour
    {
        public bool CalcuateEffort(BodyCalculator body)
        {
            return body.Effort < body.HalfEffort;
        }
    }
}
