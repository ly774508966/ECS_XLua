using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 类型：到达目标 则销毁 返回
/// </summary>
public class BulletGoalJust : BaseBullet
{
    private RaycastHit info;
    protected override void onStart()
    {
        //初始化pos
        CacheTrans.position = data.startPos;
        speed = data.speed;
        reachDis = speed;
        //加载特效
        if (!insEff&&!string.IsNullOrEmpty(data.effPath) )
        {
            insEff = true;
            ResMgr.Instance.getObj(data.effPath, (obj) =>
            {
                obj.transform.SetParent(CacheTrans);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localEulerAngles = new Vector3(-90, 0, 0);
            });
        }       
    }

    protected override void onUpdate()
    {
        //当前目标不存在
        bool isHave = EntityMgr.isHaveEntity(data.goalUID);
        if (!isHave)
        {
            onFinish();
            return;
        }
        //当前目标存在 每帧计算距离
      /*
        CEntity goal = EntityMgr.getEntity(data.goalUID);
        disVec = goal.CacheTrans.position - CacheTrans.position;
        dir = disVec.normalized;
        dis = disVec.magnitude;
        CacheTrans.LookAt(goal.CacheTrans);
        Debug.DrawRay(CacheTrans.position, dir, Color.red, 0.5f);
        if (dis <= reachDis)
        {
            if (data.callBack != null)
            {
                data.callBack.Invoke(data.cfgId, data.goalUID, CacheTrans.position);
            }
            onFinish();
        }
        CacheTrans.position += dir* speed;
        */

        //每帧射线检测 有效率问题吗？
        ///*
        CEntity goal = EntityMgr.getEntity(data.goalUID);
        disVec = goal.CacheTrans.position+Vector3.up - CacheTrans.position;
        dir = disVec.normalized;
        CacheTrans.LookAt(goal.CacheTrans.position + Vector3.up);
        Debug.DrawRay(CacheTrans.position, dir, Color.red, 0.5f);
        if (Physics.Raycast(CacheTrans.position, dir, out info,speed))
        {
            //碰撞到目标
            CEntity hitEntity = info.collider.GetComponent<CEntity>();
            if (hitEntity != null) {
                if (hitEntity.UID == goal.UID) {
                    CacheTrans.position = goal.CacheTrans.position;
                    if (data.callBack != null)
                    {
                        data.callBack.Invoke(data.cfgId, data.goalUID, CacheTrans.position);
                    }
                    onFinish();                    
                }
            }
        }
        else {
            CacheTrans.position += dir * speed;
        }
        //*/
    }

    /*
     * 备注：每帧移动距离：moveStep 
     * 1：碰撞器高速飞行可能无法触发碰撞事件
     * 2：每帧计算与目标的距离 可能moveStep大于判断距离 也无法准确碰撞到目标
     * 3：每帧向运动方向进行射线检测,检测距离为moveStep,如果没有检测到碰撞,子弹移动moveStep,如果碰撞到,子弹移动到碰撞点
     */

}

