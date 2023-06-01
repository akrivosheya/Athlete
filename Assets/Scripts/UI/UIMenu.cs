using UnityEngine;

using Managers;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    void Start()
    {
        Hide();
    }

    public void Show()
    {
        ShowCursor();
        _menu.SetActive(true);
    }

    public void Hide()
    {
        HideCursor();
        _menu.SetActive(false);
    }

    public void OnContinue()
    {
        ManagersService.States.RollbackState();
        Hide();
    }

    public void OnExit()
    {
        Application.Quit();
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
