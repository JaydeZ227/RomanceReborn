using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorIcon : MonoBehaviour
{
    public string charactorName = "";
    public GameObject lockImage, noLockImage;
     CanvasGroup canvasGroup;
    public void SetLock(bool isLock)
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (isLock)
        {
            canvasGroup.blocksRaycasts = false;
            noLockImage.SetActive(false);
            lockImage.SetActive(true);
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
            noLockImage.SetActive(true);
            lockImage.SetActive(false);
        }
    }
}
