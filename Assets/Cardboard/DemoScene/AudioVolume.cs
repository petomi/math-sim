using UnityEngine;
using System.Collections;

public class AudioVolume : MonoBehaviour {
	public float minVolume = 0.25f;
	public float maxVolume = 1.0f;
	public float currentVolume = 0.0f;
	private float currentTime = 0.0f;

//
//	// Use this for initialization
//	void Start () 
//	{
//		AudioSource.volume = minVolume;
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if (minVolume < currentVolume < maxVolume)
//		{
//			AudioSource.volume = minVolume;
//	
//		}
//	}
}
