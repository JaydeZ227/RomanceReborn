using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerSimpleEvent : MonoBehaviour
{
    public PlotSO plotSO;
    public ChooseSO chooseSO;
    public void LoadNextSay(string nextSay)
    {
        PlotController.Instance.SetSay(plotSO.GetPlot(nextSay).plots);
    }
    public void LoadNextChoose(string nextChoose)
    {
        PlotController.Instance.chooseSystem.SetChoose(chooseSO.GetChoose(nextChoose).choose);
    }
    public void LoadMusic(string music)
    {
        MusicController.Instance.PlayEffectByFrame(Resources.Load<AudioClip>("Music/"+music));
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
