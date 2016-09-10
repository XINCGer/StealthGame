/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/09
*/
using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

	public float maximunDamage=120f;	//子弹威力最大值
	public float minimumDamage=45f;		//子弹威力最小值
	public AudioClip shotClip;
	public float flashIntensity=3f;
	public float fadeSpeed=10f;
	private Animator anim;
	private HashIDs hash;
	private LineRenderer laserShotLine;
	private Light laserShotLight;
	private SphereCollider col;
	private Transform player;
	private PlayerHealth playerHealth;
	private bool shooting;
	private float scaledDamage;


	void Awake(){
		anim=GetComponent<Animator>();
		laserShotLine=GetComponentInChildren<LineRenderer>();
		laserShotLight=laserShotLine.gameObject.GetComponent<Light>();
		col=GetComponent<SphereCollider>();
		player = GameObject.FindWithTag (Tags.Player).transform;
		playerHealth=player.gameObject.GetComponent<PlayerHealth>();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashIDs>();
		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
		scaledDamage = maximunDamage - minimumDamage;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float shot = anim.GetFloat (hash.shotFloat);
		if (shot > 0.5f && !shooting)
						Shoot ();
		if(shot<0.5f){
			shooting=false;
			laserShotLine.enabled=false;
		}
		laserShotLight.intensity = Mathf.Lerp (laserShotLight.intensity,0f,fadeSpeed*Time.deltaTime);

	}

	//反向动力学IK
	void OnAnimatorIK(int layerIndex){	//layerIndex表示动画层的序号
		float aimWeight = anim.GetFloat (hash.aimWeightFloat);
		anim.SetIKPosition (AvatarIKGoal.RightHand,player.position+Vector3.up*1.5f);
		anim.SetIKPositionWeight (AvatarIKGoal.RightHand,aimWeight);
	}

	//实现射击效果
	void ShotEffects(){
		laserShotLine.SetPosition (0,laserShotLine.transform.position);
		laserShotLine.SetPosition (1,player.position+Vector3.up*1.5f);
		laserShotLine.enabled = true;
		laserShotLight.intensity = flashIntensity;
		AudioSource.PlayClipAtPoint (shotClip,laserShotLight.transform.position);
	}

	//实现射击动作
	void Shoot(){
		shooting = true;
		float fractionalDistance=(col.radius-Vector3.Distance(transform.position,player.position))/col.radius;
		//计算生命值的减少因子，当Player对象距离敌人越近的时候，damage的值也就越大
		float damage = scaledDamage * fractionalDistance + minimumDamage;
		playerHealth.TakeDamage (damage);
		ShotEffects ();
	}
}
