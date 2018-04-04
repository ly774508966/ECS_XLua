using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//加载状态
public enum E_LoadStatus
{
    Wait,
    Loading,
    Finish,
    Fail,
}

//对象池
public enum E_PoolType
{
    None,
    UseTime,
    Level,
    Gobal,
}

//AssetBundle Type
public enum E_AssetType
{
    None = 0,
    Normal,
    Atlas,
    Textures,
}

//子弹类型
public enum E_BulletType
{
    None = 0,
    GoalJust,//到达目标销毁回调 #lst = 1 需要目标
    GoalExplode,//到达目标 爆炸销毁回调 lst ={ } 需要目标
    UnGoalFirst,//无目标 遇到第一个销毁回调 #lst = 1 不需要目标
    UnGoalFirstExplode,//无目标 遇到第一个爆炸销毁回调 lst = { } 不需要目标
    UnGoalDistance,//无目标 遇到则回调 到达则销毁 不需要目标
}

public static class Define
{
    public static string On_Scene_Load_Begin = "On_Scene_Load_Begin";
    public static string On_Scene_Load_Finish = "On_Scene_Load_Finish";


}

