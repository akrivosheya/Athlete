using System.Collections.Generic;
using UnityEngine;

using Dialogue.DTO;
using Dialogue.Conditions;
using Dialogue.Behaviours;
using Exceptions;
using Factory;

namespace Dialogue
{
    public class DialoguesLoader
    {
        private readonly string _textPath = "Dialogues/Texts/";
        private readonly string _treePath = "Dialogues/";

        public DialogueNode LoadDialogueTree(string fileName)
        {
            string fullPath = _treePath + fileName;
            TextAsset json = Resources.Load<TextAsset>(fullPath);
            var nodes = new Dictionary<string, DialogueNode>();
            DialogueNode? firstNode = null;
            try
            {
                var treeDTO = JsonUtility.FromJson<TreeDTO>(json.ToString());
                foreach(var node in treeDTO.Nodes)
                {
                    var behaviours = GetObjectsList<IBehaviour>(node.Behaviours);
                    var newNode = new DialogueNode(node.Name, behaviours, this);
                    if(firstNode is null)
                    {
                        firstNode = newNode;
                    }
                    nodes.Add(node.Name, newNode);
                }
                if(firstNode is null)
                {
                    throw new DialogueTreeLoadingException(fullPath, "tree doesn't have nodes.");
                }
                foreach(var branch in treeDTO.Branches)
                {
                    CheckBranchNode(branch.NodeFromName, nodes, fullPath);
                    CheckBranchNode(branch.NodeToName, nodes, fullPath);
                    var conditions = GetObjectsList<IConditionChecker>(branch.Conditions);
                    var newBranch = new DialogueBranch(nodes[branch.NodeToName], conditions);
                    nodes[branch.NodeFromName].DialogueBranches.Add(newBranch);
                }
            }
            catch(FactoryException exception)
            {
                throw new DialogueTreeLoadingException(fullPath, exception.Message);
            }
            catch(System.Exception exception)
            {
                throw new DialogueTreeLoadingException(fullPath, exception.Message);
            }
            return firstNode;
        }

        public List<DialogueText> LoadDialogueTexts(string fileName)
        {
            string fullPath = _textPath + fileName;
            TextAsset json = Resources.Load<TextAsset>(fullPath);
            var dialogueTexts = new List<DialogueText>();
            try
            {
                var dialogueTextContainer = JsonUtility.FromJson<DialogueTextContainerDTO>(json.ToString());
                foreach(var textDTO in dialogueTextContainer.Texts)
                {
                    var dialogueText = new DialogueText(textDTO);
                    dialogueTexts.Add(dialogueText);
                }
            }
            catch(System.Exception exception)
            {
                throw new DialogueTextLoadException(fullPath, exception);
            }
            return dialogueTexts;
        }

        private void CheckBranchNode(string nodeName, Dictionary<string, DialogueNode> nodes, string fileName)
        {
            if(!nodes.ContainsKey(nodeName))
            {
                throw new DialogueTreeLoadingException(fileName, "branch refer to node + " + nodeName + " that doesn't exist.");
            }
        }

        private List<T> GetObjectsList<T>(List<InitializatorDTO> initializators) where T : class
        {
            var objects = new List<T>();
            foreach(var initializator in initializators)
            {
                var newObject = Factory<T>.Instance.GetObject(initializator.Name, initializator.Params);
                objects.Add(newObject);
            }
            return objects;
        }
    }
}
