using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour,IPointerClickHandler
{
    public Text nameText;
    public System.Action clickAction;
    public void SetText(string  name)
    {
        nameText.text = name;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        clickAction?.Invoke();
    }

  
}
