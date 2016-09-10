/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/08
*/
using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public float fieldOfViewAngle=110f;	//FOV角度
	public bool playerInSight;		//是否发现角色
	public Vector3 personalLastSighting;	//敌人观察到角色的最后位置
	private NavMeshAgent nav;	//导航网格代理对象，用于自动寻找角色
	private SphereCollider col;		//球形触发器对象
	private Animator anim;			//在Enemy001对象上绑定的Animator组件，这里是DoneEnemyAnimator
	private LastPlayerSighting lastPlayerSighting;	//gameController对象上绑定的LastPlayerSighting脚本对象
	private GameObject player;
	private Animator playerAnim;
	private PlayerHealth playerHealth;
	private HashIDs hash;
	private Vector3 previousSighting;	//上一帧中对象被观察到的位置

	void Awake(){
		nav=GetComponent<NavMeshAgent>();
		col=GetComponent<SphereCollider>();
		anim=GetComponent<Animator>();
		lastPlayerSighting = GameObject.FindWithTag (Tags.GameController).GetComponent<LastPlayerSighting>();
		player = GameObject.FindWithTag (Tags.Player);
		playerAnim=player.GetComponent<Animator>();
		playerHealth=player.GetComponent<PlayerHealth>();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashIDs>();
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	//当角色离开敌人的碰撞检测区以后，设置playerInSight为false
	void Update () {
		if(lastPlayerSighting.position!=previousSighting){
			personalLastSighting=lastPlayerSighting.position;
		}
		previousSighting = lastPlayerSighting.position;
		if(playerHealth.health>0f){
			anim.SetBool(hash.playerInSightBool,playerInSight);
		}
		else{
			anim.SetBool(hash.playerInSightBool,false);
		}
	}

	//计算导航路径长度，起点为Enemy001,终点为角色对象
	float CalculatePathLenght(Vector3 targetPosition){
		NavMeshPath path = new NavMeshPath ();
		if(nav.enabled){
			nav.CalculatePath(targetPosition,path);
		}
		Vector3[] allWayPoints = new Vector3[path.corners.Length+2];
		allWayPoints [0] = transform.position;
		allWayPoints [allWayPoints.Length - 1] = targetPosition;
		for (int i=0; i<path.corners.Length; i++){
			allWayPoints [i + 1] = path.corners [i];
		}
		float pathLength = 0;
		for(int i=0;i<allWayPoints.Length-1;i++){
			pathLength+=Vector3.Distance(allWayPoints[i],allWayPoints[i+1]);
		}
		return pathLength;
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject==player){
			playerInSight=false;
			Vector3 direction=other.transform.position-transform.position;
			float angle=Vector3.Angle(direction,transform.forward);
			if(angle<fieldOfViewAngle*0.5f){
				RaycastHit hit;
				if(Physics.Raycast(transform.position+transform.up,direction.normalized,out hit, col.radius)){
					if(hit.collider.gameObject==player){
						playerInSight=true;
						lastPlayerSighting.position=player.transform.position;
					}
				}
			}
			int state0=playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
			int state1=playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;
			if(state0==hash.locomotionState||state1==hash.shoutState){
				if(CalculatePathLenght(player.transform.position)<=col.radius)
					personalLastSighting=player.transform.position;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject==player){
			playerInSight=false;
		}
	}
	
}
