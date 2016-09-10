/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/06
*/
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

		public AudioClip shoutingClip;	//Player对象喊叫时播放的音频
		public float turnSmoothing = 15.0f;	//插值系数
		public float speedDampTime = 0.1f;
		private Animator animator;	//Player对象上的Animator组件
		private HashIDs hash;	//HashIDs脚本组件

		void Awake ()
		{
				animator = GetComponent<Animator> ();
				hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashIDs> ();
				//设置Shouting动画层的权重为1
				animator.SetLayerWeight (1, 1f);
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				bool shout = Input.GetButtonDown ("Attract");
				animator.SetBool (hash.shoutingBool, shout);
				AudioManagement (shout);
		}

		//实现刚体的旋转
		void Rotating (float h, float v)
		{
				Vector3 targetDir = new Vector3 (h, 0, v);
				Quaternion targetRotation = Quaternion.LookRotation (targetDir, Vector3.up);
				Rigidbody r = GetComponent<Rigidbody> ();
				Quaternion newRotation = Quaternion.Lerp (r.rotation, targetRotation, turnSmoothing * Time.deltaTime);
				r.MoveRotation (newRotation);
		}

		//实现角色的移动
		void MovementManagement (float h, float v, bool sneaking)
		{
				animator.SetBool (hash.sneakingBool, sneaking);
				if (h != 0 || v != 0) {
						Rotating (h, v);
						animator.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
				} else {
						animator.SetFloat (hash.speedFloat, 0f);
				}
		}

		void FixedUpdate ()
		{
				float h = Input.GetAxis ("Horizontal");
				float v = Input.GetAxis ("Vertical");
				bool sneak = Input.GetButton ("Sneak");
				MovementManagement (h, v, sneak);
		}

		//管理音频播放
		void AudioManagement (bool shout)
		{
				AudioSource audioSource = GetComponent<AudioSource> ();
				if (animator.GetCurrentAnimatorStateInfo (0).fullPathHash == hash.locomotionState) {
						if (!audioSource.isPlaying)
								audioSource.Play ();
						//Debug.Log ("Audio Play");
				} else
						audioSource.Stop ();
				if (shout) {
						AudioSource.PlayClipAtPoint (shoutingClip, transform.position);
				}
		}
}
