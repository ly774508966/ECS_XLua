using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr
{
    private static EntityMgr instance;
    private static EntityMgr Instance {
        get {
            if (instance == null) {
                instance = new EntityMgr();
            }
            return instance;
        }
    }

    private Dictionary<long, CEntity> entityPool = new Dictionary<long, CEntity>();
    private Dictionary<int, List<CEntity>> entityOptPool = new Dictionary<int, List<CEntity>>();

    private void add(CEntity entity) {
        int type = entity.Type;
        entityPool.Add(entity.UID, entity);
        if (!entityOptPool.ContainsKey(type)) {
            entityOptPool.Add(type, new List<CEntity>());
        }
        if (!entityOptPool[type].Contains(entity))
        {
            entityOptPool[type].Add(entity);
        }            
    }

    private void remove(CEntity entity) {
        long uid = entity.UID;
        if (entityPool.ContainsKey(uid)) {
            entityPool.Remove(uid);
        }
        int type = entity.Type;
        if (entityOptPool.ContainsKey(type)) {
            if (entityOptPool[type].Contains(entity)) {
                entityOptPool[type].Remove(entity);
            }
        }
    }

    private CEntity get(long uid) {
        CEntity ce = null;
        if (entityPool.ContainsKey(uid))
            ce = entityPool[uid];
        return ce;
    }

    private List<CEntity> getLst(int type) {
        List<CEntity> lst = new List<CEntity>();
        if (entityOptPool.ContainsKey(type)) {
            lst.AddRange(entityOptPool[type]);
        }
        return lst;
    }
    public bool isHave(long uid)
    {
        return entityPool.ContainsKey(uid);
    }


    #region 提供静态方法
    public static void addEntity(CEntity entity)
    {
         Instance.add(entity);
    }

    public static void removeEntity(CEntity entity)
    {
        Instance.remove(entity);
    }

    public static CEntity getEntity(long uid) {
        return Instance.get(uid);
    }

    public static bool isHaveEntity(long uid)
    {
        return Instance.isHave(uid);
    }
    #endregion
}

