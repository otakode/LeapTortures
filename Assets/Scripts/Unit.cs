using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

	void Start()
	{
	
	}

	void Update()
	{
	
	}

	void OnCollisionEnter()
	{
		Destroy(this.gameObject);
	}
}
