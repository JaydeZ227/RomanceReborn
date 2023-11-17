using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour
{
    public PlotSO plotSO;
    public ChooseSO chooseSO;
    public StateIcon_BG bgIcon;
    public HideToShow bgChangeHideCanvasGorup;
    public float changeBgHideTime = 1.5f;
    public int maxApathyNum = 12;
    public Text ApathyNumText;
    [SerializeField]
    int curApathyNum = 0;


    public void ChangeApathyNum(int changeNum)
    {
        curApathyNum += changeNum;
        if (curApathyNum <= 0)
        {
            curApathyNum = 0;
        }
        if (curApathyNum > maxApathyNum)
        {
            curApathyNum = maxApathyNum;
        }
        ApathyNumText.text = curApathyNum + "";
    }
    private void Awake()
    {
        bgChangeHideCanvasGorup.Open();
        bgIcon.onChange += () =>
        {
            cor = StartCoroutine(changeBGIE());
        };
        ApathyNumText.text = curApathyNum + "";
        timeText.text = ((int)(curTime / 3600)).ToString("#00") + ":" + ((int)(curTime % 3600 / 60)).ToString("#00");
    }
    Coroutine cor;
    IEnumerator changeBGIE()
    {
        Time.timeScale = 0;
        Debug.Log("切换场景");
        bgChangeHideCanvasGorup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        bgChangeHideCanvasGorup.Close();
        yield return new WaitForSecondsRealtime(changeBgHideTime);
        bgChangeHideCanvasGorup.OpenByAnim(() =>
        {
            Time.timeScale = 1;
            bgChangeHideCanvasGorup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        });



    }
    public StateIcon_Anim leftPlayer, rightPlayer;
    static GameController2 _instance;
    public static GameController2 Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController2>();
            }
            return _instance;
        }
    }

    public enum GameStateEnum
    {
        StartBedRoom, SchoolGate, MusicRoom, Gym, NurseOffice, ClassRoom, G

    }
    #region 时间
    public Text timeText;
    public float curTime = 6 * 60 * 60;
    public void AddTime(int h)
    {
        curTime += h * 60 * 60;
        while (curTime >= 24 * 60 * 60)
        {
            curTime -= 24 * 60 * 60;
        }
        timeText.text = ((int)(curTime / 3600)).ToString("#00") + ":" + ((int)(curTime % 3600 / 60)).ToString("#00");
    }
    public void ChangeTime(float time)
    {
        curTime = time;
        while (curTime >= 24 * 60 * 60)
        {
            curTime -= 24 * 60 * 60;
        }
        timeText.text = ((int)(curTime / 3600)).ToString("#00") + ":" + ((int)(curTime % 3600 / 60)).ToString("#00");
    }



    #endregion
    #region 地图跳转
    public string mapToMusicRoom_Plot = "MusicRoom0";
    public string mapToNurseOffice_Plot = "NurseOffice1_0";
    public string mapToGym_Plot = "Gmy1_0";
    public string mapToClassroom_Plot = "Classroom1_0";
    public void  LoadSceneByMap(string sceneName)
    {
        switch (sceneName)
        {
            case "NurseOffice":
                {
                    GameState = GameStateEnum.NurseOffice;
                }
                break;
            case "Gym":
                {
                    GameState = GameStateEnum.Gym;
                }
                break;
            case "MusicRoom":
                {
                    GameState = GameStateEnum.MusicRoom;
                }
                break;
            case "Classroom":
                {
                    GameState = GameStateEnum.ClassRoom;
                }
                break;
            default:
                break;
        }
    }
    public void ResetGameToSchoolGate()
    {
      
        GameState = GameStateEnum.SchoolGate;
    }
    public void CloseSayBar()
    {
        PlotController.Instance.say.gameObject.SetActive(false);
    }
    #endregion
    #region 第一个场景 黑屏闹钟
    public BackPage backPage;
    public string startLoadSay = "";
    public string startSceneName = "";
    public void ResetGameToHome()
    {
        GameState = GameStateEnum.StartBedRoom;
    }
    [Header("玩家的服饰")]
    string samePlayerStr = "";
    public string basePlayerStr = "player";
    public string HairAddStr1 = "MaWei";
    public string HairAddStr2 = "PiFa";
    public string clothAddStr1 = "XiaoFu";
    public string clothAddStr2 = "YunDongFu";
    public string GetPlayerFuShi()
    {
        return samePlayerStr;
    }
    public void AddHair1()
    {
        samePlayerStr += "_" + HairAddStr1;
    }
    public void AddHair2()
    {
        samePlayerStr += "_" + HairAddStr2;
    }
    public void AddCloth1()
    {
        samePlayerStr += "_" + clothAddStr1;
    }
    public void AddCloth2()
    {
        samePlayerStr += "_" + clothAddStr2;
    }
    public string busStop0_Say = "Choose_BusStop0";
    public void ChangeBedStopToChoose()
    {
        if (curApathyNum >= 2)
        {
            PlotController.Instance.SetSay(plotSO.GetPlot(busStop0_Say).plots);
        }
        else
        {
           // Debug.Log("进入学校");
            GameState = GameStateEnum.SchoolGate;
        }
    }
    public string SchoolGate_morn_BgStr = "SchoolGate_morn";
    public void LoadSchoolGate_mornBG()
    {
        bgIcon.SetState(SchoolGate_morn_BgStr);
    }
    /// <summary>
    /// 两个人同时变红 失败 最后冷漠+1
    /// </summary>
    public void ZombieAnimToDie()
    {
        ResetGameToHome();
        ChangeApathyNum(1);
    }
    #endregion
    #region Gym Scene
    public bool isEnteredGym = false;
    public string gymSayStr1="Gmy1_0";
    public string gymSayStr2= "Gmy1_1_1";
    public void SetEnterGym()
    {
        isEnteredGym = true;

    }
    #endregion
    GameStateEnum _gameState = GameStateEnum.StartBedRoom;
    
    public GameStateEnum GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            _gameState = value;
            switch (_gameState)
            {
                case GameStateEnum.StartBedRoom:
                    {
                        //初始化玩家服饰
                        samePlayerStr = basePlayerStr;


                        bgIcon.SetState(startSceneName);
                        backPage.SetBackground(2.0f,()=> 
                        {
                        PlotController.Instance.SetSay(plotSO.GetPlot(startLoadSay).plots);
                        });
                    }
                    break;
                case GameStateEnum.SchoolGate:
                    {
                        Debug.Log("进入学校门口");
                        CloseSayBar();
                        bgIcon.SetState(SchoolGate_morn_BgStr);
                        leftPlayer.SetState("");
                        rightPlayer.SetState("");
                    }
                    break;
                case GameStateEnum.MusicRoom:
                    {

                        PlotController.Instance.SetSay(plotSO.GetPlot(mapToMusicRoom_Plot).plots);

                    }
                    break;
                case GameStateEnum.Gym:
                    {
                        PlotController.Instance.SetSay(plotSO.GetPlot(mapToGym_Plot).plots);
                    }
                    break;
                case GameStateEnum.NurseOffice:
                    {
                        PlotController.Instance.SetSay(plotSO.GetPlot(mapToNurseOffice_Plot).plots);
                    }
                    break;
                case GameStateEnum.ClassRoom:
                    {
                        PlotController.Instance.SetSay(plotSO.GetPlot(mapToClassroom_Plot).plots);
                    }
                    break;
                case GameStateEnum.G:
                    break;
                default:
                    break;
            }
        }
    }

    public GameObject bagNew;

    public StateIcon tipState;
    public void SetTip(string s)
    {
        tipState.SetState(s);
    }

    public List<string> bagItemNameList = new List<string>();
    public void SetPropToBag(string s)
    {
        Debug.Log("add to bag:" + s);
        bagItemNameList.Add(s);
        SetNewToBag();
    }
    void SetNewToBag()
    {
        bagNew.SetActive(true);
    }


    public List<string> charactorNameList = new List<string>();
    public void SetCharatorToDic(string s)
    {
        charactorNameList.Add(s);
        Debug.Log("add to charactor dic:" + s);
    }
    public ToBlackPage bloodShow;
    public float endBloodTime = 2;
    public void GameOver()
    {
        bloodShow.SetBackground(endBloodTime);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

   
    private void Start()
    {
        GameState = GameStateEnum.StartBedRoom;
        leftPlayer.SetState("");
        rightPlayer.SetState("");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddTime(2);
        }
        switch (GameState)
        {
            case GameStateEnum.StartBedRoom:
                break;
            case GameStateEnum.SchoolGate:
                break;
            case GameStateEnum.MusicRoom:
                {
                    //gameTime -= Time.deltaTime;
                    //if (Input.GetKeyDown(KeyCode.G)||gameTime<=0)
                    //{
                    //    OverGame();
                    //}
                 
                }
                break;
            case GameStateEnum.Gym:
                break;
            case GameStateEnum.NurseOffice:
                break;
            case GameStateEnum.ClassRoom:
                break;
            case GameStateEnum.G:
                break;
            default:
                break;
        }
    }

}
