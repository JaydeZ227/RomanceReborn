using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour
{

    GameControllerSimpleEvent gameControllerSimpleEvent;

    public string GetJehaNameByApathyNum(string jeha_img)
    {
        string s = jeha_img;
        if (curApathyNum <= 3)
        {
            // s += "_" + 1;
        }
        else if (curApathyNum <= 7)
        {
            s += "_" + 1;
        }
        else if (curApathyNum <= 11)
        {
            s += "_" + 2;
        }
        else
        {
            s += "_" + 3;
        }
        return s;
    }

    public Gradient textColorGradient;
    public Color GetTextColor()
    {
        return textColorGradient.Evaluate((float)curApathyNum / maxApathyNum);

    }

    public PlotSO PlotSO
    {
        get
        {
            return gameControllerSimpleEvent.plotSO;
        }
    }
    public ChooseSO ChooseSO
    {
        get
        {
            return gameControllerSimpleEvent.chooseSO;
        }
    }
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
            Debug.Log("切换场景结束");
            Time.timeScale = 1;
            bgChangeHideCanvasGorup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        });



    }
    public StateIcon_Anim leftPlayer, middlePlayer, rightPlayer;
    static GameController2 _instance;
    public static GameController2 Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController2>();
                _instance.gameControllerSimpleEvent = _instance.GetComponent<GameControllerSimpleEvent>();
            }
            return _instance;
        }
    }

    public AudioClip startMusic;
    public enum GameStateEnum
    {
        StartBedRoom, SchoolGate, MusicRoom, Gym, NurseOffice, ClassRoom, Shop2ndFloor, Library, OverScene

    }
    public bool isLockClass2
    {
        get
        {
            return bagItemNameList.Contains("bat");
        }
    }
    public bool isLockLibaray = true;
    public void UnLockLibaray()
    {
        isLockLibaray = false;
    }
    public bool isLockStair = true;
    public void UnLockStair()
    {
        isLockStair = false;
    }

    public bool IsCanEnterTargetScene(string sceneName)
    {
        Debug.Log("scenenan:"+sceneName+":");
        bool isCanEnter = false;
        switch (sceneName)
        {
            case "NurseOffice":
                {
                    if (curTime<=21*60*60)
                    {
                        isCanEnter = true;
                    }
                }
                break;
            case "Gym":
                {
                    if (curTime <= 22 * 60 * 60)
                    {
                        isCanEnter = true;
                    }
                }
                break;
            case "MusicRoom":
                {
                    isCanEnter = true;
                    Debug.Log("判断音乐室");
                }
                break;
            case "Classroom":
                {
                    if (classEnterCount == 0)
                    {
                        if (curTime <= 19 * 60 * 60)
                        {
                            isCanEnter = true;
                        }
                    }
                    else if (classEnterCount >= 1)
                    {
                        if (!isLockClass2)
                        {
                            isCanEnter = true;

                        }
                        else 
                        {

                            if (curTime <= 7 * 60 * 60)
                            {
                                isCanEnter = true;
                            }
                        }

                    }
                   
                }
                break;
            case "Library":
                {
                    if (!isLockLibaray)
                    {
                        isCanEnter = true;
                    }
                   
                }
                break;
            case "Stairs":
                {
                    Debug.Log("panduan  stair");
                    if (!isLockStair)
                    {
                        isCanEnter = true;
                    }
                  
                }
                break;
            case "Shop2ndFloor":
                {
                    if (!isLockStair)
                    {
                        isCanEnter = true;
                    }
                }
                break;
            case "SchoolGate":
                {
                    isCanEnter = true;
                }
                break;
            case "OverScene":
                {
                    isCanEnter = true;
                }
                break;
            default:
                break;
        }
        return isCanEnter;
    }

    #region Timer
    public Text timeText;
    public float curTime = 6 * 60 * 60;
    public void AddTime(int s)
    {
        curTime += s;
        while (curTime >= 24 * 60 * 60)
        {
            curTime -= 24 * 60 * 60;
        }
        timeText.text = ((int)(curTime / 3600)).ToString("#00") + ":" + ((int)(curTime % 3600 / 60)).ToString("#00");
    }
    public void SetTime(int time)
    {
        curTime = time;
        while (curTime >= 24 * 60 * 60)
        {
            curTime -= 24 * 60 * 60;
        }
        timeText.text = ((int)(curTime / 3600)).ToString("#00") + ":" + ((int)(curTime % 3600 / 60)).ToString("#00");
    }



    #endregion
    #region MusicRoom
    public string mapToMusicRoom_Plot = "MusicRoom0";

    public MapPage map;
    public void LoadSceneByMap(string sceneName)
    {
        ApathyShow.SetActive(false);
        map.SetPosName(sceneName);
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
            case "Library":
                {
                    GameState = GameStateEnum.Library;
                }
                break;
            case "Shop2ndFloor":
                {
                    GameState = GameStateEnum.Shop2ndFloor;
                }
                break;
            case "Stairs":
                {
                    PlotController.Instance.SetSay(PlotSO.GetPlot(stairsSceneStart).plots);
                }
                break;
            case "SchoolGate":
                {
                    GameState = GameStateEnum.SchoolGate;
                }
                break;
            case "OverScene":
                {
                    GameState = GameStateEnum.OverScene;
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 返回学校 用动画
    /// </summary>
    public void ResetGameToSchoolGate()
    {

        showBlood.SetActive(true);
        endPage.endEvent.RemoveAllListeners();

        endPage.endEvent.AddListener(() =>
        {
            endPage.gameObject.SetActive(false);
            showBlood2.SetActive(false);
            showBlood.SetActive(false);
            GameState = GameStateEnum.SchoolGate;
        });
    }
    public void CloseSayBar()
    {
        PlotController.Instance.say.gameObject.SetActive(false);
    }
    #endregion
    #region BusZombie
    public string bsNextSay = "BusStop1";
    public string bsCharactorAllName = "zombie";
    public string bsBgName = "OnTheBus";
    public void ShowBSZombie()
    {
        StartCoroutine(busShowIE());
    }
    IEnumerator busShowIE()
    {
        bgIcon.SetState(bsBgName);
        leftPlayer.SetState(bsCharactorAllName);
        middlePlayer.SetState(bsCharactorAllName);
        rightPlayer.SetState(bsCharactorAllName);
        foreach (var item in PlotController.Instance.chooseHide)
        {
            item.SetActive(false);
        }
        PlotController.Instance.say.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        AddTime(30 * 60);
        // PlotController.Instance.say.gameObject.SetActive(true);
        PlotController.Instance.SetSay(PlotSO.GetPlot(bsNextSay).plots);
        DieToBus();
    }
    public bool isDieToBus = false;
    public void DieToBus()
    {
        isDieToBus = true;
    }
    #endregion
    #region BedZombie

    public string bedendNextSay = "3_2_1";
    public string bedendCharactorAllName = "zombie";
    public string bedendBgName = "OnTheBus";
    public void ShowBedZombie()
    {
        StartCoroutine(BedShowIE());
    }
    IEnumerator BedShowIE()
    {
        bgIcon.SetState(bedendBgName);
        leftPlayer.SetState(bedendCharactorAllName);
        middlePlayer.SetState(bedendCharactorAllName);
        rightPlayer.SetState(bedendCharactorAllName);
        SetTime(60*60*24-30*60);
        foreach (var item in PlotController.Instance.chooseHide)
        {
            item.SetActive(false);
        }
        PlotController.Instance.say.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        // PlotController.Instance.say.gameObject.SetActive(true);
        PlotController.Instance.SetSay(PlotSO.GetPlot(bedendNextSay).plots);
        //DieToBus();
    }

    public BackPage backPage;
    public string startLoadSay = "";
    public string startSceneName = "";
    public GameObject showBlood;
    public GameObject showBlood2;
    public ToBlackPage endPage;
    public void ResetGameToHome()
    {
        showBlood.SetActive(true);
        endPage.endEvent.RemoveAllListeners();

        endPage.endEvent.AddListener(() =>
        {
            endPage.gameObject.SetActive(false);
            showBlood2.SetActive(false);
            showBlood.SetActive(false);
            
            GameState = GameStateEnum.StartBedRoom;
        });
    }
    [Header("玩家的服饰")]
    string samePlayerStr = "";
    public string basePlayerStr = "player";
    public string HairAddStr_Up = "hairUp";
    public string HairAddStr_Down = "hairDown";
    public string clothAddStr_uniform = "uniform";
    public string clothAddStr_gym = "gym";
    public string GetPlayerFuShi()
    {
        return samePlayerStr;
    }
    public void AddHair_Up()
    {
        samePlayerStr += "_" + HairAddStr_Up;
    }
    public void AddHair_Down()
    {
        samePlayerStr += "_" + HairAddStr_Down;
    }
    public void AddCloth_uniform()
    {
        samePlayerStr += "_" + clothAddStr_uniform;
    }
    public void AddCloth_gym()
    {
        samePlayerStr += "_" + clothAddStr_gym;
    }
    public string busStop0_SayMinTo2 = "BusStop0";
    public string busStop0_SayMaxTo2 = "Choose_BusStop0";
    public void ChangeBedStopToChoose(int num)
    {
        if (curApathyNum >=num)
        {
            PlotController.Instance.SetSay(PlotSO.GetPlot(busStop0_SayMaxTo2).plots);
        }
        else
        {
            PlotController.Instance.SetSay(PlotSO.GetPlot(busStop0_SayMinTo2).plots);
            //GameState = GameStateEnum.SchoolGate;


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
    public string gymSayChooStr1 = "Gmy2_1_1";
    public string gymSayChooStr2 = "Gmy2_1_2";
    public string mapToGym_Plot = "Gmy1_0";
    public string mapToGym_Plot2 = "ChooseGmy_0";
    public void JudgeGymByJumpEnd()
    {
        if (isEnterJiaoshi)
        {


        }
        else if (isEnterHuShi)
        {

        }
        else
        {
            ResetGameToSchoolGate();
        }
    }
    public void JudgeGymEnterToNext()
    {
        if (isEnterJiaoshi)
        {
            PlotController.Instance.SetSay(PlotSO.GetPlot(gymSayChooStr1).plots);

        }
        else if (isEnterHuShi)
        {
            PlotController.Instance.SetSay(PlotSO.GetPlot(gymSayChooStr2).plots);
        }

    }
    public void SetEnterGym()
    {
        isEnteredGym = true;

    }
    #endregion
    #region HuShi

    public bool isEnterHuShi = false;
    public void setEnterHuShi()
    {
        isEnterHuShi = true;
    }

    public string mapToNurseOffice_Plot_DieBus = "NurseOffice1_0";
    public string mapToNurseOffice_Plot_NoDieBus = "NurseOffice1_1";


    #endregion
    #region Classroom
    public bool isEnterJiaoshi = false;
    public void SetEnterJiaoShi()
    {
        isEnterJiaoshi = true;
    }
    int classEnterCount = 0;
    public string mapToClassroom_Plot_Count1 = "Classroom1_0";
    public string mapToClassroom_Plot_Count2 = "Classroom1_0";
    public string mapToClassroom_Plot_Count3 = "Classroom1_0";

    public string classRoomChooseByNoEnter = "ClassChoose0_State1";
    public string classRoomChooseByEnter = "ClassChoose0_State2";
    public void LoadChooseByClassRoom()
    {
        if (isEnteredGym || isEnterHuShi)
        {
            PlotController.Instance.chooseSystem.SetChoose(ChooseSO.GetChoose(classRoomChooseByEnter).choose);
        }
        else
        {
            PlotController.Instance.chooseSystem.SetChoose(ChooseSO.GetChoose(classRoomChooseByNoEnter).choose);

        }
    }

    #endregion
    #region Library
    public string stairsSceneStart = "";
    public string LibrarySceneStart = "Library0";
    public string GetChooseLibrary_noChicken = "Library1_State1";
    public string GetChooseLibrary_hasChicken = "Library1_State2";
    public void EnterChooseLibraryByPropChicken()
    {
        if (bagItemNameList.Contains("Chicken"))
        {
            PlotController.Instance.chooseSystem.SetChoose(ChooseSO.GetChoose(GetChooseLibrary_hasChicken).choose);
        }
        else
        {
            PlotController.Instance.chooseSystem.SetChoose(ChooseSO.GetChoose(GetChooseLibrary_noChicken).choose);
        }
    }
    public string Library2_0_0Next = "Library2_0_1";
    public GameObject libraryTipShow;
    public void ShowTipLibary()
    {
        libraryTipShow.SetActive(true);
    }
    public void LoadNextLibraray()
    {
        PlotController.Instance.SetSay(PlotSO.GetPlot(Library2_0_0Next).plots);
    }
    #endregion
    #region Shop2ndFloor
    public string Stop2ndFloorStart = "Shop2ndFloor0";
    #endregion
    #region OverScene
    public string OverSceneStart = "Alpathy12Scene0";
    public string OverSceneEndSay = "Apathy12Scene1";
    public GameObject resetButton;
    public GameObject bloodShow_12end;
    public void ShowBlood()
    {
        bloodShow_12end.SetActive(true);
    }
    public void ShowNextSay_Apathy12()
    {
        PlotController.Instance.SetSay(PlotSO.GetPlot(OverSceneEndSay).plots);
    }
    
    public void ResetGame()
    {
        //LocalData.levelBagStr = new List<string>(bagItemNameList.ToArray());
        //LocalData.levelCharactorStr = new List<string>(charactorNameList.ToArray());
        SceneManager.LoadScene("GameScene1");
    }
    public void ShowResetButton()
    {
        resetButton.SetActive(true);
    }
    #endregion
    GameStateEnum _gameState = GameStateEnum.StartBedRoom;
    public string player_zombie = "player_zombie";
    public string GetPlayerZombie()
    {
        if (GetPlayerFuShi().Contains("gym"))
        {
            return "player_zombie_gym";
        }
        else
        {
            return "player_zombie_uniform";
        }
    }
    public GameObject ApathyShow;
    public GameStateEnum GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            _gameState = value;
            PlotController.Instance.chooseSystem.gameObject.SetActive(false);
            switch (_gameState)
            {
                case GameStateEnum.StartBedRoom:
                    {
                        PlotController.Instance.say.gameObject.SetActive(false);
                        leftPlayer.SetState("");
                        middlePlayer.SetState("");
                        rightPlayer.SetState("");
                        MusicController.Instance.PlayEffectByFrame
                            (startMusic);
                        SetTime(6*60*60);
                        //初始化玩家服饰
                        samePlayerStr = basePlayerStr;


                        bgIcon.SetState(startSceneName);
                        backPage.SetBackground(2.0f,()=> 
                        {
                            //Debug.Log(PlotSO);
                        PlotController.Instance.SetSay(PlotSO.GetPlot(startLoadSay).plots);
                        });
                    }
                    break;
                case GameStateEnum.SchoolGate:
                    {
                        map.SetPosName("SchoolGate");
                        SetTime(7 * 60 * 60);
                        Debug.Log("SchoolGate");
                        CloseSayBar();
                        bgIcon.SetState(SchoolGate_morn_BgStr);
                        leftPlayer.SetState("");
                        middlePlayer.SetState(GetJehaNameByApathyNum("Jeha_default"));
                        rightPlayer.SetState("");
                        PlotController.Instance.say.gameObject.SetActive(false);
                        ApathyShow.SetActive(true);
                        if (curApathyNum>=12)
                        {
                            LoadSceneByMap("OverScene");
                        }

                    }
                    break;
                case GameStateEnum.MusicRoom:
                    {

                        PlotController.Instance.SetSay(PlotSO.GetPlot(mapToMusicRoom_Plot).plots);

                    }
                    break;
                case GameStateEnum.Gym:
                    {
                        if (isEnteredGym)
                        {
                            PlotController.Instance.SetSay(PlotSO.GetPlot(mapToGym_Plot2).plots);
                        }
                        else
                        {
                            PlotController.Instance.SetSay(PlotSO.GetPlot(mapToGym_Plot).plots);
                        }
                      
                        SetEnterGym();
                    }
                    break;
                case GameStateEnum.NurseOffice:
                    {
                        if (isDieToBus)
                        {
                            PlotController.Instance.SetSay(PlotSO.GetPlot(mapToNurseOffice_Plot_DieBus).plots);
                        }
                        else
                        {
                            PlotController.Instance.SetSay(PlotSO.GetPlot(mapToNurseOffice_Plot_NoDieBus).plots);
                        }
                        setEnterHuShi();
                    }
                    break;
                case GameStateEnum.ClassRoom:
                    {
                        if (classEnterCount == 0)
                        {
                            PlotController.Instance.SetSay(PlotSO.GetPlot(mapToClassroom_Plot_Count1).plots);
                            AddTime(5*60*60);
                        }
                        else if (classEnterCount >= 1)
                        {
                            if (!isLockClass2)
                            {
                                PlotController.Instance.SetSay(PlotSO.GetPlot(mapToClassroom_Plot_Count2).plots);
                                AddTime(5 * 60 * 60);
                            }
                            else 
                            {
                                PlotController.Instance.SetSay(PlotSO.GetPlot(mapToClassroom_Plot_Count3).plots);
                            }

                            
                        }
                        
                        classEnterCount++;
                        SetEnterJiaoShi();
                    }
                    break;
                case GameStateEnum.Library:
                    {
                        PlotController.Instance.SetSay(PlotSO.GetPlot(LibrarySceneStart).plots);
                    }
                    break;
                case GameStateEnum.Shop2ndFloor:
                    {
                        PlotController.Instance.SetSay(PlotSO.GetPlot(Stop2ndFloorStart).plots);
                    }
                    break;
                case GameStateEnum.OverScene:
                    {
                        PlotController.Instance.SetSay(PlotSO.GetPlot(OverSceneStart).plots);
                    }
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
        if (charactorNameList.Contains(s))
        {
            return;
        }
        charactorNameList.Add(s);
        Debug.Log("add to charactor dic:" + s);
    }
    public void RemoveCharactorToDic(string s)
    {
        if (!charactorNameList.Contains(s))
        {
            return;
        }
        charactorNameList.Remove(s);
        Debug.Log("remove to charactor dic:" + s);
    }
    public ToBlackPage bloodShow;
    public float endBloodTime = 2;
    public void GameOver()
    {
        bloodShow.SetBackground(endBloodTime);
    }
    public void BackMenu()
    {
      //  LocalData.levelBagStr = new List<string>();
        //LocalData.levelCharactorStr = new List<string>();
      //  LocalData.repeatTipStrList = new List<string>();
        SceneManager.LoadScene("StartScene");
    }
    private void Awake()
    {
        //can't touch map
        mapButton.isCanTouch = false;

       var t= GameController2.Instance;
        //初始化数据
        //bagItemNameList = new List<string>(LocalData.levelBagStr);
       // charactorNameList = new List<string>(LocalData.levelCharactorStr);

        bgChangeHideCanvasGorup.Open();
        bgIcon.onChange += () =>
        {
            cor = StartCoroutine(changeBGIE());
        };
        ApathyNumText.text = curApathyNum + "";
        timeText.text = ((int)(curTime / 3600)).ToString("#00") + ":" + ((int)(curTime % 3600 / 60)).ToString("#00");
    }

    private void Start()
    {
        GameState = GameStateEnum.StartBedRoom;
        leftPlayer.SetState("");
        middlePlayer.SetState("");
        rightPlayer.SetState("");
    }
    
    private void Update()
    {
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
            case GameStateEnum.Library:
                {

                }
                break;
            case GameStateEnum.Shop2ndFloor:
                {

                }
                break;
            case GameStateEnum.OverScene:
                {

                }
                break;
            default:
                break;
        }
    }

    //hide iphone
    public ChangeSPButton mapButton;
    //display
    public void CanTouchMap()
    {
        mapButton.isCanTouch = true;
    }

}
