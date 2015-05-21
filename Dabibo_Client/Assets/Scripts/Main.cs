using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public GameObject gameMgr;

	// Use this for initialization
	void Awake ()
	{
		if(GameMgr.instance == null)
			Instantiate(gameMgr);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
