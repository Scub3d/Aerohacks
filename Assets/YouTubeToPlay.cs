using UnityEngine;
using System.Collections;

public class YouTubeToPlay : MonoBehaviour {

	public string url;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);

		if (Application.loadedLevelName == "YouTubeScene") {
			GameObject.Find("Cube").GetComponent<CoherentUIView>().m_Page = url;
		}
	}
}
