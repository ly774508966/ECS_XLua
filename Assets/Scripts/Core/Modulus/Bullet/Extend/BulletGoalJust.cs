using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 类型：到达目标 则销毁 返回
/// </summary>
public class BulletGoalJust : BaseBullet
{
    protected override void onStart()
    {
        //初始化pos
        CacheTrans.position = data.startPos;
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
        bool isHave = EntityMgr.isHaveEntity(data.goalUID);
        if (!isHave)
        {//目标不存在直接完成
            onFinish();
            return;
        }
        CEntity goal = EntityMgr.getEntity(data.goalUID);
        disVec = goal.CacheTrans.position - CacheTrans.position;
        dir = disVec.normalized;
        dis = disVec.magnitude;
        CacheTrans.LookAt(goal.CacheTrans);
        //Debug.LogError(dis);
        if (dis <= reachDis)
        {
            if (data.callBack != null)
            {
                data.callBack.Invoke(data.cfgId, data.goalUID, CacheTrans.position);
            }
            onFinish();
        }
        CacheTrans.position += dir*0.5f;
    }

}

