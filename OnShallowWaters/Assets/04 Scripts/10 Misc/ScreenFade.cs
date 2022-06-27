using System.Collections;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    private RectTransform _image;

    private void OnDestroy()
    {
        LeanTween.reset();
        
        RoomSpawnerV2.OnRoomChangeStart -= FadeIn;
        RoomSpawnerV2.OnRoomChangeFinish -= FadeOut;
    }

    private void Awake()
    {
        _image = GetComponent<RectTransform>();
    }

    private void Start()
    {
        RoomSpawnerV2.OnRoomChangeStart += FadeIn;
        RoomSpawnerV2.OnRoomChangeFinish += FadeOut;
    }
    
    private void FadeIn()
    {
        LeanTween.alpha(_image, 1f, .5f).setIgnoreTimeScale(true);
    }

    private void FadeOut()
    {
        StartCoroutine(CFadeOut());
    }

    private IEnumerator CFadeOut()
    {
        yield return new WaitForSeconds(.5f);
        LeanTween.alpha(_image, 0f, .5f).setIgnoreTimeScale(true);
    }
}
