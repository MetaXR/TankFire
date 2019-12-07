using UnityEngine.SceneManagement;

public class SceneMgr{
	private static string m_lastSceneName = "";
	public static string lastSceneName{
		get{ return m_lastSceneName;}
	}

	private static string m_nextSceneName = "";
	public static string nextSceneName{
		get{ return m_nextSceneName;}
	}

	public static void LoadScene(string prefabName){
		m_lastSceneName = m_nextSceneName;
		m_nextSceneName = prefabName;
		SceneManager.LoadSceneAsync("Common"); 
	}

	private static string m_moviepath = "";
	public static string moviepath{
		get{ return m_moviepath;}
	}
		
	public static void LoadMovie(string moviePath){
		m_moviepath = moviePath;   
		LoadScene ("Prefabs/System/PreSysMovie");
	}
		
	public static void SetMovieEndScene(string prefabName){
		m_nextSceneName = prefabName;
	} 
}
