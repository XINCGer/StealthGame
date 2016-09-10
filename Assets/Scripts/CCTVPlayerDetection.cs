/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/01
*/
using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour {

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
		if(other.gameObject==player){
			Vector3 relPlayerPos=player.transform.position-transform.position;
			RaycastHit hit;
			if(Physics.Raycast(transform.position,relPlayerPos,out hit)){
				if(hit.collider.gameObject==player){
					lastPlayerSighting.position=player.transform.position;
				}

			}
		}
	}
}
