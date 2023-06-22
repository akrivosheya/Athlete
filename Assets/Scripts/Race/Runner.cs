using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Runner : MonoBehaviour
    {
        public List<float> Times { get; set; } = new List<float>();
        public int RunPoints { get; set; } = 0;
        public int Road { get; set; } = 0;
        public bool CanChangeRoad { get; set; } = false;
        public bool IsDisqualified { get; set; } = false;
    }
}
