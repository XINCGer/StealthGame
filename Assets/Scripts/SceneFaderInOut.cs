/*
ProjectName: 潜行游戏
Author: 马三小伙儿
Blog: http://www.cnblogs.com/msxh/
Github:https://github.com/XINCGer
Date: 2016/09/01
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFaderInOut : MonoBehaviour {

	public float fadeSpeed=1.5f;
	private bool sceneStaring=true;
	private RawImage rawImage=null;


	void Awake(){
		rawImage = GetComponent<RawImage> ();		//获取RawImage对象
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(sceneStaring){
			StartScene();
		}
	}

	//淡入效果
	void FadeToClear(){
		rawImage.color = Color.Lerp (rawImage.color,Color.clear,fadeSpeed*Time.deltaTime);
	}

	//淡出效果
	void FadeToBlack(){
		rawImage.color = Color.Lerp (rawImage.color,Color.black,fadeSpeed*Time.deltaTime);
	}

	//场景开始函数
	void StartScene(){
		FadeToClear ();
		if(rawImage.color.a<=0.05f){
			rawImage.color=Color.clear;
			rawImage.enabled=false;
			sceneStaring=false;
		}
	}

	//场景结束函数
	public void EndScene(){
		rawImage.enabled = true;
		FadeToBlack ();
		if(rawImage.color.a>=0.95f){
			Application.LoadLevel(0);
		}
	}
}
