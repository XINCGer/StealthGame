/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/09
*/
using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float deadZone=5f;
	private Transform player;
	private EnemySight enemySight;
	private NavMeshAgent nav;
	private Animator anim;
	private HashIDs hash;
	private SimpleLocomotion locomotion;


	void Awake(){
		player = GameObject.FindWithTag (Tags.Player).transform;
		enemySight=GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent> ();
		anim=GetComponent<Animator>();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashIDs>();
		nav.updateRotation = false;		//不允许Nav更新敌人的方向
		locomotion = new SimpleLocomotion (anim,hash);
		anim.SetLayerWeight (1,1f);		//设定动画层Shooting\Gun的权重
		anim.SetLayerWeight (2,1f);
		deadZone *= Mathf.Deg2Rad;		//将deadZone的单位从角度转换到弧度
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		NavAnimSetup ();
	}

	void OnAnimatorMove(){
		nav.velocity = anim.deltaPosition / Time.deltaTime;		//计算出animator的运动速度
		transform.rotation = anim.rootRotation;
	}

	//计算两个给定向量的夹角
	float FindAngle(Vector3 fromVector,Vector3 toVector,Vector3 upVector){
		if(toVector==Vector3.zero){
			return 0f;
		}
		float angle = Vector3.Angle (fromVector,toVector);
		Vector3 normal = Vector3.Cross (fromVector,toVector);
		angle *= Mathf.Sign (Vector3.Dot(normal,upVector));
		angle *= Mathf.Deg2Rad;
		return angle;
	}

	void NavAnimSetup(){
		float speed;
		float angle;
		if(enemySight.playerInSight){
			speed=0f;
			angle=FindAngle(transform.forward,player.position-transform.position,transform.up);
		}
		else{
			speed=Vector3.Project(nav.desiredVelocity,transform.forward).magnitude;
			angle=FindAngle(transform.forward,nav.desiredVelocity,transform.up);
			if(Mathf.Abs(angle)<deadZone){
				transform.LookAt(transform.position+nav.desiredVelocity);
				angle=0f;
			}
			locomotion.Do(speed,angle);
		}
	}
}
