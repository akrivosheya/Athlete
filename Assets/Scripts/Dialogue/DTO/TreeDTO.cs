using System.Collections;
using System.Collections.Generic;

namespace Dialogue.DTO
{
    [System.Serializable]
    public class TreeDTO
    {
        public List<DialogueNodeDTO> Nodes;
        public List<BranchDTO> Branches;
    }
}
