using System.Collections.Generic;

using Managers;

namespace Dialogue.Behaviours
{
    public class EndBehaviour : IBehaviour
    {
        private string _mainMenuScene = "MainMenu";

        public EndBehaviour(List<string> empty)
        {
        }

        public void DoBehaviour()
        {
            ManagersService.Conditions.Reset();
            ManagersService.Level.LoadScene(_mainMenuScene, StatesManager.GameStates.Menu);
        }
    }
}
