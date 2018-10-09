using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class renderVR : MonoBehaviour {
	//Render scaling for performance vs quality. 
	//The higher render scale, the better the quality and worse the performance. 
	//The lower render scale, the worse the quality and better the performance.
	[SerializeField] private float m_RenderScale = 1f;

	void Start()
	{
		VRSettings.renderScale= m_RenderScale;
	}
}
