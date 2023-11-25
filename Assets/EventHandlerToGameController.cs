using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandlerToGameController : MonoBehaviour
{
    public void SendMassageToGameController(MethodEvent methodEvent)
    {
        switch (methodEvent.parameterType)
        {
            case ParameterType.None:
                {
                    transform.parent.SendMessage(methodEvent.methodName);
                }
                break;
            case ParameterType.INT:
                {
                    Debug.Log("µ÷ÓÃint"+ methodEvent.parameter);
                    transform.parent.SendMessage(methodEvent.methodName,int.Parse(methodEvent.parameter));
                }
                break;
            case ParameterType.STRING:
                {
                    transform.parent.SendMessage(methodEvent.methodName, methodEvent.parameter);
                }
                break;
            default:
                break;
        }
     
    }
    public void SendMassageToGameController(string methodName)
    {
      
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
[System.Serializable]
public class MethodEvent
{
    [Header("MethodName")]
    public string methodName;
    [Header("Parameter")]
    public string parameter;
    [Header("ParameterType")]
    public ParameterType parameterType=ParameterType.None;
}
public enum ParameterType
{
    None,
    INT,
    STRING,
    
}
