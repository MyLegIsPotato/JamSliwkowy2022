using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class S_Util
{
	//public class S_Util_MB : MonoBehaviour { }
	//private static S_Util_MB utilMB;

	////Now Initialize the variable (instance)
	//private static void Init()
	//{
	//	//If the instance not exit the first time we call the static class
	//	if (utilMB == null)
	//	{
	//		//Create an empty object called MyStatic
	//		GameObject gameObject = new GameObject("MyStatic");


	//		//Add this script to the object
	//		utilMB = gameObject.AddComponent<S_Util_MB>();
	//	}
	//}

	//public static void AnimateFromCurve(AnimationCurve curve, float toAnimate)
 //   {
	//	Init();

	//	utilMB.StartCoroutine(Animate(curve, toAnimate));
 //   }

	//public static IEnumerator Animate(AnimationCurve curve, float toAnimate)
	//{
	//	float animationTimer = 0;

	//	while (animationTimer < curve.keys[curve.keys.Length - 1].time)
	//	{
	//		toAnimate = curve.Evaluate(animationTimer);
	//		animationTimer += Time.deltaTime;
	//		yield return new WaitForEndOfFrame();
	//	}

	//	animationTimer = 0;
	//	toAnimate = curve.Evaluate(animationTimer);
	//	yield return null;
	//}
}
