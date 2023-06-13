using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace UI
{
    public class UIDialogue : MonoBehaviour
    {
        [SerializeField] private GameObject _font;
        [SerializeField] private Text _text;
        [SerializeField] private Text _personName;
        [SerializeField] private Image _firstPerson;
        [SerializeField] private Image _secondPerson;
        [SerializeField] private Image _centralImage;
        [SerializeField] private Color _noActiveColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private string _filePath = "Sprites/";
        [SerializeField] private float _waitSeconds = 0.2f;

        void Start()
        {
            _font.SetActive(false);
            _centralImage.gameObject.SetActive(false);
            _firstPerson.gameObject.SetActive(false);
            _secondPerson.gameObject.SetActive(false);
        }

        public void StartDialogue()
        {
            _font.SetActive(true);
            _centralImage.gameObject.SetActive(true);
            _firstPerson.gameObject.SetActive(true);
            _secondPerson.gameObject.SetActive(true);
            ChangeDialogue();
        }

        public void ChangeDialogue()
        {
            var dialogue = ManagersService.Dialogue;
            if(dialogue.CanChangeText)
            {
                _text.text = dialogue.CurrentText;
            }
            else
            {
                _personName.text = dialogue.CurrentPerson;

                _firstPerson.sprite = GetSprite(dialogue.MainPersonImage);
                _firstPerson.color = (dialogue.MainPersonIsActive) ? _activeColor : _noActiveColor;

                _secondPerson.sprite = GetSprite(dialogue.SecondPersonImage);
                _secondPerson.color = (dialogue.SecondPersonIsActive) ? _activeColor : _noActiveColor;

                _centralImage.sprite = GetSprite(dialogue.CentralImage);

                StartCoroutine(WriteText(dialogue));
            }
        }

        public void EndDialogue()
        {
            _font.SetActive(false);
            _centralImage.gameObject.SetActive(false);
            _firstPerson.gameObject.SetActive(false);
            _secondPerson.gameObject.SetActive(false);
        }

        private Sprite GetSprite(string spriteName)
        {
            var sprite = Resources.Load<Sprite>(_filePath + spriteName);
            if(sprite is null)
            {
                Debug.Log("Can't load " + _filePath + "\"" + spriteName + "\"");
                return _defaultSprite;
            }
            return sprite;
        }

        private IEnumerator WriteText(DialogueManager dialogue)
        {
            StringBuilder text = new StringBuilder();
            int i = 0;

            while(i < dialogue.CurrentText.Length)
            {
                if(dialogue.CanChangeText)
                {
                    break;
                }

                text.Append(dialogue.CurrentText[i]);
                _text.text = text.ToString();
                i++;
                
                yield return new WaitForSeconds(_waitSeconds);
            }

            dialogue.CanChangeText = true;
        }
    }
}
