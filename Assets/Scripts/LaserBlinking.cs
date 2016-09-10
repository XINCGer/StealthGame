/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/02
*/
using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour
{

		public float onTime;	//间隔onTime后灯灭
		public float offTime; 	//间隔offTime后灯亮
		private float timer; 	//记录流逝的时间
		private Renderer laserRenderer; 	//Laser对象上的Render组件对象
		private Light laserLight; 	//Laser对象上的Light组件

		// Use this for initialization
		void Start ()
		{
				laserRenderer = GetComponent<Renderer> ();
				laserLight = GetComponent<Light> ();
				timer = 0.0f;
		}
	
		// Update is called once per frame
		void Update ()
		{
				timer += Time.deltaTime;
				if (laserRenderer.enabled && timer >= onTime) {
						SwitchBeam ();
				}
				if (!laserRenderer.enabled && timer >= offTime) {
						SwitchBeam ();
				}
		}

		//用于切换Renderer和Light组件enable属性的方法
		void SwitchBeam ()
		{
				timer = 0.0f;
				//切换Renderer组件和Light组件的enable属性
				laserRenderer.enabled = !laserRenderer.enabled;
				laserLight.enabled = !laserLight.enabled;
		}
}
