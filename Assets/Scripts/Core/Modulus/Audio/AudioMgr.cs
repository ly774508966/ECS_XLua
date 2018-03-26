using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr
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
        get
        {
            if (roleAudioListener == null)
            {
                GameObject go = GameObject.Find("_roleAudioListener");
                if (go == null)
                {
                    go = new GameObject("_roleAudioListener");
                }
                roleAudioListener = go.GetComponent<AudioSource>();
                if (roleAudioListener == null)
                {
                    roleAudioListener = go.AddComponent<AudioSource>();
                }
            }
            return roleAudioListener;
        }
    }

    /// <summary>
    /// 主角
    /// </summary>
    /// <param name="clipName"></param>
    public void playRoleAudio(string clipName)
    {
        if (RoleAudioListener != null)
        {
            AudioClipMgr.getAudioClip(clipName, playRoleOneShot);
        }
    }
    /// <summary>
    /// 其他 once at point
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="pos"></param>
    public void playAtPoint(string clipName, Vector3 pos)
    {
        AudioClipMgr.getAudioClip(clipName, (clip) =>
        {
            playClipAtPoint(clip, pos);
        });
    }


    /// <summary>
    /// 内部
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="pos"></param>
    private void playClipAtPoint(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    /// <summary>
    /// 内部
    /// </summary>
    /// <param name="clip"></param>
    private void playRoleOneShot(AudioClip clip)
    {
        if (RoleAudioListener != null)
        {
            RoleAudioListener.loop = false;
            RoleAudioListener.PlayOneShot(clip);
        }
    }

    #region 以下提供静态方法
    /// <summary>
    /// 主角播放
    /// </summary>
    /// <param name="clipName"></param>
    public static void playRoleOneShot(string clipName)
    {
        Instance.playRoleAudio(clipName);
    }

    public static void playOneShot(string name,Vector3 pos)
    {
        Instance.playAtPoint(name, pos);
    }
    #endregion

}

