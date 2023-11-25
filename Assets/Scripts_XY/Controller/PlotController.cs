using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotController : MonoBehaviour
{
    static PlotController _instance;
    EventHandlerToGameController eventHandle;
    public static PlotController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlotController>();
                _instance.eventHandle = GameObject.FindGameObjectWithTag("EventHandle").GetComponent<EventHandlerToGameController>();
            }
            return _instance;
        }
    }
    public int autoPlayLayer = 0;
    public SaySystem say;
    int plotIndex = 0;
    PlotSayItem[] plots ;
    public ChooseSystem chooseSystem;
    public GameObject[] chooseHide;
 //  string nextSay="";
    public void SetSay(PlotSayItem[] plots)
    {
        this.plots = plots;
        plotIndex = 0;
        say.gameObject.SetActive(true);
        LoadSay();
    }
    public string samePlayerStr="same_player";
    public List<string> jehaStrList = new List<string>() { };
    public float autoDelay = 0.5f;
    float autoTimer = 0;
    private void Update()
    {
        if (autoPlayLayer > 0 && say.gameObject.activeSelf)
        {
            if (!chooseSystem.gameObject.activeSelf)//(plots[plotIndex].endChooseGameName == "")
            {
                if (say.isEnd)
                {
                    autoTimer += Time.deltaTime;
                    if (autoTimer > autoDelay)
                    {
                        autoTimer = 0;
                        NextSay();
                    }
                }
                else
                {
                    autoTimer = 0;
                }
            }
            else
            {
                autoTimer = 0;
            }

        }
        else
        {
            autoTimer = 0;
        }
    
    }
    void LoadSay()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            GameController.Instance.bgIcon.SetState(plots[plotIndex].changeBgName);
            say.gameObject.SetActive(true);
            say.SetText(plots[plotIndex]);

            if (plots[plotIndex].leftCharactorName == samePlayerStr)
            {
                GameController.Instance.leftPlayer.SetState(GameController.Instance.GetPlayerFuShi());
            }
            else
            {
                GameController.Instance.leftPlayer.SetState(plots[plotIndex].leftCharactorName);
            }
            //Debug.Log(plots[plotIndex].middleCharactorName+""+ samePlayerStr);
            if (plots[plotIndex].middleCharactorName == samePlayerStr)
            {
                GameController.Instance.middlePlayer.SetState(GameController.Instance.GetPlayerFuShi());
            }
            else
            {

                GameController.Instance.middlePlayer.SetState(plots[plotIndex].middleCharactorName);
            }
            if (plots[plotIndex].rightCharactorName == samePlayerStr)
            {
                GameController.Instance.rightPlayer.SetState(GameController.Instance.GetPlayerFuShi());
            }
            else
            {

                GameController.Instance.rightPlayer.SetState(plots[plotIndex].rightCharactorName);
            }
            foreach (var item in plots[plotIndex].loadSayEvent)
            {
                eventHandle.SendMassageToGameController(item);
            }
            /*
            Debug.Log("²¥·ÅÒôÐ§"+ plots[plotIndex].startClip);
            if (plots[plotIndex].startClip != null)
            {
                
                MusicController.Instance.PlayEffectByFrame(plots[plotIndex].startClip);
            }
            */
            /*
            nextSay = plots[plotIndex].endNextSay;
            System.Action addAction = null;
            addAction += () =>
            {
                if (plots[plotIndex].endActionName_ToGameController != "")
                {
                    GameController2.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
                }
            };
            if (plots[plotIndex].sayEndAddPropName != "")
            {
                addAction += () =>
                {
                    GameController.Instance.SetPropToBag(plots[plotIndex].sayEndAddPropName);

                };
            }
            if (plots[plotIndex].sayEndGetTipName != "")
            {
                addAction += () =>
                {
                    GameController.Instance.SetTip(plots[plotIndex].sayEndGetTipName);
                };
            }
            if (plots[plotIndex].sayEndLockCharactor != "")
            {
                string cname = plots[plotIndex].sayEndLockCharactor;
                addAction += () =>
                {
                    Debug.Log("add to characrtor:" + cname);
                    GameController.Instance.SetCharatorToDic(plots[plotIndex].sayEndLockCharactor);
                };
            }
            if (plots[plotIndex].endChooseGameName == "")
            {
                foreach (var hide in chooseHide)
                {
                    hide.SetActive(true);
                }
                say.endAction = () =>
                {
                    addAction?.Invoke();
                };
            }
            else
            {
                foreach (var hide in chooseHide)
                {
                    hide.SetActive(false);
                }
                say.endAction = () =>
                {
                    chooseSystem.SetChoose(GameController.Instance.chooseSO.GetChoose(plots[plotIndex].endChooseGameName).choose);
                    addAction?.Invoke();
                };
            }
            */
            say.endAction = () =>
            {
                foreach (var item in plots[plotIndex].jumpEndEvent)
                {
                    eventHandle.SendMassageToGameController(item);
                }

            };
        }
        else if(SceneManager.GetActiveScene().name == "GameScene2")
        {
            if (plots[plotIndex].changeBgName == "")
            {

            }
            else if (plots[plotIndex].changeBgName == "curBG")
            {

            }
            else
            {
                GameController2.Instance.bgIcon.SetState(plots[plotIndex].changeBgName);
            }
            /*
            Debug.Log("²¥·ÅÒôÐ§" + plots[plotIndex].startClip);
            
            if (plots[plotIndex].startClip != null)
            {

                MusicController.Instance.PlayEffectByFrame(plots[plotIndex].startClip);
            }
            */

            say.gameObject.SetActive(true);
            say.SetText(plots[plotIndex]);

            if (plots[plotIndex].leftCharactorName == samePlayerStr)
            {
                GameController2.Instance.leftPlayer.SetState(GameController2.Instance.GetPlayerFuShi());
            }
            else if (plots[plotIndex].leftCharactorName == GameController2.Instance.player_zombie)
            {
                GameController2.Instance.leftPlayer.SetState(GameController2.Instance.GetPlayerZombie());
            }
            else if (jehaStrList.Contains( plots[plotIndex].leftCharactorName ))
            {
                GameController2.Instance.leftPlayer.SetState(GameController2.Instance.GetJehaNameByApathyNum(plots[plotIndex].leftCharactorName));
            }
            else
            {
                GameController2.Instance.leftPlayer.SetState(plots[plotIndex].leftCharactorName);
            }

            if (plots[plotIndex].rightCharactorName == samePlayerStr)
            {
                GameController2.Instance.rightPlayer.SetState(GameController2.Instance.GetPlayerFuShi());
            }
            else if (jehaStrList.Contains(plots[plotIndex].rightCharactorName))
            {
                GameController2.Instance.leftPlayer.SetState(GameController2.Instance.GetJehaNameByApathyNum(plots[plotIndex].rightCharactorName));
            }
            else if (plots[plotIndex].rightCharactorName == GameController2.Instance.player_zombie)
            {
                GameController2.Instance.rightPlayer.SetState(GameController2.Instance.GetPlayerZombie());
            }
            else
            {

                GameController2.Instance.rightPlayer.SetState(plots[plotIndex].rightCharactorName);
            }
            if (plots[plotIndex].middleCharactorName == samePlayerStr)
            {
                GameController2.Instance.middlePlayer.SetState(GameController2.Instance.GetPlayerFuShi());
            }
            else if(plots[plotIndex].middleCharactorName == GameController2.Instance.player_zombie)
            {
                GameController2.Instance.middlePlayer.SetState(GameController2.Instance.GetPlayerZombie());
            }
            else if (jehaStrList.Contains(plots[plotIndex].middleCharactorName))
            {
                GameController2.Instance.leftPlayer.SetState(GameController2.Instance.GetJehaNameByApathyNum(plots[plotIndex].middleCharactorName));
            }
            else
            {

                GameController2.Instance.middlePlayer.SetState(plots[plotIndex].middleCharactorName);
            }
            foreach (var item in plots[plotIndex].loadSayEvent)
            {
                eventHandle.SendMassageToGameController(item);
            }
            say.endAction = () =>
            {
                foreach (var item in plots[plotIndex].jumpEndEvent)
                {
                    eventHandle.SendMassageToGameController(item);
                }
                /*
                chooseSystem.SetChoose(GameController2.Instance.chooseSO.GetChoose(plots[plotIndex].endChooseGameName).choose);
                addAction?.Invoke();
                */
            };
            /*
            nextSay = plots[plotIndex].endNextSay;
            System.Action addAction = null;
            
            addAction += () =>
            {
                if (plots[plotIndex].jumpTextEndAction_ToGameController != "")
                {
                    GameController2.Instance.SendMessage(plots[plotIndex].jumpTextEndAction_ToGameController);
                }
            };

            if (plots[plotIndex].sayEndAddPropName != "")
            {
                addAction += () =>
                {
                    GameController2.Instance.SetPropToBag(plots[plotIndex].sayEndAddPropName);
                };
            }
            if (plots[plotIndex].sayEndGetTipName != "")
            {
                addAction += () =>
                {
                    GameController2.Instance.SetTip(plots[plotIndex].sayEndGetTipName);
                };
            }
            if (plots[plotIndex].sayEndLockCharactor != "")
            {
                string cname = plots[plotIndex].sayEndLockCharactor;
                
                addAction += () =>
                {
                    Debug.Log("add to characrtor:" + cname);
                    GameController2.Instance.SetCharatorToDic(plots[plotIndex].sayEndLockCharactor);
                };
                
            }
           
            if (plots[plotIndex].endChooseGameName == "")
            {
                foreach (var hide in chooseHide)
                {
                    hide.SetActive(true);
                }
                say.endAction = () =>
                {
                    addAction?.Invoke();
                };
            }
            else
            {
                foreach (var hide in chooseHide)
                {
                    hide.SetActive(false);
                }
                say.endAction = () =>
                {
                    chooseSystem.SetChoose(GameController2.Instance.chooseSO.GetChoose(plots[plotIndex].endChooseGameName).choose);
                    addAction?.Invoke();
                };
            }
            */
            foreach (var hide in chooseHide)
            {
                hide.SetActive(!plots[plotIndex].IsJumpEndChoose());
            }
          
        }
    }
    public void TouchSay()
    {
        if (!say.isEnd)
        {
            say.ShowAll();
            return;
        }
        NextSay();
    }
    public void NextSay()
    {
        if (!say.isEnd)
        {
            say.ShowAll();
        }
        var thisPlot = plots[plotIndex];
       
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
          
            //use Next Action
            /*
            if (plots[plotIndex].endActionName_ToGameController != "")
            {
                GameController.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
            }
            */
           // if (nextSay == "")
           // {
                plotIndex++;
          // }
          //  else
          //  {

          //      SetSay(GameController.Instance.plotSO.GetPlot(nextSay).plots);
          //  }


            if (plotIndex < plots.Length)
            {
                LoadSay();
            }
            else
            {
                say.gameObject.SetActive(false);
                GameController.Instance.leftPlayer.SetState("");
                GameController.Instance.middlePlayer.SetState("");
                GameController.Instance.rightPlayer.SetState("");
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            

                //use Next Action

                /*
                if (thisPlot.ApathyNumChange != 0)
                {
                    GameController2.Instance.ChangeApathyNum(thisPlot.ApathyNumChange);
                    // GameController2.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
                }

                if (thisPlot.addTimeCount != 0)
                {
                    GameController2.Instance.AddTime(thisPlot.addTimeCount);
                    // GameController2.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
                }
                if (thisPlot.SetTimeCount != 0)
                {
                    GameController2.Instance.ChangeTime(thisPlot.SetTimeCount);
                    // GameController2.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
                }
                */
                // Debug.Log("NextSay"+ nextSay);
                //if (nextSay == "")
                //  {
                plotIndex++;
          //  }
          //  else
         //  {

              //  SetSay(GameController2.Instance.PlotSO.GetPlot(nextSay).plots);
          //  }


            if (plotIndex < plots.Length)
            {
                LoadSay();
            }
            else
            {
                say.gameObject.SetActive(false);
                GameController2.Instance.leftPlayer.SetState("");
                GameController2.Instance.middlePlayer.SetState("");
                GameController2.Instance.rightPlayer.SetState("");
            }
            /*
            if (thisPlot.endActionName_ToGameController != "")
            {
                GameController2.Instance.SendMessage(thisPlot.endActionName_ToGameController);
            }
            */
        }
        foreach (var item in thisPlot.touchNextEvent)
        {
            eventHandle.SendMassageToGameController(item);
        }
    }
}

[System.Serializable]
public class PlotSayItem : PlotItem
{
    [TextArea(4, 10)]
    public string sayContent;//说话内容
    public FontStyle fontStyle = FontStyle.Normal;
    public string changeBgName;//换背景
    public string charactorName;//人物名字
    public int nameSetType = 0;
    public string leftCharactorName = "";
    public string middleCharactorName = "";
    public string rightCharactorName = "";
    
   /*
  
    public string endChooseGameName;

    public string endActionName_ToGameController;
    public string jumpTextEndAction_ToGameController;
    public string endNextSay="";
    [Header("say end add prop to bag")]
    public string sayEndAddPropName = "";
    [Header("end lock tip")]
    public string sayEndGetTipName;
    [Header("end get charactor dic")]
    public string sayEndLockCharactor = "";

    public int addTimeCount;
    public int SetTimeCount = 0;
    public int ApathyNumChange;
    */
  //  public AudioClip startClip;
    public MethodEvent[] loadSayEvent;
    public MethodEvent[] jumpEndEvent;
    public MethodEvent[] touchNextEvent;
    public bool IsJumpEndChoose()
    {
        bool isContine = false;
        foreach (var item in jumpEndEvent)
        {
            if (item.methodName== "LoadNextChoose")
            {
                isContine = true;
            }
        }
        return isContine;
    }
}
[System.Serializable]
public class PlotItem
{

}