using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public int type = 1;
	public int healthValue = 10;
	
	// Use this for initialization
	public int healthUpgrade() {
		return healthValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
