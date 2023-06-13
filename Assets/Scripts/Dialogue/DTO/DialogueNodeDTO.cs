using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.DTO
{
    [System.Serializable]
    public class DialogueNodeDTO
    {
        public string Name;
        public List<InitializatorDTO> Behaviours;
    }
}
