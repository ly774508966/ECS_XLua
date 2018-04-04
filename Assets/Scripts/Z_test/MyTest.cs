using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class MyTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 v1 = new Vector3(1, 1, 1);
        Vector3 v2 = new Vector3(2, 2, 2);
        Vector3 v3 = new Vector3(3, 3, 3);

        float val = (v2.x - v1.x) * (v2.x - v1.x) + (v2.y - v1.y) * (v2.y - v1.y) + (v2.z - v1.z) * (v2.z - v1.z);
        Debug.Log(val);
        Debug.Log(Mathf.Sqrt(val));

        float val2 = (v2.x - v3.x) * (v2.x - v3.x) + (v2.y - v3.y) * (v2.y - v3.y) + (v2.z - v3.z) * (v2.z - v3.z);
        Debug.Log(val2);
        Debug.Log(Mathf.Sqrt(val2));
    }


    IEnumerator loadByWWW()
    {
        int lastKb = 0;
        WWW www = new WWW("http://ynnx.sg.ufileos.com/patch/1_2.zip");
        while (!www.isDone) {
            yield return new WaitForSeconds(1);
            int mb = (www.bytesDownloaded / 1024) ;
            Debug.Log("每秒kb :  " + (mb- lastKb));
            lastKb = mb;
        }
        yield return www;

    }

}
