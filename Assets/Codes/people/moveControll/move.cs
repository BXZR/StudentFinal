using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class move : MonoBehaviour {


	public GameObject EffectForDestination;//导航目标标记，可以为空
	public float moveSpeed = 0.5f;//移动速度
	[Range(0f,1f)]
	public float rotateSpeed = 0.3f;//转身速率，用于插值
	//是否正在移动标记
	public bool isMoving = false;
	public GameObject flyTrans;//飞行媒介
	public moveModeBasic theMoveModeNow;//当前的移动模式
	public CharacterController theMoveController;//移动控制单元
	private Vector2 moveAxisValue;//输入轴数据保存
	private Quaternion headingAim  = Quaternion.identity;//转向的目标，用于插值
	private animatorController theAnimatorController;//动画控制单元
	private Vector3 AxisForMove = Vector3.zero;//移动控制单元的轴向


	//额外功能
	private NavMeshAgent TheAgent;//导航代理

	/// <summary>
	/// 自动寻路的目标检查
	/// 这个是用于鼠标点击的时候和任务自动寻路的时候使用的
	/// </summary>
	public void FindDestination()
	{
		if (!TheAgent)
			return;

		//print ("开始寻路");
		Ray mRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit mHi;
		if (Physics.Raycast (mRay, out mHi))
		{
			if (mHi.collider .tag == "earth")
			{
				Vector3 destenation = new Vector3 (mHi .point .x, mHi .point .y, mHi .point .z);
				TheAgent.SetDestination (destenation);

				if(TheAgent.hasPath)
				{
					if (EffectForDestination) 
					{
						GameObject ef = (GameObject)Instantiate (EffectForDestination);//创建预设
						ef.transform.position = destenation;
					}
				}
			}
		}
	}



	/// <summary>
	/// 使用轴进行移动的时候的操纵方法
	/// 在分析输入并给出方向和xy的计算结果
	/// </summary>
	public void InputOperateWithAxis(Vector2 theAxis)
	{
		float xAxisValue = theAxis.x;
		float yAxisValue = theAxis.y;

		if (Mathf.Abs (xAxisValue) < 0.3f && Mathf.Abs (yAxisValue) < 0.3f) 
		{
			if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				return;
		}
		
		//用cos来算比较保靠，如果用tan就可能会出现0的情况
		//例如x = 0 ， y = 1
		float allAxisAdd = xAxisValue * xAxisValue + yAxisValue * yAxisValue;
		float Yadd = Mathf.Asin( xAxisValue / Mathf.Sqrt( allAxisAdd) ) *Mathf.Rad2Deg;
		Yadd = yAxisValue > 0 ? Yadd : 180-Yadd;

		Vector3 eulerOld = Camera.main.transform.rotation.eulerAngles;
		Vector3 eulerNew = Vector3.zero;
		eulerNew = new Vector3 (0f , Yadd + eulerOld.y, 0f);
		headingAim = Quaternion.Euler (eulerNew );

		AxisForMove.x = xAxisValue;
		AxisForMove.z = yAxisValue;
	}

	/// <summary>
	/// 真正实现移动的方法.
	/// 转向的实现也在这里
	/// 这个方法需要持续计算
	/// </summary>
	/// <param name="theValue">The value.</param>
	private void TrueMove()
	{
		//重力控制---------------------------------------------------------------------------------------------------
		theMoveModeNow.OnGravity(theMoveController);
		//控制当前的转向---------------------------------------------------------------------------------------------
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation , headingAim , rotateSpeed);
		//真正的移动过程---------------------------------------------------------------------------------------------
		//这是用于真正移动的方法，单独放在这里是因为很多时候是不可以移动的
		float headingMinus = Mathf.Abs (headingAim.eulerAngles.y - this.transform.rotation.eulerAngles.y);
		if ( headingMinus < 15f && isMoving && theMoveModeNow!=null)
			theMoveModeNow.OnMove (theMoveController , moveSpeed );
		//最后补充额外计算过程---------------------------------------------------------------------------------------
		theMoveModeNow.ExtraUpdate(this);
	}
		
	/// <summary>
	/// 动画控制.
	/// 使用输入轴的数据与Animator状态机的轴数据做对应
	/// </summary>
	private void AnimationControl()
	{
		theMoveModeNow.OnPlayAnimation(theAnimatorController, AxisForMove.x ,AxisForMove.z);
	}

	//开始移动
	public void startMoving()
	{
		AxisForMove = Vector3.zero;
		isMoving = true;
	}
    //强制停止移动 
	public void stopMoving()
	{
		AxisForMove = Vector3.zero;
		isMoving = false;
	}

	/// <summary>
	/// 切换到指定的运动状态
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void changeMoveMode(moveModeBasic newMode)
	{
		theMoveModeNow.OnEndMove (this);
		theMoveModeNow = newMode;
		theMoveModeNow.OnStartMove (this);
	}


	void Start () 
	{
		TheAgent = this.GetComponent<NavMeshAgent> ();
		headingAim = this.transform.rotation;
		theAnimatorController = this.GetComponent<animatorController> ();
		theMoveController = this.GetComponent<CharacterController> ();
		theMoveModeNow = new runMoveMode ();
	}

	void Update () 
	{
		TrueMove ();
		AnimationControl ();
	}
}

