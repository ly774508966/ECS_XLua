using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletMgr
{
    private static BulletMgr instance;
    public static BulletMgr Instance {
        get {
            if (instance == null) {
                instance = new BulletMgr();
            }
            return instance;
        }
    }

    private Transform poolRoot;
    protected Transform PoolRoot {
        get {
            if (poolRoot == null) {
                GameObject root = new GameObject("_bulletRoot");
                poolRoot = root.transform;
                poolRoot.transform.position = new Vector3(5000, -5000, 5000);
            }
            return poolRoot;
        }
    }

    private Dictionary<E_BulletType, List<BaseBullet>> bulletPool = new Dictionary<E_BulletType, List<BaseBullet>>();

    public T getBullet<T>(BulletData data) where T : BaseBullet
    {
        E_BulletType btype = data.btype;
        if (bulletPool.ContainsKey(btype) && bulletPool[btype].Count > 0)
        {
            BaseBullet bb = bulletPool[btype][0];
            bb.refreshData(data);
            bulletPool[btype].RemoveAt(0);
            bb.CacheTrans.SetParent(null);
            return bb as T;
        }
        else {
            return BulletFactory.create<T>(data);
        }
    }

    public void saveBullet(BaseBullet bb) {
        E_BulletType btype = bb.btype;
        if (!bulletPool.ContainsKey(btype)) {
            bulletPool.Add(btype, new List<BaseBullet>());
        }
        bb.onDispose();
        bulletPool[btype].Add(bb);
        bb.CacheTrans.SetParent(PoolRoot);
        bb.CacheTrans.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 切换场景调用
    /// </summary>
    public void clearPool() {
       
    }
}
