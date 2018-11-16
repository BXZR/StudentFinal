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
		skillName = "治疗";//技能名字
		skillInformation = "恢复最大生命10%的生命值";//技能介绍
		skillAllTimer = 10f;//冷却时间
		skillEffectTime = 0.2f;//技能持续时间
		thePlayer = this.GetComponentInParent<Player>();
	}

	//播放技能动画
	public override void UseTheSkill ()
	{
		theStateNow = skillState.isUsing;
		thePlayer.theSkillNow = this;
		thePlayer.hpNow += thePlayer.hpMaxNow * 0.1f;
	}



}
