using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZiYingArrow : SkillBasic {

	public GameObject Arrow;//弹矢引用保存
	Arrows ArrowUsing;//弹矢引用保存
	Vector3 forward;

	private animatorController theAnimatorController;

	void Start ()
	{
		Init ();
		theAnimatorController = thePlayer.GetComponent<animatorController> ();
	}

	public override void Init ()
	{
		skillName = "剑气";//技能名字
		skillInformation = "发射剑气进行穿透伤害";//技能介绍
		skillAllTimer = 0.5f;//冷却时间
		skillEffectTime = 0.2f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		thePlayer.theSkillNow = this;
		theAnimatorController.PlayAnimation (playerAction.attack);
	}


	//真正的技能效果
	public override void SkillEffect (float extradamage)
	{
		//没有控制者就不发
		if (!this.thePlayer || theStateNow!= skillState.isReady)
			return;

		theStateNow = skillState.isUsing;

		forward = this.thePlayer.transform.forward;
		//考虑到多种连发的情况，暂时还是不做弹矢的对象池子，后期优化吧
		if (!ArrowUsing) 
			ArrowUsing = GameObject.Instantiate (Arrow).GetComponent<Arrows>();

		ArrowUsing.gameObject.SetActive (true);
		ArrowUsing.transform.position = thePlayer.transform.position + thePlayer.transform.rotation * new Vector3 (0f, 0.2f * thePlayer.transform.localScale.y + 0.25f, 0.5f);
		ArrowUsing.transform.forward = forward;

		Invoke ("effectDestoryExtra", skillEffectTime);
	}
		
	public  void effectDestoryExtra ()
	{
		if (ArrowUsing)
		{
			try
			{
				ArrowUsing.gameObject.SetActive(false); 
				//DestroyImmediate(ArrowUsing.gameObject);
				theStateNow = skillState.isCooling;
			}
			catch(Exception d){Destroy (ArrowUsing);}
		}
	}


}
