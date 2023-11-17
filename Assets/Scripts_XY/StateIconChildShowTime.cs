using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIconChildShowTime : MonoBehaviour
{
    public float showTime = 2f;
    float showTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        showTimer += Time.deltaTime;
        if (showTimer>showTime)
        {
            GetComponentInParent<StateIcon>().SetState("");
        }
    }
}
