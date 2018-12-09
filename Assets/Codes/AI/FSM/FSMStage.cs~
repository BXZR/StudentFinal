using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FSMStage :MonoBehaviour  {

	private NavMeshAgent theMoveController;//AI人物的移动控制类

	public FSMBasic theStateNow ;
	private Acter thethis; 
	private Animator theAnimator;
	public float AIThinkTimer = 0.20f;//AI每隔一段时间再进行思考
	//这是一个值得优化的点，在一些条件下关闭掉AI计算会非常省事
	//AI的计算很重并且同时计算的很多
	public bool theAiIsActing = true;//AI是否计算的标记
	//HPSlider头顶生命条只有在攻击和追杀的时候才显示
	private PlayerBloodCanvas theBloodCanvas;

	//初始化
	void Start ()
	{
		this.transform.root.tag = "AI";//打上标记方便找

		thethis = this.GetComponent <Acter> ();
		theMoveController = this.GetComponentInChildren<NavMeshAgent> ();
		theAnimator = this.GetComponentInChildren<Animator> ();
	
		theStateNow = new FSM_Search ();
		theStateNow.makeState (theMoveController , theAnimator ,thethis , null );
		theStateNow.OnFSMStateStart ();
		this.enabled = false;
	}
		
	//有些东西在这里总控制会比较方便
	//例如头顶生命条没有必要每一个AIState保留引用的
	void OnStateChange(FSMBasic oldState = null  , FSMBasic newState = null)
	{
		if (!thethis.theBloodSlider)
			thethis.MakeHpSlider ();
		if (newState.theThis && thethis.theBloodSlider)
		{
			if (newState is FSM_RunAfter || newState is FSM_Attack)
				thethis.theBloodSlider.gameObject.SetActive (true);
			else
				thethis.theBloodSlider.gameObject.SetActive (false);
		}
	}


	/// <summary>
	/// 强制显示生命条
	/// </summary>
	public void GotAim()
	{
		if (!thethis.theBloodSlider)
			thethis.MakeHpSlider ();
		if (this.theStateNow is FSM_Search  ) 
		{
			FSMBasic theStateNew = new FSM_RunAfter ();
			theStateNew.makeState(this.theMoveController,this.theAnimator, theStateNow .theThis, theStateNow.theAim);
			if (theStateNew != theStateNow)
				OnStateChange (theStateNow , theStateNew);

			theStateNow = theStateNew;
		}
	}

	//有关AI计算状态-----------------------------------------------------------

    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
		//出于优化考虑不必让AI一直计算下去
		//此外这也可是“怪物僵直”状态的一个做法
		if (theAiIsActing && thethis.isAlive && theStateNow != null)
		{
			theStateNow.actInThisState ();
			FSMBasic theStateNew = theStateNow.moveToNextState ();

			if (theStateNew != theStateNow)
				OnStateChange (theStateNow , theStateNew);

			theStateNow = theStateNew;
		}
	}
}
