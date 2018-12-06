using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一些可能用到的事件
public delegate void HpChangeHook(float adder);
public delegate void DamageChangeHook(float adder);
public delegate void LearningChangeHook(float adder);
public delegate void KillHook(Acter beKilled);
public delegate void InteractiveHook(InteractiveBasic aim);


public class Acter : MonoBehaviour {

	//角色的基类
	//其实所有的运算都应该是根以基类作为基础的
	//游戏人物的数值
	public string playerName = "";//角色名字
	public float hpNow = 100f;//当前生命值
	public float hpMaxNow = 100f;//当前生命上限
	public float attackDamage = 4f;//当前攻击力
	public int lvNow = 1;//当前等级
	public bool isAlive = true;//当前是否生存



	//动画控制单元
	public Animator theAnimationController;
	//移动控制单元
	public move theMoveController;
	//头顶血条
	public PlayerBloodCanvas theBloodSlider;

	//公有操作生命的事件
	public event HpChangeHook HpChanger;
	//公有操作战斗力的事件
	public event DamageChangeHook DamageChanger;
	//公有操作击杀的事件
	public event KillHook KillEvent;
	//公有的交互的事件
	public event InteractiveHook InteractiveEvent;


	/// <summary>
	/// 攻击的时候触发
	/// 触发方式可以是动画事件也可能是投掷武器
	/// </summary>
	/// <param name="aim">Aim.</param>
	public virtual void OnAttack(Acter aim)
	{
		if (!aim.isAlive)
			return;

		aim.OnHpChange (-this.attackDamage);
		if (aim.hpNow == 0) 
			OnKill (aim);
	}

	/// <summary>
	/// 外部操作生命改变的方法，这个方法全球唯一
	/// 所有新操作生命的方法都由内部事件处理.
	/// </summary>
	public  void OnHpChange(float hpChangeAdder)
	{
		HpChanger (hpChangeAdder);

		if (hpChangeAdder >= 0)
			return;

		if (hpNow == 0)
			OnDead ();
		else 
		{
			if(Random.value < 0.1f)
			   theAnimationController.Play ("getHit");
		}
	}

	/// <summary>
	/// 如果玩家挂了，自动调用这个方法后续处理
	/// </summary>
	public virtual void OnDead()
	{
		isAlive = false;
		theAnimationController.Play ("dead");
		UIController.GetInstance ().ShowUI<messageBox> ("击杀！");
		Destroy (this.gameObject, 2f);

	}


	/// <summary>
	///击杀方法包装
	/// </summary>
	/// <param name="aim">Aim.</param>
	public void OnKill(Acter aim)
	{
		if(KillEvent!= null)
		    KillEvent (aim);
	}

	public void OnInteractive(InteractiveBasic aim)
	{
		if (InteractiveEvent != null)
			InteractiveEvent (aim);
	}


	/// <summary>
	/// 外部操作战斗力的方法，这个方法全球统一
	/// 所有新操作战斗力的方法都由内部事件处理.
	/// </summary>
	public  void  OnDamageChange(float damageChangeAdder)
	{
		DamageChanger (damageChangeAdder);
	}


	/// <summary>
	/// 刷新数值，更改显示
	/// 因为都是事件驱动的，没有一直查询，只好手动更新了
	/// </summary>
	public virtual void MakeFlash()
	{
		OnDamageChange (0f);
		OnHpChange (0f);
	}


	/// <summary>
	///额外方法,创建头顶血条
	/// </summary>
	public void MakeHpSlider()
	{
		GameObject theSlider = GameObject.Instantiate( SystemValues.LoadResources<GameObject>("UI/PlayerBloodCanvas"));

		theSlider.transform.position = this.transform.position + new Vector3 (0f,1.5f,0f);
		theSlider.transform.localScale = new Vector3 (0.01f,0.01f,0.01f);
		theSlider.transform.SetParent (this.transform);

		PlayerBloodCanvas theShowCanvas = theSlider.GetComponent<PlayerBloodCanvas> ();
		theShowCanvas.MakeFkash (this);
		this.theBloodSlider = theShowCanvas;
	}
}
