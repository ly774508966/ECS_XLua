using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class FbxInfo
{
    public string amtName {
        get {
            if (fbxAmt == null)
                return "null";
            else
                return fbxAmt.name;
        }
    }
    public string modelName
    {
        get
        {
            if (fbxModel == null)
                return "null";
            else
                return fbxModel.name;
        }
    }
    public string path = "";
    public bool isSelect = false;
    public UnityEngine.Object fbxAmt;
    public UnityEngine.Object fbxModel;
}

public class AnimToolUI : EditorWindow
{
    private string fbxPath = "Assets/Res/Arts/FBX";

    private static AnimToolUI _instance = null;
    private Vector2 scrollPos = Vector2.zero;
    private Rect windowRect = new Rect(200 , 200, 200, 300);
    private List<FbxInfo> fbxLst = new List<FbxInfo>();
    private FbxInfo selectInfo = null;
    private bool isInit = false;
    private string winName = "";
    private string inputName = "";

    [MenuItem("ToolsWindow/AnimTools")]
    public static void showWindow()
    {
        if (_instance == null)
        {
            _instance = (AnimToolUI)EditorWindow.GetWindow(typeof(AnimToolUI), false, "动画编辑器", true);
            _instance.initLst();
            _instance.maxSize = new Vector2(1280, 720);
            _instance.isInit = true;
        }
    }

    /// <summary>
    /// 初始化所有fbx列表
    /// </summary>
    private void initLst()
    {
        string[] allPath = Directory.GetDirectories(getFullPath());
        for (int i = 0; i < allPath.Length; i++)
        {
            FbxInfo info = new FbxInfo();
            string path = allPath[i];
            info.path = path;
            string[] sonLst = Directory.GetFiles(path, "*.FBX");
            for (int j = 0; j < sonLst.Length; j++)
            {
                if (sonLst[j].Contains("Amt"))
                {
                    UnityEngine.Object amt = getObj(getAssetPath(sonLst[j]));
                    info.fbxAmt = amt;
                }
                else {
                    UnityEngine.Object model = getObj(getAssetPath(sonLst[j]));
                    info.fbxModel = model;
                }               
            }
            fbxLst.Add(info);
        }
    }

    private void OnGUI()
    {
        if (!isInit) return;
        GUILayout.BeginVertical();
        //名称
        GUILayout.BeginHorizontal();
        GUILayout.Box(getTex("animtoolIcon.png"));
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("编辑模型", GUILayout.Width(200), GUILayout.Height(80));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        //左边列表  
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Width(200), GUILayout.Height(600));
        drawLeftLst();
        GUILayout.EndScrollView();
        //右边详细信息
        drawRightInfo();
        GUILayout.EndHorizontal();
        //结束
        GUILayout.EndVertical();
        drawCreatePrefabWin(winName);
    }

    //左侧列表
    private void drawLeftLst()
    {
        for (int i = 0; i < fbxLst.Count; i++)
        {
            string name = fbxLst[i].amtName;
            GUI.color = fbxLst[i].isSelect ? Color.green : Color.white;
            if (GUILayout.Button(name , GUILayout.Width(175), GUILayout.Height(50)))
            {                
                if (selectInfo != null) {
                    selectInfo.isSelect = false;
                }
                fbxLst[i].isSelect = true;
                selectInfo = fbxLst[i];
                pingObj(selectInfo.fbxAmt);
            }
            GUI.color = Color.white;
        }
    }
    //右侧详细信息
    private void drawRightInfo()
    {
        if (selectInfo == null)
        {
            GUILayout.Label("请选择模型");
        }
        else {
            GUILayout.BeginVertical();
            GUILayout.Label("模型路径：" + selectInfo.path);
            GUILayout.BeginHorizontal();
            GUILayout.Label("当前动画模型："+ selectInfo.amtName);
            EditorGUILayout.ObjectField(selectInfo.fbxAmt, typeof(GameObject), false, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("当前预设模型：" + selectInfo.modelName);
            EditorGUILayout.ObjectField(selectInfo.fbxModel, typeof(GameObject), false, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("创建新预设", GUILayout.Width(160), GUILayout.Height(30))) {
                //draw window      
                inputName = "请输入";
                drawCreatePrefabWin("创建新预设");
            }
            
            GUILayout.EndVertical();
        }
    }

    private void drawCreatePrefabWin(string name) {
        if (string.IsNullOrEmpty(name)) return;
        winName = name;
        BeginWindows();
         GUILayout.Window(1, new Rect(400, 250, 200, 300), drawWinFunc, "创建新预设");        
        EndWindows();
    }
    private void drawWinFunc(int wid) {
        GUILayout.BeginVertical();
        GUI.color = Color.green;
        GUILayout.Label("请输入预设名称", GUILayout.Height(30));
        GUI.color = Color.white;
        inputName = GUILayout.TextField(inputName, GUILayout.Height(40));
        GUILayout.Space(100);
        if (GUILayout.Button("创建", GUILayout.Height(30)))
        {
            //TODO 创建预设 保存到对应文件夹
            winName = "";
        }
        GUILayout.Space(10);
        if (GUILayout.Button("取消",GUILayout.Height(30))) {
            winName = "";
        }
        GUILayout.EndVertical();
    }

    //path "Assets/Ztest/xx.png"
    private Texture getTex(string name)
    {
        string path = Path.Combine("Assets/ZTEST", name);
        return AssetDatabase.LoadAssetAtPath<Texture>(path);
    }

    private UnityEngine.Object getObj(string path)
    {
        return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
    }
    //FBX绝对路径
    private string getFullPath()
    {
        return Path.Combine(Application.dataPath, "Res/Arts/FBX");
    }
    //assetdata 路径
    private string getAssetPath(string full)
    {
        int index = full.IndexOf("Assets");
        return full.Substring(index);
    }
    //Ping obj
    private void pingObj(UnityEngine.Object obj) {
        EditorGUIUtility.PingObject(obj);
    }

    //fbx文件夹绝对路径
    string getFullPath(string name)
    {
        return Path.Combine(Application.dataPath, name);
    }
}

