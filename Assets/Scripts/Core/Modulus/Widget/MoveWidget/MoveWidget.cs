using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物理移动组件
/// </summary>
public class MoveWidget:MonoBehaviour
{
    private CharacterController cc= null;
    protected CharacterController CC
    {
        get
        {
            if (cc == null)
            {
                cc = this.GetComponentInParent<CharacterController>();
            }
            return cc;
        }
    }

    private float moveSpeed=0;//移速
    private float moveAtt = 0;//衰减
    private float moveDistance = 0;//位移距离
    private Vector3 moveDir = Vector3.zero;//位移方向
    private float moveTimer = 0;//位移时间
    

    public void setMoveArgs(float dis,float speed,float att,int dirType) {
        //计算出时间
        moveSpeed = speed;
        moveTimer = dis / speed;
        moveAtt = att;
        moveDir = getDir(dirType);        
    }

    private void Update()
    {
        if (moveTimer <= 0) return;
        moveSpeed -= moveAtt;
        moveSpeed = moveSpeed <= 0 ? 0 : moveSpeed;
        CC.SimpleMove(moveDir*moveSpeed*4);
        moveTimer -= Time.deltaTime;
    }

    private Vector3 getDir(int dirType) {
        switch (dirType) {
            case 1:
                return this.transform.forward;
            case 2:
                return this.transform.forward*-1;
            case 3:
                return this.transform.right * -1;
            case 4:
                return this.transform.right ;
            case 5:
                return this.transform.up;
            case 6:
                return this.transform.up*-1;
        }
        return Vector3.zero;
    }

}