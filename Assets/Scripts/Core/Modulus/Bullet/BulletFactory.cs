using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory
{
    public static T create<T>(BulletData data)where T:BaseBullet {        
        GameObject go = new GameObject();
        T bb = go.AddComponent<T>();
        bb.refreshData(data);
        return bb;
    }

}

