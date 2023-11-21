using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaySystem : MonoBehaviour
{
    public Text sayText;
    public Text nameText_Left;
    public Text nameText_Right;
    public GameObject nameLeftShow;
    public GameObject nameRightShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool isEnd = true;

    PlotSayItem plot;
    public void SetText(PlotSayItem plot)
    {
        this.plot = plot;
        if (plot.nameSetType == 0)
        {
            nameLeftShow.SetActive(false);
            nameRightShow.SetActive(false);
        }
        else if (plot.nameSetType==1)
        {
            nameText_Right.text = plot.charactorName;
            nameLeftShow.SetActive(false);
            nameRightShow.SetActive(true);
        }
        else if(plot.nameSetType==-1)
        {
            nameText_Left.text = plot.charactorName;
            nameLeftShow.SetActive(true);
            nameRightShow.SetActive(false);
        }
        
        playStr = plot.sayContent;
        sayText.text = "";
        if (jumpCor!=null)
        {
            StopCoroutine(jumpCor);
        }
        isEnd = false;
    
        jumpCor=StartCoroutine(jumpIE());
    }
    
    string playStr;
    public float playSpeed = 0.02f;
    Coroutine jumpCor=null;
    public System.Action endAction;
    public int stopLayer = 0;
    IEnumerator jumpIE()
    {
        for (int i = 0; i < playStr.Length; i++)
        {
            float speed = playSpeed;
          
            yield return new WaitForSeconds(speed);
            sayText.text = ""+ playStr.Substring(0,i+1);
            if (stopLayer>0)
            {
                i--;
            }
        }
        isEnd = true;

        endAction?.Invoke();
    }
    public void Replay()
    {
        SetText(this.plot);
    }
    public void ShowAll()
    {
        if (isEnd)
        {
            return;
        }
        sayText.text = playStr;
        endAction?.Invoke();
        isEnd = true;
        if (jumpCor!=null)
        {
            StopCoroutine(jumpCor);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
