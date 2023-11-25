using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToBlackPage : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        backTimer = 0;
        isOver = false;
    }

    public float backTime=2.0f;
    float backTimer = 0;
    
    public void SetBackground(float time)
    {
        backTimer = 0;
        backTime = time;
        gameObject.SetActive(true);
        isOver = false;
        if (isShow)
        {
            canvasGroup.alpha = backTimer / backTime;
        }
        else
        {
            canvasGroup.alpha = 1 - backTimer / backTime;
        }
    
    }
    public bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public UnityEvent endEvent;
    bool isOver = false;
    public bool isHide = true;
    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            return;
        }
        backTimer += Time.deltaTime;
        if (backTimer>backTime)
        {
            if (isHide)
            {
                gameObject.SetActive(false);
            }
            endEvent?.Invoke();
            isOver = true;
        }
        if (isShow)
        {
            canvasGroup.alpha = backTimer / backTime;
        }
        else
        {
            canvasGroup.alpha = 1 - backTimer / backTime;
        }
        
    }
}
