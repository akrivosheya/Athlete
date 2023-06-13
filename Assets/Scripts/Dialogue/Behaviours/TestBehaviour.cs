using UnityEngine;

namespace Dialogue.Behaviours
{
    public class TestBehaviour : IBehaviour
    {
        private string _text;

        public TestBehaviour(string text)
        {
            _text = text;
        }

        public void DoBehaviour()
        {
            Debug.Log(_text);
        }
    }
}
