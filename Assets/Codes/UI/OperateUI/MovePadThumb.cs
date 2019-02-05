using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePadThumb : MonoBehaviour {


	private Image theShowImage;

	void Start()
	{
		theShowImage = this.GetComponent<Image> ();
		EndShow ();
	}

	void OnEnable()
	{
		EndShow ();
	}


	/// <summary>
	/// 使用轴进行移动的时候的自身旋转
	/// </summary>
	public void InputOperateWithAxis(Vector2 theAxis)
	{
		//计算当前的目标角度-----------------------------------------------------------------------------------------------------------------------
		float xAxisValue = theAxis.x;
		float yAxisValue = theAxis.y;
		if (Mathf.Abs (xAxisValue) < 0.1f && Mathf.Abs (yAxisValue) < 0.1f)
		{
			this.transform.rotation = Quaternion.identity;
			return;
		}

		float allAxisAdd = xAxisValue * xAxisValue + yAxisValue * yAxisValue;
		float ZRotate = Mathf.Asin( xAxisValue / Mathf.Sqrt( allAxisAdd) ) *Mathf.Rad2Deg;

		ZRotate = yAxisValue > 0 ? 360-ZRotate : ZRotate + 180;
		Vector3 rotate = new Vector3 (0f,0f,ZRotate);
		this.transform.rotation = Quaternion.Euler (rotate);
	}

	/// <summary>
	///显示箭头.
	/// </summary>
	public void StartShow()
	{
		theShowImage.enabled = true;
	}

	/// <summary>
	/// 停止显示箭头.
	/// </summary>
	public void EndShow()
	{
		if(theShowImage)
		    theShowImage.enabled = false;
	}

}
