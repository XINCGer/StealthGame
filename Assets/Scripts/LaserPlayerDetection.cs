/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/02
*/
using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag (Tags.Player);
		lastPlayerSighting = GameObject.FindWithTag (Tags.GameController).GetComponent<LastPlayerSighting>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider other){
		if(GetComponent<Renderer>().enabled){
			if(other.gameObject==player){
				lastPlayerSighting.position=other.transform.position;
			}
		}
	}
}
