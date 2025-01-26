using System;
using DG.Tweening;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform _mainMenu;
    [SerializeField] private Transform _levelMenu;

    [SerializeField] private GameObject _quitButton;

    private bool _currentIsMain = true;

    private void Start()
    {
        #if UNITY_WEBGL
            _quitButton.SetActive(false);
        #endif
    }

    public void ToggleMenus()
    {
        if (_currentIsMain)
        {
            DOTween.Sequence()
                .Append(_mainMenu.transform.DOScale(Vector3.zero, 0.3f))
                .Append(_levelMenu.transform.DOScale(Vector3.one, 0.3f));
        }
        else
        {
            DOTween.Sequence()
                .Append(_levelMenu.transform.DOScale(Vector3.zero, 0.3f))
                .Append(_mainMenu.transform.DOScale(Vector3.one, 0.3f));
        }

        _currentIsMain = !_currentIsMain;
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
