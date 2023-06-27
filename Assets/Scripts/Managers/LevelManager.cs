using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public string CurrentSceneName { get { return SceneManager.GetActiveScene().name; } }
        private string _previousScene;
        private string _sceneToLoad = "";
        private StatesManager.GameStates _nextSceneState;
        private StatesManager.GameStates _previousSceneState;

        void Awake()
        {
            Messenger.AddListener(Events.ClearedDarkness, OnClearedDarkness);
            Messenger.AddListener(Events.MadeDarkness, OnMadeDarkness);
        }

        void Start()
        {
            ManagersService.States.CurrentState = StatesManager.GameStates.Menu;
            ManagersService.States.CurrentState = StatesManager.GameStates.Loading;
        }

        void OnDestroy()
        {
            Messenger.RemoveListener(Events.ClearedDarkness, OnClearedDarkness);
            Messenger.RemoveListener(Events.MadeDarkness, OnMadeDarkness);
        }

        public void LoadScene(string scene, StatesManager.GameStates state)
        {
            _previousScene = CurrentSceneName;
            _previousSceneState = ManagersService.States.CurrentState;
            _sceneToLoad = scene;

            ManagersService.States.CurrentState = state;
            ManagersService.States.CurrentState = StatesManager.GameStates.Loading;

            Messenger.Broadcast(Events.LoadScene);
        }

        public void LoadPreviousScene()
        {
            LoadScene(_previousScene, _previousSceneState);
        }

        public void ImmediatelyLoadScene(string scene)
        {
            _previousScene = CurrentSceneName;
            SceneManager.LoadScene(scene);
        }

        private void OnClearedDarkness()
        {
            ManagersService.States.RollbackState();
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
