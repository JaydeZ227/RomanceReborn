using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIcon_BG : MonoBehaviour
{
    [System.Serializable]
    public class StateConfig
    {
        public string stateName = "״̬";
        public HideToShow stateShow;
    }
    [SerializeField]
    StateConfig[] states;
    string lastState;
    bool isInit = false;
    public System.Action onChange;

    void Init()
    {
        if (isInit)
        {
            return;
        }
        isInit = true;
        foreach (var state in states)
        {
            //Debug.Log(state.stateName);
            state.stateShow.Close(); ;
        }
    }
    public void SetStateNoAnim(string stateName)
    {
        Init();
        //Debug.Log(stateName+"");
        if (stateName == lastState)
        {
            return;
        }
       
        foreach (var state in states)
        {
            if (state.stateName == lastState)
            {
                state.stateShow.Close();
            }
            if (state.stateName == stateName)
            {
                state.stateShow.Open();
            }

        }
        lastState = stateName;

        
    }
    public void SetState(string stateName)
    {
        Init();
        //Debug.Log(stateName+"");
        if (stateName==lastState)
        {
            return;
        }
    
        foreach (var state in states)
        {
            if (state.stateName==lastState)
            {
                state.stateShow.CloseByAnim();
            }
            if (state.stateName== stateName)
            {
                state.stateShow.OpenByAnim();
            }
            
        }
        lastState = stateName;
        onChange?.Invoke();
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
