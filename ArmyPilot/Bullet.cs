using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            GameObject.Find("FPSController").GetComponent<Health>().CurHealt -= GameObject.Find("ArmyPilotSinper").GetComponent<AiSniper>().Damage;
        }
    }
}
