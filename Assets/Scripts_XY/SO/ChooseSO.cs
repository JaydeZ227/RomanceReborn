using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chooses", menuName = "SO/Choose")]
public class ChooseSO : ScriptableObject
{
    public ChooseMessage[] chooseMessages;
    public ChooseMessage GetChoose(string chooseName)
    {
        foreach (var item in chooseMessages)
        {
            if (item.chooseName == chooseName)
            {
                return item;
            }
        }
        return null;
    }
    [System.Serializable]
    public class ChooseMessage
    {
        public string chooseName = "";
        public ChooseGame choose;
    }
}
