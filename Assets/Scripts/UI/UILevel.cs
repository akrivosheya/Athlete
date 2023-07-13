using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using ScriptableObjects;
using Data;
using Managers;
using Formatters;

namespace UI
{
    public class UILevel : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
    {
        public int Index { get; set; }
        [SerializeField] private LevelsDataSO _levels;
        [SerializeField] private string _defaultDescription = "Now this level is not available";
        [SerializeField] private string _categoryString = "Category: ";
        [SerializeField] private string _timeString = "Time: ";
        [SerializeField] private string _emptyString = "";
        private LevelData _level;
        private Text _description;

        void Start()
        {
            _level = _levels.GetLevelData(Index);
            _description = GameObject.FindWithTag(Constraints.Tags.Description).GetComponent<Text>();
        }

        /*public void OnPointerEnter(PointerEventData eventData)
        {
            if(Index > 0 && !_levels.GetLevelData(Index - 1).IsCompleted)
            {
                _description.text = _defaultDescription;
            }
            else
            {
                var description = new StringBuilder();
                description.Append(_level.Description);
                description.Append('\n');
                description.Append(_timeString);
                if(_level.IsCompleted)
                {
                    description.Append(TimeFormatter.GetTimeFormat(_level.Time));
                    if(_level.Category > 0)
                    {
                        description.Append('\n');
                        description.Append(_categoryString);
                        description.Append(_level.Category);
                    }
                }
                else
                {
                    description.Append('-');
                }
                _description.text  = description.ToString();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _description.text = _emptyString;
        }*/

        public void OnClick()
        {
            ManagersService.Race.Race = Index;
            ManagersService.Level.LoadScene(_level.Scene, (StatesManager.GameStates)_level.State);
        }
    }
}
