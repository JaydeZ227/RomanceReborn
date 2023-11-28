using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenuButton : MonoBehaviour
{
    ChangeSPButton changeSPButton;
    private void Awake()
    {
        changeSPButton = GetComponent<ChangeSPButton>();
        changeSPButton.touchEvent.AddListener(() => 
        {
            SceneManager.LoadScene("StartScene");
        
        });
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
