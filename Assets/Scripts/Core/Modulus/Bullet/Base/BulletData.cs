using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    public E_BulletType btype;
    public int luaType;
    public long cfgId;
    public string effPath = null;
    public string expPath = null;
    public Vector3 startPos;
    public Vector3 endPos;
    public long goalUID;
    public float speed = 0.5f;
    //飞行曲线 todo
    //public int flyType;
    public Action<long, long,Vector3> callBack;  
}

