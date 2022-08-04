using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_Unsync : MonoBehaviour {

	public enum Movement {JUMPING_FullOnly, SCROLLING_FullOnly, STATIC};
	public Movement movement = Movement.STATIC;
	public float speed = 1;
	private float position = 0;
	private Material material;

    [SerializeField]
	AnimationCurve unsyncAnimation;
	float animationTimer;

	public IEnumerator Unsync()
    {
		while(animationTimer < unsyncAnimation.keys[unsyncAnimation.keys.Length - 1].time)
        {
			speed = unsyncAnimation.Evaluate(animationTimer);
			animationTimer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
        }

		animationTimer = 0;
		speed = unsyncAnimation.Evaluate(animationTimer);
		yield return null;
    }


	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/VUnsync") );
	}



	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		position = speed * 0.1f;

		material.SetFloat("_ValueX", position);
		Graphics.Blit (source, destination, material);
	}
}
