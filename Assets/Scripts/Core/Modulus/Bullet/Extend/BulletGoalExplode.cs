using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletGoalExplode: BaseBullet
{
    private RaycastHit info;
    protected override void onStart()
    {
        //初始化pos
        CacheTrans.position = data.startPos;
        speed = data.speed;
        reachDis = speed;
        //加载特效
        if (!insEff && !string.IsNullOrEmpty(data.effPath))
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
        CEntity goal = EntityMgr.getEntity(data.goalUID);
        disVec = goal.CacheTrans.position + Vector3.up - CacheTrans.position;
        dir = disVec.normalized;
        CacheTrans.LookAt(goal.CacheTrans.position + Vector3.up);
        Debug.DrawRay(CacheTrans.position, dir, Color.red, 0.5f);
        if (Physics.Raycast(CacheTrans.position, dir, out info, speed))
        {
            //碰撞到目标
            CEntity hitEntity = info.collider.GetComponent<CEntity>();
            if (hitEntity != null)
            {
                if (hitEntity.UID == goal.UID)
                {
                    CacheTrans.position = goal.CacheTrans.position;
                    if (data.callBack != null)
                    {
                        data.callBack.Invoke(data.cfgId, data.goalUID, CacheTrans.position);
                    }
                    onFinish();
                }
            }
        }
        else
        {
            CacheTrans.position += dir * speed;
        }
    }



}

