/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/07
*/
using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;		//是否需要钥匙才能打开门
	public AudioClip doorSwitchClip; 	//打开门时的音频文件
	public AudioClip accessDeniedClip;	//开门被拒绝音频文件
	private Animator anim;	//Animator动画组件
	private HashIDs hash; 
	private GameObject player;
	private PlayerInventory playerInventory;
	private int count;


	void Awake(){
		anim=GetComponent<Animator>();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashIDs>();
		player = GameObject.FindWithTag (Tags.Player);
		playerInventory=player.GetComponent<PlayerInventory>();

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool (hash.openBool,count>0);
		AudioSource audio=GetComponent<AudioSource>();
		if(anim.IsInTransition(0)&&!audio.isPlaying){
			audio.clip=doorSwitchClip;
			audio.Play();
		}
	}


	void OnTriggerEnter(Collider other){
		AudioSource audio=GetComponent<AudioSource>();
		if(other.gameObject==player){
			if(requireKey){
				if(playerInventory.hasKey)
					count++;
				else{
					audio.clip=accessDeniedClip;
					//Debug.Log("NONONONO");
					audio.Play();
				}
			}
			else count++;
		}
		else if(other.gameObject.tag==Tags.Enemy){
			if(other is CapsuleCollider)
				count++;
		}
	}

	//处理角色或者敌人离开的事件
	void OnTriggerExit(Collider other){
		if(other.gameObject==player || (other.gameObject.tag==Tags.Enemy&&other is CapsuleCollider)){
			count=Mathf.Max(0,count-1);
		}
	}
}
