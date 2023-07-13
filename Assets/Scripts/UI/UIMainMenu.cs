using UnityEngine;

using ScriptableObjects;

namespace UI
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _mainWindow;
        /*[SerializeField] private GameObject _levelsWindow;
        [SerializeField] private LevelsDataSO _levels;
        [SerializeField] private UILevel _levelPrefab;
        [SerializeField] private Vector3 _firstPosition = new Vector3(-230, 150, 0);
        [SerializeField] private float _xOffset = 230f;
        [SerializeField] private float _yOffset = -50f;
        [SerializeField] private int _rowsCount = 5;
        [SerializeField] private int _columnsCount = 2;*/

        void Start()
        {
            /*for(int i = 0; i < _levels.Count; i++)
            {
                if(i / _rowsCount > _columnsCount)
                {
                    break;
                }
                var levelButton = Instantiate(_levelPrefab);
                levelButton.Index = i;

                var offset = Vector3.zero;
                offset.y = _yOffset * (i % _rowsCount);
                offset.x = _xOffset * (i / _rowsCount);

                levelButton.transform.SetParent(_levelsWindow.transform);
                levelButton.transform.localPosition = _firstPosition + offset;
            }
            _levelsWindow.SetActive(false);*/
            _mainWindow.SetActive(true);
        }

        public void OnClickLevels()
        {
            //_levelsWindow.SetActive(true);
            _mainWindow.SetActive(false);
        }

        public void OnClickBack()
        {
            //_levelsWindow.SetActive(false);
            _mainWindow.SetActive(true);
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}
