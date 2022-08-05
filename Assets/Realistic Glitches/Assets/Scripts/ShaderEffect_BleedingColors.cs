using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors : MonoBehaviour {

	public float intensity = 3;
	public float shift = 0.5f;
	private Material material;



	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/BleedingColors") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Intensity", intensity);
		material.SetFloat("_ValueX", shift);
		Graphics.Blit (source, destination, material);
	}


	float animationTime;
	[SerializeField]
	AnimationCurve aCurve;

	public void GlitchScreen()
	{
		StartCoroutine(GlitchScreenAnim());
	}

	IEnumerator GlitchScreenAnim()
	{
		while (animationTime < aCurve.keys[aCurve.keys.Length - 1].time)
		{
			shift = aCurve.Evaluate(animationTime);
			animationTime += Time.deltaTime;
			yield return null;
		}

		yield return null;
	}
}
