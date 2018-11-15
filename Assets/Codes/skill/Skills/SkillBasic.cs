﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum skillState {isUsing , isCooling , isReady}
public class SkillBasic : MonoBehaviour {

	//仅仅是一个简单的技能
	//这个游戏根本就没有复杂的技能系统

	//技能公有参数
	public string skillName = "";//技能名字
	public string skillInformation = "";//技能介绍
	public float timerForAdd = 0f;//累加时间，用于计算和显示
	public float skillAllTimer = 1f;//冷却时间
	public float skillEffectTime = 1f;//技能持续时间
	public skillState theStateNow = skillState.isReady;//当前技能的状态
	public float theStatePercent = 0f;//用于显示和计算的当前技能状态百分比
	public Player thePlayer;//使用技能的人物
	public UISkillButton theButton;//技能对应的按钮
	//技能虚方法
	public virtual void Init(){}
	public virtual void SkillEffect(float extradamage){theStateNow = skillState.isUsing;}//真正的技能效果
	public virtual void UseTheSkill(){}//播放技能动画
	public virtual void OnUpdate(){}//持续效果
	public virtual void OnReady(){}//当冷却完成之后发生
	public virtual void OnCool(){}//当效果完成之后进入冷却的时候发生



	/// <summary>
	/// 冷却时间计算
	/// 技能通用方法
	/// 返回的是冷却时间百分比
	/// </summary>
	public void makeTimeCanculate()
	{
		if (theStateNow == skillState.isReady) 
		{
			timerForAdd = 0f;
			theStatePercent = 0f;
		} 
		else if (theStateNow == skillState.isUsing) 
		{
			timerForAdd += Time.deltaTime;
			if (timerForAdd < skillEffectTime)
				theStatePercent = timerForAdd / skillEffectTime;
			else
			{
				theStateNow = skillState.isCooling;
				OnCool ();
			}
		} 
		else if (theStateNow == skillState.isCooling) 
		{
			timerForAdd += Time.deltaTime;
			if (timerForAdd < skillAllTimer)
				theStatePercent = (timerForAdd - skillEffectTime) / (skillAllTimer - skillEffectTime);
			else
			{
				theStateNow = skillState.isReady;
				OnReady ();
			}
		}
		//print ("theStatePercent = "+theStatePercent);
	}
}
