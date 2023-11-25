using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPage : MonoBehaviour
{
    public Transform playerIcon;
    public Transform buttonParent;
    string thisPos;
    public void SetPosName(string s)
    {
        thisPos = s;
    }
    private void OnEnable()
    {
        foreach (var item in buttonParent.GetComponentsInChildren<LoadMapButton>())
        {
            item.SetLock(!GameController2.Instance.IsCanEnterTargetScene(item.mapName));
            
            if (item.mapName==thisPos)
            {
                playerIcon.transform.position = item.transform.position;
            }
        }
    }
    private void Awake()
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
