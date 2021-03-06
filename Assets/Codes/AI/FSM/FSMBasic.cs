﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMBasic  {

	//单机版游戏人物AI的有限状态机的基类
	public  float theViewAreaAngel = 180f;
	public  float theSearchLength = 5f;
	public  float theAttackLength = 1.25f;


	//统一的状态机初始化参数
	public NavMeshAgent theMoveController;//AI人物的移动控制类
	public Animator theAnimator;
	public Acter theAim;//目标
	public Acter theThis;//自己

	//获得必要的数据
	public void makeState(NavMeshAgent theMoveControllerIn,  Animator theAnimatorIn, Acter thisIn , Acter AimIn)
	{
		theMoveController = theMoveControllerIn;
		theAnimator = theAnimatorIn;
		theAim = AimIn;
		theThis = thisIn;
	}

	//重新设置范围属性
	public void setArea(float attackArea , float searchArea)
	{
		this.theSearchLength = searchArea;
		this.theAttackLength = attackArea;
	}
	//这个状态的开始阶段应该做什么
	public virtual void OnFSMStateStart(){}
	//这个状态的结束阶段应该做什么
	public virtual void OnFSMStateEnd(){}
	//在这个状态下需要做什么
	public virtual void  actInThisState(){}
	//思考转换到下一个状态
	//返回下一个状态，这个状态可以是自身
	public virtual FSMBasic moveToNextState(){return this;}
	//状态转换的时候会发生一次的事情
	public virtual void  OnChangeToThisState(){}
}
