using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMove : MonoBehaviour 
{
	public bool MoveX = false;
	public bool ToUp = false;
	public float XSpeed = 0.1f;
	private float offsetX = 0;

	public bool MoveY = false;
	public bool ToLeft = false;
	public float YSpeed = 0.1f;
	private float offsetY = 0;


	void FixedUpdate()
	{
		if (MoveX) 
		{
			offsetX = Time.time * XSpeed;
			if (ToUp)
				offsetX *= -1;
		}

		if (MoveY) 
		{
			offsetY = Time.time * YSpeed;
			if (ToLeft)
				offsetY *= -1;
		}
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (offsetX,offsetY);
	}
}
