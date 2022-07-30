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

    [Range(0f, 1f)]
    public float insanityPercent;

    public float InsanityPecent
    {
        set {

            if(value != InsanityPecent)
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
        get { return insanityPercent; }
    }

    private void Update()
    {
        InsanityPecent = insanityPercent;
    }
}
