using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHpup : SkillBasic {


	void Start ()
	{
		Init ();

	}

	public override void Init ()
	{
		skillAllTimer = 10f;//冷却时间
		skillEffectTime = 0.2f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
		skillName = "调息";//技能名字
		skillInformation = "行走江湖必备技能，能够治疗自身的损伤。\n恢复最大生命10%的生命值\n冷却时间：" + skillAllTimer.ToString("f1") + "秒";//技能介绍
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;
		thePlayer.OnHpChange(thePlayer.hpMaxNow * 0.1f);
	}



}
