using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Profiling;
using Cinemachine;

public class TimeManager : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float slowdownFactor = .02f;
    [SerializeField] private float backToNormalTimeLength = 1f;
    private bool activated;

    [Header("SKB 3")]
    [SerializeField] private Volume volume;
    [SerializeField] private GameObject ZAWARUDO;
    private LensDistortion lensDist;
    private Animator anim;
    private VolumeParameter<float> V = new VolumeParameter<float>();
    private CinemachineBrain cb;

    void Start()
    {
        cb = FindObjectOfType<CinemachineBrain>();
        anim = GetComponent<Animator>();
        volume.profile.TryGet(out lensDist);
        ResetTimeSettings();
    }

    public void StartSlowMo(float slowTime)
    {
        backToNormalTimeLength = slowTime;
        activated = true;
        
        SlowMoActivated();
        StartCoroutine(DeactivateSlowMo());
    }

    private void ResetTimeSettings()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;        
    }

    private void SlowMoActivated()
    {
        cb.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
        anim.SetTrigger("zawarudo");
        activated = true;
        Time.timeScale *= slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void SetIntensity(float value)
    {
        V.value = value;
        lensDist.intensity.SetValue(V);
    }

    private IEnumerator DeactivateSlowMo()
    {
        while (Time.timeScale < 1f)
        {
            //yield return new WaitForNextFrameUnit();
            V.value = Mathf.Clamp(Time.timeScale, 0f, 0.5f);
            lensDist.intensity.SetValue(V);
            Time.timeScale += (1f / backToNormalTimeLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            yield return null;
        }
        cb.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        Mathf.Clamp(V.value, 1f, 0f);
        lensDist.intensity.SetValue(V);
        activated = false;
        ResetTimeSettings();
        
        yield return 0;
    }
}
