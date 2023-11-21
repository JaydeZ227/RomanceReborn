using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour
{
    public Sprite YesIcon, NoIcon;
    bool isAuto = false;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = NoIcon;
        GetComponent<Button>().onClick.AddListener(()=> 
        {
            if (isAuto)
            {
                PlotController.Instance.autoPlayLayer--;
                GetComponent<Image>().sprite=NoIcon;
                isAuto = false;
            }
            else
            {
                PlotController.Instance.autoPlayLayer++;
                GetComponent<Image>().sprite = YesIcon;
                isAuto = true;
            }
        });    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
