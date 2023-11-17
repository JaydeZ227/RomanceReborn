using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMapButton : MonoBehaviour
{
    public string mapName="";
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(()=> 
        {
            GameController2.Instance.LoadSceneByMap(mapName);
            GetComponentInParent<PhonePage>().CloseOrOpenThis();
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
