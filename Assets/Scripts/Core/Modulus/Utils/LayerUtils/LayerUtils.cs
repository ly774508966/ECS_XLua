using System;
using System.Collections.Generic;
using UnityEngine;

public class LayerUtils
{
    public static void setLayer(GameObject obj,int layerIndex) {
        obj.layer = layerIndex;
    }

    public static void setLayer(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.GetMask(layerName);
    }


}

