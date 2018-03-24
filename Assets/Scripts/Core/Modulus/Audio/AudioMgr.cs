using System;
using System.Collections.Generic;
using UnityEngine;

  public  class AudioMgr
    {

    private static AudioMgr instance;
    private static AudioMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioMgr();
            }
            return instance;
        }
    }

    private AudioSource roleAudioListener;
    protected AudioSource RoleAudioListener
    {
        get {
            if (roleAudioListener == null) {
                GameObject go = GameObject.Find("_roleAudioListener");
                if (go == null) {
                    go = new GameObject("_roleAudioListener");
                }
                roleAudioListener = go.GetComponent<AudioSource>();
                if (roleAudioListener == null) {
                    roleAudioListener = go.AddComponent<AudioSource>();
                } 
            }
            return roleAudioListener;
        }
    }

    public  void playRoleAudio(string clipName)
    {
        if (RoleAudioListener != null) {
            AudioClipMgr.getAudioClip(clipName, playRoleOneShot);
        }
    }

    public void playAtPoint(string name)
    {

    }

    /// <summary>
    /// 内部
    /// </summary>
    /// <param name="clip"></param>
    private void playRoleOneShot(AudioClip clip) {
        if (RoleAudioListener != null)
        {
            RoleAudioListener.loop = false;
            RoleAudioListener.PlayOneShot(clip);
        }
    }



    #region 以下提供静态方法
    public static void playRoleOneShot(string clipName) {
        Instance.playRoleAudio(clipName);
    }
    #endregion

}

