using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float slowdownFactor = .02f;
    [SerializeField] private float backToNormalTimeLength = 1f;
    private bool activated;

    void Start()
    {
        ResetTimeSettings();
    }

    void Update()
    {
        if (activated)
        {
            SlowMoActivated();
            StartCoroutine(DeactivateSlowMo());
        }
    }

    public void StartSlowMo(float slowTime)
    {
        backToNormalTimeLength = slowTime * 10;
        activated = true;
    }

    private void ResetTimeSettings()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;
    }

    private void SlowMoActivated()
    {
        activated = true;
        Time.timeScale *= slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    private IEnumerator DeactivateSlowMo()
    {
        while (Time.timeScale < 1f)
        {
            yield return new WaitForNextFrameUnit();

            Time.timeScale += (1f / backToNormalTimeLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        }
        activated = false;
        ResetTimeSettings();
    }
}
