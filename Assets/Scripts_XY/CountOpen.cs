using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountOpen : MonoBehaviour
{
    public int useCount = 2;
    public void LoseCount()
    {
        useCount--;
        if (useCount==0)
        {
            gameObject.SetActive(true);
        }
    }
   
}
