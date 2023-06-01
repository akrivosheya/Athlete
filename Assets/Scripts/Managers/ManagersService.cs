using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(StatesManager))]
    public class ManagersService : MonoBehaviour
    {
        public static StatesManager States { get; private set; }
        
        void Awake()
        {
            States = GetComponent<StatesManager>();
        }

        void Update()
        {
            
        }
    }
}
