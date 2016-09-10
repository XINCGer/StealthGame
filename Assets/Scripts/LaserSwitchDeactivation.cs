/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/07
*/
using UnityEngine;
using System.Collections;

/**
 * 脚本实现功能，当玩家进入sphere collider触发区域后，若按下Switch键(Z键),则关闭与此开关相关联的激光栅栏
 * 关闭激光栅栏的同时，切换开关台上方锁的纹理，表明锁已经打开。并且播放开锁音频文件
 */
public class LaserSwitchDeactivation : MonoBehaviour {

	public GameObject laser;	//激光栅栏对象，由外部指定
	public Material unlockedMat;	//锁开时所用的纹理
	public GameObject player;	//玩家对象

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//实现用于切换纹理，关闭激光栅栏，播放音频的函数
	void LaserDeactivation(){
		laser.SetActive (false);	//禁用激光栅栏
		Renderer screen = transform.Find ("prop_switchUnit_screen").GetComponent<Renderer>();	//获取Renderer组件
		screen.material=unlockedMat;
		GetComponent<AudioSource>().Play();
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject==player){
			if(Input.GetButton("Switch")){
				LaserDeactivation();
			}
		}
	}
}
