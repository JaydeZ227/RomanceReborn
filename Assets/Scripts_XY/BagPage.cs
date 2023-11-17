using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BagPage : MonoBehaviour
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
    public Transform propParent;
    void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        if (SceneManager.GetActiveScene().name == "GameScene1")
        {
            for (int i = 0; i < propParent.childCount; i++)
            {
                if (GameController.Instance.bagItemNameList.Contains(propParent.GetChild(i).GetComponent<PropIcon>().propName))
                {
                    propParent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    propParent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameScene2")
        {
            for (int i = 0; i < propParent.childCount; i++)
            {
                if (GameController2.Instance.bagItemNameList.Contains(propParent.GetChild(i).GetComponent<PropIcon>().propName))
                {
                    propParent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    propParent.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        
    }
    void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
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
