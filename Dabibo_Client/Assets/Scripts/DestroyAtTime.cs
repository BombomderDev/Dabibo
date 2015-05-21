using UnityEngine;
using System.Collections;

public class DestroyAtTime : MonoBehaviour {
	public float time = 0.8f;
	void Start ()
	{
		Destroy(this.gameObject,time);	
	}
}
