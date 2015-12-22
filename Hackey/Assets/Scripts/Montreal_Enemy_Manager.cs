using UnityEngine;
using System.Collections;

public class Montreal_Enemy_Manager : MonoBehaviour {

	public static int mtlPlayerCount;
	public const int startMTLCount = 4;
	public Transform mtlEnemy;

	public Transform[] spPoints;

	// Use this for initialization
	void Start () {
		mtlPlayerCount = startMTLCount;

		for (int i = 0; i < startMTLCount; i++) {
			Instantiate(mtlEnemy, spPoints[i].position, Quaternion.identity);
			mtlEnemy.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
