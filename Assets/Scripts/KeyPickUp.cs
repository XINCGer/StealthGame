/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/07
*/
using UnityEngine;
using System.Collections;

public class KeyPickUp : MonoBehaviour {

	public AudioClip keyGrab;	//当角色拿到钥匙卡时播放的音频
	private GameObject player;	//角色对象
	private PlayerInventory playerInventory;	//管理角色钥匙卡状态的playerInventory对象

	void Awake(){
		player = GameObject.FindWithTag (Tags.Player);
		playerInventory=player.GetComponent<PlayerInventory>();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//拾取钥匙的函数
	void OnTriggerEnter(Collider other){
		if(other.gameObject==player){
			AudioSource.PlayClipAtPoint(keyGrab,transform.position);
			playerInventory.hasKey=true;
			Destroy(gameObject);
		}
	}
}
