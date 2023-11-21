using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotController : MonoBehaviour
{
    static PlotController _instance;
    public static PlotController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlotController>();
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
   string nextSay;
    public void SetSay(PlotSayItem[] plots)
    {
        this.plots = plots;
        plotIndex = 0;
        say.gameObject.SetActive(true);
        LoadSay();
    }
    public string samePlayerStr="same_player";

    public float autoDelay = 0.5f;
    float autoTimer = 0;
    private void Update()
    {
        if (autoPlayLayer > 0 && say.gameObject.activeSelf)
        {
            if (plots[plotIndex].endChooseGameName == "")
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
                GameController.Instance.leftPlayer.SetState(GameController.Instance.same_player_cloth);
            }
            else
            {
                GameController.Instance.leftPlayer.SetState(plots[plotIndex].leftCharactorName);
            }
            if (plots[plotIndex].rightCharactorName == samePlayerStr)
            {
                GameController.Instance.rightPlayer.SetState(GameController.Instance.same_player_cloth);
            }
            else
            {

                GameController.Instance.rightPlayer.SetState(plots[plotIndex].rightCharactorName);
            }

            nextSay = plots[plotIndex].endNextSay;
            System.Action addAction = null;
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
        }
        else if(SceneManager.GetActiveScene().name == "GameScene2")
        {
            GameController2.Instance.bgIcon.SetState(plots[plotIndex].changeBgName);
            say.gameObject.SetActive(true);
            say.SetText(plots[plotIndex]);

            if (plots[plotIndex].leftCharactorName == samePlayerStr)
            {
               // GameController2.Instance.leftPlayer.SetState(GameController2.Instance.same_player_cloth);
            }
            else
            {
                GameController2.Instance.leftPlayer.SetState(plots[plotIndex].leftCharactorName);
            }
            if (plots[plotIndex].rightCharactorName == samePlayerStr)
            {
            //    GameController2.Instance.rightPlayer.SetState(GameController2.Instance.same_player_cloth);
            }
            else
            {

                GameController2.Instance.rightPlayer.SetState(plots[plotIndex].rightCharactorName);
            }

            nextSay = plots[plotIndex].endNextSay;
            System.Action addAction = null;
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
        }
    }
    public void NextSay()
    {
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            if (!say.isEnd)
            {
                say.ShowAll();
            }
            //use Next Action
            if (plots[plotIndex].endActionName_ToGameController != "")
            {
                GameController.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
            }

            if (nextSay == "")
            {
                plotIndex++;
            }
            else
            {

                SetSay(GameController.Instance.plotSO.GetPlot(nextSay).plots);
            }


            if (plotIndex < plots.Length)
            {
                LoadSay();
            }
            else
            {
                say.gameObject.SetActive(false);
                GameController.Instance.leftPlayer.SetState("");
                GameController.Instance.rightPlayer.SetState("");
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            if (!say.isEnd)
            {
                say.ShowAll();
            }
            //use Next Action
            if (plots[plotIndex].ApathyNumChange != 0)
            {
                GameController2.Instance.ChangeApathyNum(plots[plotIndex].ApathyNumChange);
                // GameController2.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
            }
            if (plots[plotIndex].endActionName_ToGameController != "")
            {
                GameController2.Instance.SendMessage(plots[plotIndex].endActionName_ToGameController);
            }
          
            if (nextSay == "")
            {
                plotIndex++;
            }
            else
            {

                SetSay(GameController2.Instance.plotSO.GetPlot(nextSay).plots);
            }


            if (plotIndex < plots.Length)
            {
                LoadSay();
            }
            else
            {
                say.gameObject.SetActive(false);
                GameController2.Instance.leftPlayer.SetState("");
                GameController2.Instance.rightPlayer.SetState("");
            }
        }
     
    }
}

[System.Serializable]
public class PlotSayItem : PlotItem
{
    public string changeBgName;//背景图片名字
    public string charactorName;//说话角色名字
    public int nameSetType = 0;
    public string leftCharactorName = "";
    public string rightCharactorName = "";
    
    [TextArea(4,10)]
    public string sayContent;//说话内容
  
    public string endChooseGameName;

    public string endActionName_ToGameController;

    public string endNextSay="";
    [Header("say end add prop to bag")]
    public string sayEndAddPropName = "";
    [Header("end lock tip")]
    public string sayEndGetTipName;
    [Header("end get charactor dic")]
    public string sayEndLockCharactor = "";
    public int ApathyNumChange;

}
[System.Serializable]
public class PlotItem
{

}