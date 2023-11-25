using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    public float moveSpeed = 10;
    Vector3 targetPos;
    System.Action endAction;
    bool isMove = false;
    public void SetMoveTo(Vector3 targetPos,System.Action endAction)
    {
        this.targetPos = targetPos;
        this.endAction = endAction;
        isMove = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            float moveDistance = moveSpeed * Time.deltaTime;
            float distance = Vector3.Distance(transform.position,targetPos);
            if (distance <= moveDistance)
            {
                transform.position = targetPos;
                isMove = false;
                endAction?.Invoke();
            }
            else
            {
                transform.position += (targetPos - transform.position).normalized * moveDistance;
            }
        }
    }
}
