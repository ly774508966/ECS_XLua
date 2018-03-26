using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞行技能基类
/// 类型1：有目标，销毁条件：到达目标；回调 call lua roleId
/// 类型2：无目标，销毁条件：遇到第一个目标；计算当前飞行路径中的碰撞体，遇到第一个则销毁 call lua roleId，pos
/// 类型3：无目标，销毁条件：飞行距离完成；计算当前飞行路径中的碰撞体，遇到role回调
/// </summary>
public class BaseBullet : MonoBehaviour
{
    private bool dataInit = false;
    
    protected long effectId = -1;
    protected bool insEff = false;
    protected BulletData data = null;
    protected Vector3 disVec = Vector3.zero;//缓存两点距离
    protected Vector3 dir = Vector3.zero;//缓存移动方向
    protected float dis = 0;
    protected float speed = 1;
    protected float reachDis = 0.5f;
    //是否定义子弹碰撞大小 无真实碰撞 todo 
    //是否定义飞行曲线 todo
    public E_BulletType btype = E_BulletType.None;    

    private Transform cacheTrans;
    public Transform CacheTrans {
        get {
            if (cacheTrans == null) {
                cacheTrans = this.transform;
            }
            return cacheTrans;
        }
    }

    private GameObject cacheObj;
    public GameObject CacheObj
    {
        get
        {
            if (cacheObj == null)
            {
                cacheObj = this.gameObject;
            }
            return cacheObj;
        }
    }

    private void Update()
    {
        if (!dataInit) return;
        onUpdate();
    }

    /// <summary>
    /// 子类重写飞行逻辑
    /// </summary>
    protected virtual void onUpdate()
    {

    }
    /// <summary>
    /// 子类重写初始化
    /// </summary>
    protected virtual void onStart()
    {

    }

    /// <summary>
    ///刷新数据
    /// </summary>
    /// <param name="data"></param>
    public virtual void refreshData(BulletData data)
    {
        this.data = data;
        btype = data.btype;
        onStart();
        this.enabled = true;
        dataInit = true;
    }

    /// <summary>
    /// 完成
    /// </summary>
    public virtual void onFinish()
    {
        BulletMgr.Instance.saveBullet(this);
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void onDispose()
    {
        data = null;
        disVec = Vector3.zero;
        dir = Vector3.zero;
        dis = 0;
        dataInit = false;
        this.enabled = false;
    }
}