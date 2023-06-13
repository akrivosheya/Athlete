using System.Collections.Generic;

using Dialogue.Conditions;

namespace Dialogue
{
    public class DialogueBranch
    {
        public DialogueNode Node { get; private set; }
        private List<IConditionChecker> _conditions;

        public DialogueBranch(DialogueNode Node, List<IConditionChecker> conditions)
        {
            this.Node = Node;
            _conditions = conditions;
        }

        public bool ConditionsAreMet()
        {
            foreach(var condition in _conditions)
            {
                if(!condition.IsMet())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
