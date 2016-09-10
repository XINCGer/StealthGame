/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/08
*/
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float health=100f;	//角色的生命值
	public float resetAfterDeathTime=5f;	//表示角色死亡后多久游戏才重新开始
	public AudioClip deathClip;	//角色死亡时播放的音频
	private Animator anim;
	private PlayerMovement playerMovement;	//角色死亡后移动控制
	private HashIDs hash;
	private SceneFaderInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private float timer;
	private bool playerDead;	//角色是否死亡


	void Awake(){
		anim=GetComponent<Animator>();
		playerMovement=GetComponent<PlayerMovement>();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashIDs>();
		sceneFadeInOut = GameObject.FindWithTag (Tags.Fader).GetComponent<SceneFaderInOut>();
		lastPlayerSighting = GameObject.FindWithTag (Tags.GameController).GetComponent<LastPlayerSighting>();

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health<=0f){
			if(!playerDead){
				PlayerDying();
			}
			else{
				PlayerDead();
				LevelReset();
			}
		}
	}

	//Player对象垂死时的状态
	void PlayerDying(){
		playerDead = true;
		anim.SetBool (hash.deadBool,playerDead);
		AudioSource.PlayClipAtPoint (deathClip,transform.position);
	}

	//表示角色死亡
	void PlayerDead(){
		if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash==hash.dyingState){
			anim.SetBool(hash.deadBool,false);
			anim.SetFloat(hash.speedFloat,0f);
			playerMovement.enabled=false;
			lastPlayerSighting.position=lastPlayerSighting.resetPosition;
			GetComponent<AudioSource>().Stop();
		}
	}
	//resetAfterDeathTime时间以后游戏结束
	void LevelReset(){
		timer += Time.deltaTime;
		if(timer>=resetAfterDeathTime){
			sceneFadeInOut.EndScene();
		}
	}

	//当角色受到伤害时生命值会减少amount
	public void TakeDamage(float amount){
		health-=amount;
	}
}
