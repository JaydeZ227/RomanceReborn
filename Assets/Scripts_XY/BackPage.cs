using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackPage : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public float backTime=2.0f;
    float backTimer = 0;
 
    System.Action backEndAction;
    public void SetBackground(float time,System.Action backEndAction)
    {
        backTimer = 0;
        backTime = time;
        gameObject.SetActive(true);
        this.backEndAction = backEndAction;
        canvasGroup.alpha = 1 - backTimer / backTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        backTimer += Time.deltaTime;
        if (backTimer>backTime)
        {
            gameObject.SetActive(false);
            this.backEndAction?.Invoke();
            
        }
        canvasGroup.alpha =1- backTimer / backTime;
        
    }
}
