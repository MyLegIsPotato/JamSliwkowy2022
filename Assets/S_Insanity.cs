using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[ExecuteInEditMode]
public class S_Insanity : MonoBehaviour
{
    [SerializeField]
    PostProcessingProfile mainCameraPostProcessingProfile;
    [SerializeField]
    ShaderEffect_BleedingColors bleedingColors;

    public AnimationCurve gotHitCurve;

    [SerializeField]
    HingeJoint headJoint;

    //Insanity - just vignette:

    [Range(0f, 1f)]
    public float vignettePercent;

    public float VignettePercent
    {
        get { return vignettePercent; }
        set
        {

            if (value != InsanityPercent)
            {
                vignettePercent = value;
            }

            VignetteModel.Settings newVignette = mainCameraPostProcessingProfile.vignette.settings;
            newVignette.intensity = Mathf.Lerp(0.1f, 0.5f, vignettePercent);
            mainCameraPostProcessingProfile.vignette.settings = newVignette;
        }
    }


    //Insanity lvl 1:

    [Range(0f, 1f)]
    public float insanityPercent;

    public float InsanityPercent
    {
        get { return insanityPercent; }
        set {

            if(value != InsanityPercent)
            {
                insanityPercent = value;
            }
              

            AmbientOcclusionModel.Settings newAO = mainCameraPostProcessingProfile.ambientOcclusion.settings;
            newAO.intensity = Mathf.Lerp(1.29f, 2f, insanityPercent);
            newAO.radius = Mathf.Lerp(0.1f, 0.3f, insanityPercent);
            mainCameraPostProcessingProfile.ambientOcclusion.settings = newAO;

            BloomModel.Settings newBloom = mainCameraPostProcessingProfile.bloom.settings;
            newBloom.bloom.intensity = Mathf.Lerp(0.14f, 9f, insanityPercent);
            mainCameraPostProcessingProfile.bloom.settings = newBloom;

            ChromaticAberrationModel.Settings newCA = mainCameraPostProcessingProfile.chromaticAberration.settings;
            newCA.intensity = Mathf.Lerp(0.037f, 0.75f, insanityPercent);
            mainCameraPostProcessingProfile.chromaticAberration.settings = newCA;

            GrainModel.Settings newGrain = mainCameraPostProcessingProfile.grain.settings;
            newGrain.intensity = Mathf.Lerp(0.1f, 0.80f, insanityPercent);
            mainCameraPostProcessingProfile.grain.settings = newGrain;

            VignetteModel.Settings newVignette = mainCameraPostProcessingProfile.vignette.settings;
            newVignette.intensity = Mathf.Lerp(0.1f, 0.5f, insanityPercent);
            mainCameraPostProcessingProfile.vignette.settings = newVignette;

            if(insanityPercent > 0.7f)
            {
                bleedingColors.intensity = Mathf.Lerp(0f, 0.8f, insanityPercent);
                bleedingColors.shift = Mathf.Lerp(2f, 5f, insanityPercent);
            }
            else
            {
                bleedingColors.intensity = 0;
            }
        }
    }

    //Insanity lvl 2:

    [Range(0f, 1f)]
    public float insanityPercentLvl2;

    public float InsanityPecentLvl2
    {
        get { return insanityPercentLvl2; }
        set
        {

            if (value != InsanityPecentLvl2)
            {
                insanityPercentLvl2 = value;
            }


            AmbientOcclusionModel.Settings newAO = mainCameraPostProcessingProfile.ambientOcclusion.settings;
            newAO.intensity = Mathf.Lerp(2f, 5f, insanityPercentLvl2);
            newAO.radius = Mathf.Lerp(0.3f, 1f, insanityPercentLvl2);
            mainCameraPostProcessingProfile.ambientOcclusion.settings = newAO;

            BloomModel.Settings newBloom = mainCameraPostProcessingProfile.bloom.settings;
            newBloom.bloom.intensity = Mathf.Lerp(9f, 15f, insanityPercentLvl2);
            mainCameraPostProcessingProfile.bloom.settings = newBloom;

            ChromaticAberrationModel.Settings newCA = mainCameraPostProcessingProfile.chromaticAberration.settings;
            newCA.intensity = Mathf.Lerp(0.75f, 2f, insanityPercentLvl2);
            mainCameraPostProcessingProfile.chromaticAberration.settings = newCA;

            GrainModel.Settings newGrain = mainCameraPostProcessingProfile.grain.settings;
            newGrain.intensity = Mathf.Lerp(0.5f, 2f, insanityPercentLvl2);
            mainCameraPostProcessingProfile.grain.settings = newGrain;

            VignetteModel.Settings newVignette = mainCameraPostProcessingProfile.vignette.settings;
            newVignette.intensity = Mathf.Lerp(0.5f, 2f, insanityPercentLvl2);
            mainCameraPostProcessingProfile.vignette.settings = newVignette;

            if (insanityPercentLvl2 > 0.7f)
            {
                bleedingColors.intensity = Mathf.Lerp(0.6f, 1f, insanityPercentLvl2);
                bleedingColors.shift = Mathf.Lerp(3f, 10f, insanityPercentLvl2);
            }
            else
            {
                bleedingColors.intensity = 0;
            }
        }
    }

    private void Update()
    {
        if(insanityPercentLvl2 > 0)
        {
            InsanityPecentLvl2 = insanityPercentLvl2;
        }
        else if(insanityPercent > 0)
        {
            InsanityPercent = insanityPercent;

        }
        else
        {
            vignettePercent = Mathf.InverseLerp(-30, +30, Mathf.Abs(headJoint.angle));
            VignettePercent = vignettePercent;
        }
    }
}
