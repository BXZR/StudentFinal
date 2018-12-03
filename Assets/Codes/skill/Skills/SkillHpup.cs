﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHpup : SkillBasic {


	float hpUpPerSecond = 0f;

	void Start ()
	{
		Init ();
	}

	public override void Init ()
	{
		skillAllTimer = 15;//冷却时间
		skillEffectTime = 3f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "五气朝元";//技能名字
		skillInformation = "琼华派特有的调息疗伤之法。\n恢复15%最大生命，半血以下效果提升至20%最大生命。\n持续时间："+skillEffectTime.ToString("f1")+"秒\n冷却时间：" + skillAllTimer.ToString("f1") + "秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;
		float rate = thePlayer.hpNow < thePlayer.hpMaxNow * 0.5f ? 0.2f : 0.15f;
		hpUpPerSecond = thePlayer.hpMaxNow * rate / skillEffectTime;
		InvokeRepeating ("makeTrueHpUp" , 0f , 1f);
	}

	float timeAdder = 0f;
	private  void  makeTrueHpUp()
	{
		thePlayer.OnHpChange( hpUpPerSecond );
		timeAdder += 1f;
		if (timeAdder >= skillEffectTime) 
		{
			CancelInvoke ();
			timeAdder = 0f;
		}
	}

//	public override void OnUpdate ()
//	{
//		if (theStateNow == skillState.isUsing)
//		{
//			thePlayer.OnHpChange( hpUpPerSecond * Time.deltaTime );
//		}
//	}

	void Update()
	{
		OnUpdate ();
	}



}
