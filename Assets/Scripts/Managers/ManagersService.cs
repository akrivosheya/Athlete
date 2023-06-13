using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(StatesManager))]
    [RequireComponent(typeof(DialogueManager))]
    [RequireComponent(typeof(LevelManager))]
    public class ManagersService : MonoBehaviour
    {
        public static StatesManager States { get; private set; }
        public static DialogueManager Dialogue { get; private set; }
        public static LevelManager Level { get; private set; }
        [SerializeField] private string _firstScene;
        
        void Awake()
        {
            States = GetComponent<StatesManager>();
            Dialogue = GetComponent<DialogueManager>();
            Level = GetComponent<LevelManager>();

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            Level.ImmediatelyLoadScene(_firstScene);
        }
    }
}
