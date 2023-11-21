using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideToShow : MonoBehaviour
{
    bool isInit = false;
    private void Init()
    {
        if (isInit)
        {
            return;
        }
        isInit = true;
        canvasGroup = GetComponent<CanvasGroup>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    CanvasGroup canvasGroup;
    SpriteRenderer spriteRenderer;
    void SetAlpha()
    {
        if (canvasGroup!=null)
        {
            canvasGroup.alpha = a;
        }
        if (spriteRenderer != null)
        {
           var color= spriteRenderer.color;
            color.a = a;
            spriteRenderer.color = color;
        }
    }
    float a = 0;
    bool isOpen = false;
    System.Action openEnd;
    bool isOpenEnd = false;
    public void OpenByAnim(System.Action openEnd=null)
    {
        Init();
        this.openEnd=openEnd;
        gameObject.SetActive(true);
        isOpen = true;
        isOpenEnd = false;
    }
    public void Open()
    {
        Init();
        a = 1;
        SetAlpha();
        isOpenEnd = true;
        isOpen = true;
        gameObject.SetActive(true);
    }
    public bool isUseRealTime = false;
    public bool isCloseHide = true;
    public void CloseByAnim()
    {
        Init();
        isOpen = false;
    }
    public void Close()
    {
        Init();
        a = 0;
        SetAlpha();
        isOpen = false;
        if (isCloseHide)
        {
            gameObject.SetActive(false);
        }
      
    }
    // Start is called before the first frame update
    void Start()
    {
        realDaltaTime_Last = Time.realtimeSinceStartup;
    }
    public float showAlphaSpeed = 2;
    public float hideAlphaSpeed = 2;
    float realDaltaTime_Last = 0;
    // Update is called once per frame
    void Update()
    {
        float realDaltaTime = Time.realtimeSinceStartup-realDaltaTime_Last;
        if (isOpen)
        {
            if (!isOpenEnd)
            {
                a +=( isUseRealTime ? realDaltaTime : Time.deltaTime) * showAlphaSpeed;
                if (a > 1)
                {
                    isOpenEnd = true;
                    openEnd?.Invoke();
                    a = 1;
                }
            }
           
        }
        else
        {
            a -= (isUseRealTime ? realDaltaTime : Time.deltaTime)*hideAlphaSpeed;
            if (a<0)
            {
                a = 0;
                if (isCloseHide)
                {
                    gameObject.SetActive(false);
                }
              
            }
        }
        SetAlpha();
        realDaltaTime_Last = Time.realtimeSinceStartup;
    }
}
