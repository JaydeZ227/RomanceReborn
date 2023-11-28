using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhonePage : MonoBehaviour
{
    bool isOpen = false;
    public void CloseOrOpenThis()
    {
        Debug.Log("µ÷ÓÃ");
        if (isOpen)
        {
         
            Close();
        }
        else
        {
           
            Open();
        }
    }
    public Transform charactorLockParent;
    public GameObject page1, page_Charactor;
    public GameObject page_Map;
    public StateIcon charactorMassage;
    void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        bagButton.SetActive(false);
        iphoneButton.SetActive(false);
        page1.SetActive(true);
        page_Map.SetActive(false);
        page_Charactor.SetActive(false);
        charactorMassage.SetState("");
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            for (int i = 0; i < charactorLockParent.childCount; i++)
            {
                var icon = charactorLockParent.GetChild(i).GetComponent<CharactorIcon>();
                if (GameController.Instance.charactorNameList.Contains(icon.charactorName))
                {
                    icon.gameObject.SetActive(true);
                    icon.SetLock(false);
                }
                else
                {
                    icon.gameObject.SetActive(false);
                    icon.SetLock(true);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            for (int i = 0; i < charactorLockParent.childCount; i++)
            {
                var icon = charactorLockParent.GetChild(i).GetComponent<CharactorIcon>();
                if (GameController2.Instance.charactorNameList.Contains(icon.charactorName))
                {
                    icon.gameObject.SetActive(true);
                    icon.SetLock(false);
                }
                else
                {
                    icon.gameObject.SetActive(false);
                  
                    icon.SetLock(true);
                }
            }
        }
          
    }
    void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
        bagButton.SetActive(true);
        iphoneButton.SetActive(true);
    }
    public GameObject bagButton;
    public GameObject iphoneButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
