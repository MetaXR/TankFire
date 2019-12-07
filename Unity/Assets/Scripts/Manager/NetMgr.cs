using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网络通讯接口.
/// </summary>
public class NetMgr : SingletonMonoBehavior<NetMgr> {
    
	/// <summary>
    /// 播放视屏,供外部调用.
    /// </summary>
    /// <param name="moviePath">Movie path.</param>
	public void PlayMovie(string moviePath){
		SceneMgr.LoadMovie (moviePath);
	}

	public void NetLoadScene(string sceneName){
		SceneLoading.GetInstance ().ChangeToScene (sceneName);
	}

	public void SendNetMsg(string funcName, string ars){
		Application.ExternalCall (funcName, ars);
	}

	public void SendNetMsg(string funcName, int ars){
		Application.ExternalCall (funcName, ars);
	}

	public void SendNetMsg(string funcName, float ars){
		Application.ExternalCall (funcName, ars);
	}
}
