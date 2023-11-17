using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseSystem : MonoBehaviour
{
    public Text titleText;
    public ChooseButton[] buttons;
    
    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            buttons[j].clickAction += () =>
            {
                gameObject.SetActive(false);
                if (SceneManager.GetActiveScene().name == "GameScene1")
                {
                    if (chooseItems[j].chooseAction != "")
                    {
                        GameController.Instance.SendMessage(chooseItems[j].chooseAction);
                    }
                    if (chooseItems[j].chooseAction2 != "")
                    {
                        GameController.Instance.SendMessage(chooseItems[j].chooseAction2);
                    }
                    if (chooseItems[j].chooseEndLoadSay != "")
                    {

                        PlotController.Instance.SetSay(GameController.Instance.plotSO.GetPlot(chooseItems[j].chooseEndLoadSay).plots);

                    }
                    if (chooseItems[j].chooseEndLoadChoose != "")
                    {
                        SetChoose(GameController.Instance.chooseSO.GetChoose(chooseItems[j].chooseEndLoadChoose).choose);
                    }
                }
                else if (SceneManager.GetActiveScene().name == "GameScene2")
                {
                    if (chooseItems[j].changeApathyCount != 0)
                    {
                        GameController2.Instance.ChangeApathyNum(chooseItems[j].changeApathyCount);
                    }
                    if (chooseItems[j].addTimeCount != 0)
                    {
                        GameController2.Instance.AddTime(chooseItems[j].addTimeCount);
                    }
                    if (chooseItems[j].changeTimeCount != 0)
                    {
                        GameController2.Instance.ChangeTime(chooseItems[j].changeTimeCount);
                    }

                    if (chooseItems[j].chooseAction != "")
                    {
                        GameController2.Instance.SendMessage(chooseItems[j].chooseAction);
                    }
                    if (chooseItems[j].chooseAction2 != "")
                    {
                        GameController2.Instance.SendMessage(chooseItems[j].chooseAction2);
                    }
                    if (chooseItems[j].chooseEndLoadSay != "")
                    {

                        PlotController.Instance.SetSay(GameController2.Instance.plotSO.GetPlot(chooseItems[j].chooseEndLoadSay).plots);

                    }
                    if (chooseItems[j].chooseEndLoadChoose != "")
                    {
                        SetChoose(GameController2.Instance.chooseSO.GetChoose(chooseItems[j].chooseEndLoadChoose).choose);
                    }
                
                }

                endAction?.Invoke();
            };
        }
    }
    ChooseItem[] chooseItems;
    public System.Action endAction;
    public void SetChoose(ChooseGame chooseGame)
    {
        if (chooseGame.endAction == "")
        {
            endAction = null;
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "GameScene1")
            {
                endAction = ()=> { GameController.Instance.SendMessage(chooseGame.endAction); };
            }
            else if (SceneManager.GetActiveScene().name == "GameScene2")
            {
                endAction = () => { GameController2.Instance.SendMessage(chooseGame.endAction); };
            }
                
        }
        gameObject.SetActive(true);
        titleText.text = chooseGame.titleContent;
        this.chooseItems = chooseGame.chooseItem;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < chooseGame.chooseItem.Length)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].SetText(chooseGame.chooseItem[i].name);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
[System.Serializable]
public class ChooseGame
{
    [TextArea(4,8)]
    public string titleContent;
  
    public ChooseItem[] chooseItem;
    public string endAction;
    
}
[System.Serializable]
public class ChooseItem
{
    public string name;
    public string chooseUpdateProp;
    public string chooseUpdateTip;
    public string chooseAction;
    public string chooseAction2;
    
    public string chooseEndLoadSay;
    public string chooseEndLoadChoose;
    public int changeApathyCount = 0;
    public int addTimeCount = 0;
    public float changeTimeCount = 0;
}
