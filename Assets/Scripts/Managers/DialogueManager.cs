using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dialogue;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        public string CurrentText { get { return _currentDialogueNode.CurrentText; } }
        public string CurrentPerson { get { return _currentDialogueNode.CurrentPerson; } }
        public string MainPersonImage { get { return _currentDialogueNode.MainPersonImage; } }
        public string SecondPersonImage { get { return _currentDialogueNode.SecondPersonImage; } }
        public string CentralImage { get { return _currentDialogueNode.CentralImage; } }
        public bool MainPersonIsActive { get { return _currentDialogueNode.MainPersonIsActive; } }
        public bool SecondPersonIsActive { get { return _currentDialogueNode.SecondPersonIsActive; } }
        public bool CanChangeText { get; set; }
        [SerializeField] private float _waitTimeSeconds = 0.1f;
        private Dictionary<string, DialogueNode> _dialogueTrees = new Dictionary<string, DialogueNode>();
        private DialoguesFileFormatter _formatter = new DialoguesFileFormatter();
        private DialoguesLoader _loader = new DialoguesLoader();
        private DialogueNode _currentDialogueNode;

        public void StartDialogue(string pointName, string sceneName)
        {
            string key = _formatter.GetKey(pointName, sceneName);
            if(_dialogueTrees.ContainsKey(key))
            {
                StartCoroutine(ConductDialogue(_dialogueTrees[key]));
            }
            else
            {
                DialogueNode dialogueNode = _loader.LoadDialogueTree(key);
                _dialogueTrees.Add(key, dialogueNode);
                StartCoroutine(ConductDialogue(dialogueNode));
            }
        }

        private IEnumerator ConductDialogue(DialogueNode dialogueNode)
        {
            ManagersService.States.CurrentState = StatesManager.GameStates.Dialogue;
            _currentDialogueNode = dialogueNode;
            _currentDialogueNode.Reset();
            CanChangeText = false;
            Messenger.Broadcast(Events.StartedDialogue);
            yield return new WaitForSeconds(_waitTimeSeconds);

            while(true)
            {
                if(Input.anyKeyDown && !ManagersService.States.CurrentStateIsPause)
                {
                    if(!CanChangeText)
                    {
                        CanChangeText = true;
                    }
                    else if(_currentDialogueNode.HasNextText)
                    {
                        _currentDialogueNode.SwitchText();
                        CanChangeText = false;
                    }
                    else if(_currentDialogueNode.IsLast())
                    {
                        break;
                    }
                    else
                    {
                        _currentDialogueNode = _currentDialogueNode.NextDialogueNode();
                        _currentDialogueNode.Reset();
                        CanChangeText = false;
                    }
                    Messenger.Broadcast(Events.ChangedText);
                    yield return new WaitForSeconds(_waitTimeSeconds);
                }
                else
                {
                    yield return null;
                }
            }

            Messenger.Broadcast(Events.EndDialogue);
            ManagersService.States.CurrentState = StatesManager.GameStates.Walking;
        }
    }
}
