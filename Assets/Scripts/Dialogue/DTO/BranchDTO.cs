using System.Collections.Generic;

namespace Dialogue.DTO
{
    [System.Serializable]
    public class BranchDTO
    {
        public string NodeFromName;
        public string NodeToName;
        public List<InitializatorDTO> Conditions;
    }
}
