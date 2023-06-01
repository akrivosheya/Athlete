using UnityEngine;
using UnityEngine.UI;

public class UIKeys : MonoBehaviour
{
    [SerializeField] private GameObject _interactionKey;
    private bool _isShowingInteraction = false;

    void Start()
    {
        _interactionKey.SetActive(false);
    }

    void Update()
    {
        if(_isShowingInteraction)
        {
            _isShowingInteraction = false;
            return;
        }
        else if(_interactionKey.activeSelf)
        {
            _interactionKey.SetActive(false);
        }
    }

    public void ShowInteraction()
    {
        _interactionKey.SetActive(true);
        _isShowingInteraction = true;
    }
}
