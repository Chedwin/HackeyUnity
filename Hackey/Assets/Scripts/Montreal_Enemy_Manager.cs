using UnityEngine;
using System.Collections;

public class Montreal_Enemy_Manager : MonoBehaviour {

	public static int mtlPlayerCount = 4;
	public Transform mtlEnemy;

	public Transform[] spPoints;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			Instantiate(mtlEnemy, spPoints[i].position, Quaternion.identity);
			mtlEnemy.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
