using System;
using System.Collections.Generic;
using Machines;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlicerScrollbarController: MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private SlicerController _machine;

    public void OnPointerUp(PointerEventData eventData)
    {
        _machine.OnSliderValueChanged(GetComponent<Scrollbar>());
    }
}
