using System;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Window _uiWindow;

    public void OnPointerClick(PointerEventData eventData)
    {
        _uiWindow.gameObject.SetActive(true);
        _uiWindow.Focus();
    }
}
