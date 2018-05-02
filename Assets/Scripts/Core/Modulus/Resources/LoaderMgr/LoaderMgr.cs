using System;
using System.Collections.Generic;
using UnityEngine;

public class LoaderMgr
{

    private static LoaderMgr instance;
    public static LoaderMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoaderMgr();
            }
            return instance;
        }
    }

    public void initialize()
    {
        TimerMgr.addEveryMillHandler(onTick, 50);
    }

    private List<string> removeLst = new List<string>();
    private Dictionary<string, LoaderTask> dictTask = new Dictionary<string, LoaderTask>();
    private Queue<LoaderTask> loadQueue = new Queue<LoaderTask>();

    public void addTask(string url, Action<string, bool, TBundle> call, List<string> depends = null)
    {
        if (!dictTask.ContainsKey(url))
        {
            if (depends == null)
            {
                depends = new List<string>();
                ManifestMgr.getDepends(url, ref depends);
            }
            //先加载所有依赖            
            for (int i = 0; i < depends.Count; i++)
            {
                if (!dictTask.ContainsKey(depends[i]))
                {
                    add(depends[i], null);
                }
            }
            add(url, call);
        }
        else
        {
            dictTask[url].addHandler(call);
        }
    }

    private void add(string url, Action<string, bool, TBundle> call = null)
    {
        LoaderTask task = new LoaderTask(url, call);
        dictTask.Add(task.url, task);
        loadQueue.Enqueue(task);
    }

    private void removeTask(string url)
    {
        if (dictTask.ContainsKey(url))
        {
            dictTask.Remove(url);
        }
    }

    public void onTick(int count)
    {
        if (loadQueue.Count <= 0) return;
        LoaderTask task = loadQueue.Peek();
        if (task.status == E_LoadStatus.Loading) return;
        if (task.status == E_LoadStatus.Finish || task.status == E_LoadStatus.Fail)
        {
            removeTask(task.url);
            loadQueue.Dequeue();
            if (loadQueue.Count > 0)
                task = loadQueue.Peek();
        }
        if (task != null && task.status == E_LoadStatus.Wait) task.doLoad();
    }
}

