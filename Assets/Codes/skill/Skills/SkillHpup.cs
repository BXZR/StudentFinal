using System.Collections;
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
		skillName = "调息";//技能名字
		skillInformation = "行走江湖必备技能，能够治疗自身的损伤。\n恢复共最大生命15%的生命值\n持续时间："+skillEffectTime.ToString("f1")+"秒\n冷却时间：" + skillAllTimer.ToString("f1") + "秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;
		hpUpPerSecond = thePlayer.hpMaxNow * 0.15f / skillEffectTime;
	}

	public override void OnUpdate ()
	{
		if (theStateNow == skillState.isUsing)
		{
			thePlayer.OnHpChange( hpUpPerSecond * Time.deltaTime );
		}
	}

	void Update()
	{
		OnUpdate ();
	}



}
