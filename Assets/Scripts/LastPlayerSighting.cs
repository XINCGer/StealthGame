/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/01
*/
using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour
{

		public Vector3 position = new Vector3 (1000f, 1000f, 1000f);	//起始位置
		public Vector3 resetPosition = new Vector3 (1000f, 1000f, 1000f);
		public float lightHighIntensity = 0.25f;	//高亮度
		public float lightLowIntensity = 0f;		//低亮度
		public float fadeSpeed = 7f;
		public float musicFadeSpeed = 1f;
		public AlarmLight alarmScript; //AlarmLight脚本对象
		private Light mainLight;	//主灯光上面的Light对象
		private AudioSource music;	//背景音乐
		private AudioSource[] sirens;	//警报音乐
		private AudioSource panicAudio;	 //当角色处于危险时播放的音乐
		private const float muteVolume = 0f;  //静音音量
		private const float normalVolume = 0.8f;	//正常音量

		void Awake ()
		{
				alarmScript = GameObject.FindWithTag (Tags.AlarmLight).GetComponent<AlarmLight> ();
				mainLight = GameObject.FindWithTag (Tags.MainLight).GetComponent<Light> ();
				music = GetComponent<AudioSource> ();
				panicAudio = transform.Find ("secondary_music").GetComponent<AudioSource> ();
				GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag (Tags.Siren);
				sirens = new AudioSource[sirenGameObjects.Length];
				for (int i=0; i<sirens.Length; i++) {
						sirens [i] = sirenGameObjects [i].GetComponent<AudioSource> ();
				}
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				SwithchAlarm ();
				MusicFading ();
		}

		//用于切换警报声源
		void SwithchAlarm ()
		{
				//当角色位置变动，即进入了危险状态，则开启警报
				alarmScript.alarmOn = (position != resetPosition);
				float newIntensity;
				//当角色进入危险状态时，将主灯光调暗，安全时将灯光调亮
				if (position != resetPosition) {
						newIntensity = lightLowIntensity;
				} else {
						newIntensity = lightHighIntensity;
				}
				mainLight.intensity = Mathf.Lerp (mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);
				//遍历音频列表，当角色处于危险状态切音频没有播放的时候，开启播放，否则关闭
				for (int i=0; i<sirens.Length; ++i) {
						if (position != resetPosition && !sirens [i].isPlaying)
								sirens [i].Play ();
						else if (position == resetPosition) {
								sirens [i].Stop ();
						}
				}
		}

		//实现音量大小渐变的效果
		void MusicFading ()
		{
				if (position != resetPosition) {
						music.volume = Mathf.Lerp (music.volume, muteVolume, musicFadeSpeed * Time.deltaTime);
						panicAudio.volume = Mathf.Lerp (panicAudio.volume, normalVolume, musicFadeSpeed * Time.deltaTime);
				} else {
						music.volume = Mathf.Lerp (music.volume, normalVolume, musicFadeSpeed * Time.deltaTime);
						panicAudio.volume = Mathf.Lerp (panicAudio.volume, muteVolume, musicFadeSpeed * Time.deltaTime);
				}
		}
}
