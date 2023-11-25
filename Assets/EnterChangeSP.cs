using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnterChangeSP : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Sprite enterSP;
    public Sprite exitSP;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = enterSP;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = exitSP;
    }

    private void OnEnable()
    {
        GetComponent<Image>().sprite = exitSP;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
