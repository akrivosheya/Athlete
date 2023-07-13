using System.Collections.Generic;

using Exceptions;
using Dialogue.Behaviours;

namespace Dialogue
{
    public class DialogueNode
    {
        public List<DialogueBranch> DialogueBranches { get; private set; } = new List<DialogueBranch>();
        public bool HasNextText { get { return _currentTextIndex < _dialogueTexts.Count - 1; } }
        public string CurrentText { get { return _dialogueTexts[_currentTextIndex].Text; } }
        public string CurrentPerson { get { return _dialogueTexts[_currentTextIndex].Person; } }
        public string MainPersonImage { get { return _dialogueTexts[_currentTextIndex].MainPersonImage; } }
        public string SecondPersonImage { get { return _dialogueTexts[_currentTextIndex].SecondPersonImage; } }
        public string CentralImage { get { return _dialogueTexts[_currentTextIndex].CentralImage; } }
        public bool MainPersonIsActive { get { return _dialogueTexts[_currentTextIndex].MainPersonIsActive; } }
        public bool SecondPersonIsActive { get { return _dialogueTexts[_currentTextIndex].SecondPersonIsActive; } }
        public bool IsEmpty { get{ return _dialogueTexts.Count == 0; } }
        private List<IBehaviour> _behaviours;
        private List<DialogueText> _dialogueTexts;
        private string _name;
        private int _currentTextIndex = 0;

        public DialogueNode(string name, List<IBehaviour> behaviours, DialoguesLoader loader)
        {
            _name = name;
            _behaviours = behaviours;
            _dialogueTexts = loader.LoadDialogueTexts(_name);
        }

        public void Reset()
        {
            _currentTextIndex = 0;
            foreach(var behaviour in _behaviours)
            {
                behaviour.DoBehaviour();
            }
        }

        public void SwitchText()
        {
            _currentTextIndex++;
        }

        public bool IsLast()
        {
            if(DialogueBranches.Count == 0)
            {
                return true;
            }
            foreach(var branch in DialogueBranches)
            {
                if(branch.ConditionsAreMet())
                {
                    return false;
                }
            }
            return true;
        }

        public DialogueNode NextDialogueNode()
        {
            if(DialogueBranches.Count == 0)
            {
                throw new NoNextNodesException(_name);
            }
            foreach(var branch in DialogueBranches)
            {
                if(branch.ConditionsAreMet())
                {
                    return branch.Node;
                }
            }
            throw new NoNextNodesException(_name);;
        }
    }
}
