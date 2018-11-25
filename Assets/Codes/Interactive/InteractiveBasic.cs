using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBasic : MonoBehaviour {

	//交互类的基类
	//有一个交互键，按下之后会有一个范围检查
	//范围检查之后，距离主人公最近的哪一个交互物体，需要做出反应

	//交互操作
	public virtual void MakeInteractive(){}
}
