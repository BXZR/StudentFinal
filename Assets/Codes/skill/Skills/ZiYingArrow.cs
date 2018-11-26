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
		skillAllTimer = 0.5f;//冷却时间
		skillEffectTime = 0.2f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "气剑指";//技能名字
		skillInformation = "昆仑琼华派的基础招式之一。\n将剑气凝于指尖激射而出，对面前的敌人造成穿透伤害。" +
			"\n剑气持续："+skillEffectTime.ToString("f1")+"秒 冷却时间："+(skillAllTimer - skillEffectTime ).ToString("f1")+"秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		thePlayer.theSkillNow = this;
		SearchAim ();
		theAnimatorController.PlayAnimation (playerAction.attack);
	}


	//尝试看向身边最近的目标
	private GameObject theAim = null;
	private void SearchAim()
	{
		if (theAim ) 
		{
			this.thePlayer.theMoveController.MakeLookAt (theAim.transform);
		}
		else 
		{
			Collider[] attackAims = Physics.OverlapSphere (this.thePlayer.transform.position, 12f);
			GameObject aAim = null;
			float distance = 999f;
			for (int i = 0; i < attackAims.Length; i++) 
			{
				Player thePlayerGet = attackAims [i].GetComponent<Player> ();
				if (!thePlayerGet || thePlayerGet == this.thePlayer)
					continue;
			
				float distanceNew = Vector3.Distance (this.thePlayer.transform.position, attackAims [i].transform.position);
				if (distanceNew < distance) 
				{
					distance = distanceNew;
					aAim = attackAims [i].gameObject;
				}
			}
			if (aAim) 
			{
				theAim = aAim;
				this.thePlayer.theMoveController.MakeLookAt (theAim.transform);
			}
		}
	}


	//真正的技能效果
	public override void SkillEffect (float extradamage)
	{

		theStateNow = skillState.isUsing;

		forward = this.thePlayer.transform.forward;
		//考虑到多种连发的情况，暂时还是不做弹矢的对象池子，后期优化吧
		if (!ArrowUsing) 
		{
			ArrowUsing = GameObject.Instantiate (Arrow).GetComponent<Arrows> ();
			ArrowUsing.thePlayer = this.thePlayer;
		}

		ArrowUsing.gameObject.SetActive (true);
		ArrowUsing.transform.position = thePlayer.transform.position + thePlayer.transform.rotation * new Vector3 (0f, 0.2f * thePlayer.transform.localScale.y + 0.25f, 0.15f);
		ArrowUsing.transform.forward = forward;

		Invoke ("effectDestoryExtra", skillEffectTime);
	}
		
	public  void effectDestoryExtra ()
	{
		if (ArrowUsing)
		{
			try{ArrowUsing.gameObject.SetActive(false); }
			catch(Exception d){Destroy (ArrowUsing);}
		}
	}




}
