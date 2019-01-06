using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {
    public int moshi;
	// Use this for initialization
	void Start () {
		
	}
	public void Load()
    {
        PublicData.gamemoshi = moshi;
        SceneManager.LoadScene("Scenes/SampleScene");
        print("选择模式"+PublicData.gamemoshi);
    } 
	// Update is called once per frame
	void Update () {
		
	}
}
