using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioTask {

    private List<Action<AudioClip>> loadHandler = new List<Action<AudioClip>>();
    private string path;
    private string name;

    public AudioTask(string path, Action<AudioClip> call) {
        this.path = path;
        getAudioName();
        addHandler(call);
        LoaderMgr.Instance.addTask(this.path, onLoaderFinish);
    }

    private void getAudioName() {
        string[] lst = this.path.Split('/');
        this.name = lst.Length > 1 ? lst[lst.Length - 1] : lst[0];
    }

    public void addHandler(Action<AudioClip> call) {
        if (! loadHandler.Contains(call)) {
            loadHandler.Add(call);
        }
    }

    public void onDispose() {
        loadHandler.Clear();
        loadHandler = null;
    }

    private void onLoaderFinish(string result, bool isSucc, TBundle tb)
    {
        if (isSucc)
        {
            AudioClip clip = tb.Ab.LoadAsset<AudioClip>(name);
            for (int i = 0; i < loadHandler.Count; i++)
            {
                loadHandler[i].Invoke(clip);
            }
        }
        else {
            Debug.LogError("加载audio失败 path " + this.path);
        }
    }

}


public class AudioClipMgr
{
    private static AudioClipMgr instance;
    private static AudioClipMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioClipMgr();
            }
            return instance;
        }
    }

    private Dictionary<string, AudioClip> cilpPool = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioTask> tasks = new Dictionary<string, AudioTask>();

    public void getClip(string name, Action<AudioClip> callBack)
    {
        loadClip(name, callBack);
    }

    /// <summary>
    /// 加载clip
    /// </summary>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    private void loadClip(string path, Action<AudioClip> callBack)
    {
        if (AssetMgr.isHave(path))
        {
            TBundle tb = AssetMgr.getBundle(path);
            if (callBack != null)
            {
                callBack(tb.Ab.LoadAsset<AudioClip>(getAudioName(path)));
            }
        }
        else
        {
            //需要加载ab
            if (tasks.ContainsKey(path))
            {
                tasks[path].addHandler(callBack);
            }
            else {
                AudioTask task = new AudioTask(path, callBack);
                tasks.Add(path, task);
            }
        }
    }

    public void removeTask(string path) {
        if (tasks.ContainsKey(path)) {
            AudioTask task = tasks[path];
            tasks.Remove(path);
            task.onDispose();
        }
        
    }

    private string getAudioName(string path)
    {
        string[] lst = path.Split('/');
       return lst.Length > 1 ? lst[lst.Length - 1] : lst[0];
    }

    #region   以下提供静态方法
    public static void getAudioClip(string name, Action<AudioClip> callBack)
    {
        Instance.getClip(name, callBack);
    }
    #endregion

}

