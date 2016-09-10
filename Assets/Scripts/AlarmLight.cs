/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/08/31
*/
using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

	public float fadeSpeed=2.0f;	//表示警报灯光亮度渐变速度
	public float highIntensity=4f;	//警报灯的最大值
	public float lowIntensity=0.5f;	//警报灯的最小值
	public float changeMargin=0.2f;	//切换变量
	public bool alarmOn;	//是否开启警报
	private float targetIntensity;	//目标亮度值，取值为最大值或最小值
	private Light alarmLight;	//当前场景中的警报灯对象

	void Awake(){
		//取得绑定的灯光组件
		alarmLight = GetComponent<Light> ();
		alarmLight.intensity = 0.0f;	//初始化亮度为0
		targetIntensity = highIntensity;	//目标亮度值初始值为最大亮度值
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(alarmOn){
			//当警报开启的时候，警报灯的亮度从当前值均匀增加到最亮值
			alarmLight.intensity=Mathf.Lerp(alarmLight.intensity,targetIntensity,fadeSpeed*Time.deltaTime);
			CheckTargetIntensity();
		}
		else{
			alarmLight.intensity=Mathf.Lerp(alarmLight.intensity,0.0f,fadeSpeed*Time.deltaTime);
		}
	}

	void CheckTargetIntensity(){
		//在灯光的最大亮度和最小亮度之间不断的转换状态
		if(Mathf.Abs(targetIntensity-alarmLight.intensity)<changeMargin){
			if(targetIntensity==highIntensity)targetIntensity=lowIntensity;
			else targetIntensity=highIntensity;
		}
	}
}
