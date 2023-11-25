using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMapButton : MonoBehaviour
{
    public string mapName="";
    Button btn;
    public GameObject lockShow;
    Transform playerIcon;
    
    public void SetLock(bool isLock)
    {
        lockShow.SetActive(isLock);
        GetComponent<Button>().interactable = !isLock;
    }
    private void Awake()
    {
        playerIcon = GetComponentInParent<MapPage>().playerIcon;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(()=> 
        {
            playerIcon.GetComponent<PlayerIcon>().SetMoveTo(transform.position,()=> 
            {
                GameController2.Instance.LoadSceneByMap(mapName);
                GetComponentInParent<PhonePage>().CloseOrOpenThis();
                
            });
          
        });
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
