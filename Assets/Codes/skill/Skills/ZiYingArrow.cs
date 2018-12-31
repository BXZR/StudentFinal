using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZiYingArrow : SkillBasic {

	public GameObject Arrow;//弹矢引用保存
	Vector3 forward;
	public float searchDistance = 5f;
	private animatorController theAnimatorController;

	void Start ()
	{
		Init ();
		theAnimatorController = thePlayer.GetComponent<animatorController> ();
		InvokeRepeating("makeStepFlash" , 0f , 1f);
	}

	public override void Init ()
	{
		skillAllTimer = 0.25f;//冷却时间
		skillEffectTime = 0.25f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "琼华剑法";//技能名字
		skillInformation = "琼华派多段御剑之法。\n【化相真如剑】一束风云起。\n【千方残光剑】三刃共飞花。\n【上清破云剑】巨剑天上来。"
			+"\n剑气持续："+skillEffectTime.ToString("f1")+"秒 冷却时间："+(skillAllTimer - skillEffectTime ).ToString("f1")+"秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		if (canUseTheSkill ())
		{
			OnUse ();
			thePlayer.theSkillNow = this;
			SearchAim ();
			theAnimatorController.PlayAnimation (playerAction.attack);
		}
	}


	#region 真正的技能效果
	//真正的技能效果
	int stepNow = 0;
	float flashTimer = 0f;
	float flashTimerMax = 1f;
	public override void SkillEffect (float extradamage)
	{
		switch (stepNow) 
		{
		case 0:
		case 1:
			Step1 ();
			flashTimer += 1f;
			break;
		case 2:
			Step2 ();
			flashTimer += 1f;
			break;
		case 3:
			Step3 ();
			flashTimer += 1f;
			break;
		}
		stepNow++;
		stepNow = stepNow >= 4 ? 0 : stepNow;
	}

	void makeStepFlash()
	{
		flashTimer = Mathf.Clamp(flashTimer , 0f , 2f);
		flashTimer -= 1f;
		if (flashTimer < 0) 
		{
			flashTimer = flashTimerMax;
			stepNow = 0;
		}
	}
	#endregion



	#region 招式瞄准
	//尝试看向身边最近的目标
	private Acter theAim = null;
	private void SearchAim()
	{
		if (theAim && theAim.isAlive &&Vector3.Distance(theAim.transform.position , this.thePlayer.transform.position )< searchDistance) 
		{
			this.thePlayer.theMoveController.MakeLookAt (theAim.transform);
		}
		else 
		{
			Collider[] attackAims = Physics.OverlapSphere (this.thePlayer.transform.position, searchDistance);
			Acter aAim = null;
			float distance = 999f;
			for (int i = 0; i < attackAims.Length; i++) 
			{
				Acter thePlayerGet = attackAims [i].GetComponent<Acter> ();
				if (!thePlayerGet || thePlayerGet == this.thePlayer || !thePlayerGet.isAlive)
					continue;
			
				float distanceNew = Vector3.Distance (this.thePlayer.transform.position, attackAims [i].transform.position);
				if (distanceNew < distance) 
				{
					distance = distanceNew;
					aAim = thePlayerGet;
				}
			}
			if (aAim) 
			{
				theAim = aAim;
				this.thePlayer.theMoveController.MakeLookAt (theAim.transform);
			}
		}
	}
	#endregion



	#region 第一段 一束剑气
	Arrows ArrowUsing;//弹矢引用保存
	public void Step1()
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
	#endregion

	#region 第二段 三束剑气
	int arrowCounts = 3;
	float angleForArrow = 35f;
	List<Arrows> theArrows = new List<Arrows> ();
	public void Step2()
	{
		theStateNow = skillState.isUsing;
		Arrows theArrow ;
		for (int i = 0; i < arrowCounts; i++)
		{
			//四元数的方法在这里似乎不是很好用
			//forward = Quaternion.AngleAxis((float)(45*i), new Vector3(0,1,0)) *this.thePlayer.transform.forward ;
			//print ("forward = "+ forward);
			if (theArrows.Count < i + 1 || theArrows [i] == null) 
			{
				theArrow  = GameObject.Instantiate (Arrow).GetComponent<Arrows> ();
				theArrow .thePlayer = this.thePlayer;
				theArrows.Add (theArrow );
			} 
			else 
			{
				theArrows [i].gameObject.SetActive (true);
				theArrow = theArrows [i];
			}

			Vector3 positionNew = thePlayer.transform.position + thePlayer.transform.rotation * new Vector3 (0f, 0.2f * thePlayer.transform.localScale.y + 0.25f, 0.15f);
			theArrow.transform.position = positionNew;

			theArrow.transform.forward = this.thePlayer.transform.forward;
			theArrow.transform.Rotate (new Vector3 (0, (float)(-angleForArrow * (arrowCounts / 2) + angleForArrow * i), 0));

			if(arrowCounts %2 == 0)
				theArrow.transform.Rotate (new Vector3 (0, (float)(angleForArrow /2), 0));

			theArrow.transform.forward = theArrow.transform.forward;
			Invoke ("makeArrowOver" , skillEffectTime );
		}
	}


	void makeArrowOver()
	{
		theArrows.RemoveAll (X=>X==null);
		for (int i = 0; i < theArrows.Count; i++)
			if (theArrows [i].gameObject.activeInHierarchy) 
				theArrows [i].gameObject.SetActive (false);
	}
	#endregion


	#region 第三段 一束超大号剑气
	Arrows ArrowUsingBig;//弹矢引用保存
	public void Step3()
	{
		theStateNow = skillState.isUsing;
		forward = -this.thePlayer.transform.up;
		//考虑到多种连发的情况，暂时还是不做弹矢的对象池子，后期优化吧
		if (!ArrowUsingBig) 
		{
			ArrowUsingBig = GameObject.Instantiate (Arrow).GetComponent<Arrows> ();
			ArrowUsingBig.thePlayer = this.thePlayer;
			Vector3 scaleOld = ArrowUsingBig.transform.localScale;
			ArrowUsingBig.transform.localScale = new Vector3 (scaleOld.x *8f , scaleOld.y*7f , scaleOld.z *2f);
			ArrowUsingBig.GetComponent<TrailRenderer> ().widthMultiplier = 5f;
		}
		ArrowUsingBig.gameObject.SetActive (true);

		ArrowUsingBig.transform.position = thePlayer.transform.position + thePlayer.transform.rotation * new Vector3 (0f, 0.2f * thePlayer.transform.localScale.y + 2.25f, 2f);

		ArrowUsingBig.transform.forward = forward;
		Invoke ("effectDestoryExtraBig", skillEffectTime* 1.5f);
	}
	public  void effectDestoryExtraBig ()
	{
		if (ArrowUsingBig)
		{
			try{ArrowUsingBig.gameObject.SetActive(false); }
			catch(Exception d){Destroy (ArrowUsingBig);}
		}
	}
	#endregion




}
