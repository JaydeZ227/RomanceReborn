using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlotSO plotSO;
    public ChooseSO chooseSO;
    public StateIcon_BG bgIcon;
    public HideToShow bgChangeHideCanvasGorup;
    public float changeBgHideTime = 1.5f;
    private void Awake()
    {
        bgChangeHideCanvasGorup.Open();
        bgIcon.onChange += () =>
        {
            cor=StartCoroutine(changeBGIE());
        };
    }
    Coroutine cor;
    IEnumerator changeBGIE()
    {
        Time.timeScale = 0;
        Debug.Log("切换场景");
        bgChangeHideCanvasGorup.GetComponent<CanvasGroup>().blocksRaycasts=false;
      bgChangeHideCanvasGorup.Close();
        yield return new WaitForSecondsRealtime(changeBgHideTime);
        bgChangeHideCanvasGorup.OpenByAnim(()=> 
        {
            Time.timeScale = 1;
            bgChangeHideCanvasGorup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        });
        
        
   
    }
    public StateIcon_Anim leftPlayer, middlePlayer,rightPlayer;
    static GameController _instance;
    public static GameController Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }

    public enum GameStateEnum
    {

        Black,
        ExitBed,
        Game,
        InSchool,
        E,
        F,
        G
    }


    GameStateEnum _gameState = GameStateEnum.Black;
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
                case GameStateEnum.Black:
                    {
                        Debug.Log("开始");
                        bgIcon.SetStateNoAnim(heiPingScene_Start);
                        backPage.SetBackground(hideTime_Start, () =>
                        {
                            GameState = GameStateEnum.ExitBed;
                        });
                    }
                    break;
                case GameStateEnum.ExitBed:
                    {
                        PlotController.Instance.SetSay(plotSO.GetPlot(plotName_ExitBed).plots);
                    }
                    break;
                case GameStateEnum.Game:
                    {
                        if (cor!=null)
                        {
                            StopCoroutine(cor);
                            bgChangeHideCanvasGorup.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            Time.timeScale = 1;
                        }

                   
                    }
                    break;
                case GameStateEnum.InSchool:
                    {
                        PlotController.Instance.SetSay(plotSO.GetPlot(enterScroolSay).plots);
                    }
                    break;
                case GameStateEnum.E:
                    break;
                case GameStateEnum.F:
                    break;
                case GameStateEnum.G:
                    break;
                default:
                    break;
            }
        }
    }

    public GameObject bagNew;

    public void LoadScene2()
    {
        StartCoroutine(loadNextSceneIE());
    }
    IEnumerator loadNextSceneIE()
    {
        //LocalData.levelBagStr =new List<string>( bagItemNameList.ToArray());
        //LocalData.levelCharactorStr = new List<string>(charactorNameList.ToArray());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene2");
    }
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
        //LocalData.repeatTipStrList = new List<string>();
        //LocalData.levelBagStr = new List<string>();
        //LocalData.levelCharactorStr = new List<string>();
        SceneManager.LoadScene("StartScene");
    }

    #region Black screen
    public string heiPingScene_Start = "BedRoom";
    public float hideTime_Start = 2;
    public BackPage backPage;
    #endregion
    #region ExitBed
    public string plotName_ExitBed = "BedRoom_1";
    /*
  

    [HideInInspector]
    public string same_player_cloth = "Chaerin_wakeup";
    public string nextPlayerClothA = "";
    public string nextPlayerClothB = "";

    
    
    public void SetNextPlayerClothA()
    {
        same_player_cloth = nextPlayerClothA;
    }
    public void SetNextPlayerClothB()
    {
        same_player_cloth = nextPlayerClothB;
    }
    */
    #endregion
    #region InGame
    public string gameBG = "";
    public float gameTime = 2;
    /// <summary>
    /// enter Game
    /// </summary>
    public void EnterGameState()
    {
        bgIcon.SetState(gameBG);
        GameState = GameStateEnum.Game;
    }
    public void OverGame()
    {

        GameState = GameStateEnum.InSchool;
    }
  
    #endregion
    #region InSchool
    public string enterScroolSay;

    [Header("玩家的服饰")]
    string samePlayerStr = "Chaerin";
    public string basePlayerStr = "same_player";
    public string HairAddStr_Up = "hairUp";
    public string HairAddStr_Down = "hairDown";
    public string clothAddStr_uniform = "uniform";
    public string clothAddStr_gym = "gym";
    public string GetPlayerFuShi()
    {
        Debug.Log("当前发型"+samePlayerStr);
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


    public string chooseEnterSayByGymOrMaWei = "Classroom_1_0_1";
    public string chooseEnterSayByNoGymOrMaWei = "Classroom_1_0";
    public void SetMaWeiOrGym()
    {
        isMaWeiOrGym = true;
    }
  
    bool isMaWeiOrGym = false;
    public void ChooseInScrool()
    {
        if (isMaWeiOrGym)
        {
            PlotController.Instance.SetSay(plotSO.GetPlot(chooseEnterSayByGymOrMaWei).plots);
        }
        else
        {
            PlotController.Instance.SetSay(plotSO.GetPlot(chooseEnterSayByNoGymOrMaWei).plots);
        }
    }
    #endregion
    public GameObject Iphone1;
    public GameObject Bag1;
    public void TakeProp()
    {
        Iphone1.SetActive(true);
        Bag1.SetActive(true);
    }

    public AudioClip audioClip;
    private void Start()
    {
        MusicController.Instance.PlayEffectByFrame(audioClip);
        GameState = GameStateEnum.Black;
        leftPlayer.SetState("");
        middlePlayer.SetState("");
        rightPlayer.SetState("");
    }
    private void Update()
    {
        switch (GameState)
        {
            case GameStateEnum.Black:
                break;
            case GameStateEnum.ExitBed:
                break;
            case GameStateEnum.Game:
                {
                    //gameTime -= Time.deltaTime;
                    //if (Input.GetKeyDown(KeyCode.G)||gameTime<=0)
                    //{
                    //    OverGame();
                    //}
                 
                }
                break;
            case GameStateEnum.InSchool:
                break;
            case GameStateEnum.E:
                break;
            case GameStateEnum.F:
                break;
            case GameStateEnum.G:
                break;
            default:
                break;
        }
    }

}
