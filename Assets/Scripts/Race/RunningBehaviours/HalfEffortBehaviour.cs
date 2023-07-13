using System.Collections.Generic;

namespace Race.RunningBehaviours
{
    public class HalfEffortBehaviour : IRunningBehaviour
    {
        public HalfEffortBehaviour(List<string> emptyParameters)
        {}

        public bool CalcuateEffort(BodyCalculator body)
        {
            return body.Effort < body.HalfEffort;
        }
    }
}
