using UnityEngine;
using System.Collections;

public class TestButtonScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseOver()
	{
		GetComponent<Renderer>().material.color = Color.red;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;

	}

	void OnMouseDown()
	{
		GetComponent<Renderer>().material.color = Color.green;
	}

}
