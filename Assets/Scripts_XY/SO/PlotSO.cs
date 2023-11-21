using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Plots",menuName ="SO/Plot")]
public class PlotSO : ScriptableObject
{
    public PlotMessage[] plotMessages;
    public PlotMessage GetPlot(string plotName)
    {
        foreach (var item in plotMessages)
        {
            //Debug.Log("item:"+item.plotName+"|   |"+plotName);
            if (item.plotName==plotName)
            {
                return item;
            }
        }
        
        return null;
    }
    [System.Serializable]
    public class PlotMessage
    {
        public string plotName = "";
        public PlotSayItem[] plots;
    }
  
}
