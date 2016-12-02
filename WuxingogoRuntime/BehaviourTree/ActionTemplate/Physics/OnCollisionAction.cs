//
//  OnCollisionAction1.cs
// 
//
//  Created by Wuxingogo on 11/3/2016.
//
//
using UnityEngine;
using System.Collections;
using wuxingogo.btFsm;

[ActionTitle("Physics/OnCollisionAction")]
public class OnCollisionAction : BTAction
{
	public LayerMask colliderMask;

	public override void OnCollisionEnter(Collision collision)
	{
		XLogger.Log ("OnCollisionEnter", collision.collider);
	}
	public override void OnCollisionExit (Collision collision)
	{
		XLogger.Log ("OnCollisionExit", collision.collider);
	}
	public override void OnCollisionStay (Collision collision)
	{
		XLogger.Log ("OnCollisionStay", collision.collider);
	}
	public override void OnTriggerEnter(Collider collider)
	{
		XLogger.Log ("OnTriggerEnter", collider);
	}
	public override void OnTriggerExit (Collider collider)
	{
		XLogger.Log ("OnTriggerExit", collider);
	}
	public override void OnTriggerStay (Collider collider)
	{
		XLogger.Log ("OnTriggerStay", collider);
	}
}
