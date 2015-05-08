using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleDistortionScript : MonoBehaviour 
{
	public GameObject titleNormal;
	public GameObject titleCorrupted;
	private bool isCorrupted = true;


	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Distortion ());
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{

	
	}

	public IEnumerator Distortion()
	{
		titleCorrupted.SetActive (isCorrupted);
		titleNormal.SetActive (!isCorrupted);
		int i = Random.Range (0, 2);
		isCorrupted = !isCorrupted;
		yield return new WaitForSeconds (i);
		StartCoroutine (Distortion ());
	}
}
