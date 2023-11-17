using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSPButton : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler,IPointerClickHandler
{
    public UnityEvent touchEvent;
    public void OnPointerClick(PointerEventData eventData)
    {
        touchEvent?.Invoke();
    }
    public Sprite enterSP;
    public Sprite exitSP;
    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = enterSP;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = exitSP;
    }
    Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        img.sprite = exitSP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
