using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchAction : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent useAction;
    public void OnPointerClick(PointerEventData eventData)
    {
        useAction?.Invoke();
    }
}
