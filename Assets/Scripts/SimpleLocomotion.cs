/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/08
*/
using UnityEngine;
using System.Collections;

public class SimpleLocomotion {
	public float speedDampTime=0.1f;	//表示Animator.SetFloat()参数Speed达到目标值的时间
	public float angularSpeedDampTime=0.7f;		//表示Animator.SetFloat()中参数AngularSpeed达到目标值的时间
	public float angleResponseTime = 0.6f;		//计算角速度时，变化角度angle所需时间
	private Animator anim;
	private HashIDs hash;

	public SimpleLocomotion(Animator animator,HashIDs hashIDs){
		anim = animator;
		hash = hashIDs;
	}

	//设置animator的角速度和线速度
	public void Do(float speed,float angle){
		float angularSpeed = angle / angleResponseTime;
		anim.SetFloat (hash.speedFloat,speed,speedDampTime,Time.deltaTime);
		anim.SetFloat (hash.angularSpeedFloat,angularSpeed,angularSpeedDampTime,Time.deltaTime);
	}

}
