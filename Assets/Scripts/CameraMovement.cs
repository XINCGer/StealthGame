/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/06
*/
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float smooth=1.5f;
	private Transform player;	//Player对象的Transform组件
	private Vector3 relCameraPos;	//Player到摄像机的向量
	private float relCameraPosMag;	//Player到摄像机向量的长度
	private Vector3 newPos;


	void Awake(){
		player = GameObject.FindWithTag (Tags.Player).transform;
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;	//magnitude表示向量relCameraPos的长度
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//检测给定位置是否适合
	bool ViewingPositionCheck(Vector3 checkPos){
		RaycastHit hit;
		if(Physics.Raycast(checkPos,player.position-checkPos,out hit,relCameraPosMag)){
			if(hit.transform!=player)
				return false;
		}
		newPos = checkPos;
		return true;
	}

	//让摄像机始终正对角色
	void SmoothLookAt(){
		Vector3 relPlayerPosition = player.position - transform.position;	//摄像机到Player的向量
		Quaternion lookAtRotation = Quaternion.LookRotation (relPlayerPosition,Vector3.up);	//摄像机从当前位置旋转到player位置的旋转量
		transform.rotation = Quaternion.Lerp (transform.rotation,lookAtRotation,smooth*Time.deltaTime);	//将摄像机从当前值均匀的插值到目标值
	}

	void FixedUpdate(){
		Vector3 standardPos = player.position + relCameraPos;
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		Vector3 []checkPoints=new Vector3[5];
		checkPoints [0] = standardPos;
		checkPoints [1] = Vector3.Lerp (standardPos,abovePos,0.25f);
		checkPoints [2] = Vector3.Lerp (standardPos,abovePos,0.5f);
		checkPoints [3] = Vector3.Lerp (standardPos,abovePos,0.75f);
		checkPoints [4] = abovePos;
		for(int i=0;i<checkPoints.Length;i++){
			if(ViewingPositionCheck(checkPoints[i])){
				break;
			}
		}
		transform.position = Vector3.Lerp (transform.position,newPos,smooth*Time.deltaTime);
		SmoothLookAt ();
	}
}
