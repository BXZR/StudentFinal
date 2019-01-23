using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasic : MonoBehaviour {

	//所有UICanvas的基类
	public virtual void BeforeShow(){}//一些必要的准备工作可以在这里做
	public virtual void OnShow(string value = ""){}//开始的时候增加的特效
	public virtual void OnEndShow(){}//结束的时候增加的特效
}
