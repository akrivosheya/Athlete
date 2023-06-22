using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public string CurrentSceneName { get { return SceneManager.GetActiveScene().name; } }
        private string _previousScene;
        private string _sceneToLoad = "";

        void Awake()
        {
            Messenger.AddListener(Events.ClearedDarkness, OnClearedDarkness);
            Messenger.AddListener(Events.MadeDarkness, OnMadeDarkness);
        }

        void Start()
        {
            ManagersService.States.CurrentState = StatesManager.GameStates.Finish;
            ManagersService.States.CurrentState = StatesManager.GameStates.Loading;
        }

        void OnDestroy()
        {
            Messenger.RemoveListener(Events.ClearedDarkness, OnClearedDarkness);
            Messenger.RemoveListener(Events.MadeDarkness, OnMadeDarkness);
        }

        public void LoadScene(string scene)
        {
            _previousScene = CurrentSceneName;
            _sceneToLoad = scene;

            ManagersService.States.CurrentState = StatesManager.GameStates.Loading;

            Messenger.Broadcast(Events.LoadScene);
        }

        public void LoadPreviousScene()
        {
            LoadScene(_previousScene);
        }

        public void ImmediatelyLoadScene(string scene)
        {
            _previousScene = CurrentSceneName;
            SceneManager.LoadScene(scene);
        }

        private void OnClearedDarkness()
        {
            ManagersService.States.RollbackState();
            ManagersService.States.SwitchState();
            try
            {
                Messenger.Broadcast(Events.LoadedScene);
            }
            catch(MessengerInternal.BroadcastException)
            {
                Debug.Log("No listeners for " + Events.LoadedScene + " in scene " + _sceneToLoad);
            }
        }

        private void OnMadeDarkness()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
