using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    [Header("Volume Sliders")] 
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    private void OnDestroy()
    {
        SoundManager sm = SoundManager.Instance;
        
        bgmSlider.onValueChanged.RemoveListener(sm.ChangeMusicVol);
        sfxSlider.onValueChanged.RemoveListener(sm.ChangeEffectVol);
    }

    private void Start()
    {
        SetVolumeSliderValues();
        
        SoundManager sm = SoundManager.Instance;
        bgmSlider.onValueChanged.AddListener(sm.ChangeMusicVol);
        sfxSlider.onValueChanged.AddListener(sm.ChangeEffectVol);
    }
    
    private void SetVolumeSliderValues()
    {
        SoundManager sm = SoundManager.Instance;

        bgmSlider.value = sm.MusicVolume;
        sfxSlider.value = sm.EffectsVolume;
    }
}
