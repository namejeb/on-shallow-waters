using System.Collections;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    private RectTransform image;

    private void OnDestroy()
    {
        LeanTween.reset();
        
        RoomSpawner.OnRoomChangeStart -= FadeIn;
        RoomSpawner.OnRoomChangeFinish -= FadeOut;
    }

    private void Start()
    {
        image = GetComponent<RectTransform>();
        RoomSpawner.OnRoomChangeStart += FadeIn;
        RoomSpawner.OnRoomChangeFinish += FadeOut;
    }
    
    private void FadeIn()
    {
        LeanTween.alpha(image, 1f, .5f).setIgnoreTimeScale(true);
    }

    private void FadeOut()
    {
        StartCoroutine(cFadeOut());
        //LeanTween.alpha(image, 0f, .5f).setIgnoreTimeScale(true);
    }

    private IEnumerator cFadeOut()
    {
        yield return new WaitForSeconds(.5f);
        LeanTween.alpha(image, 0f, .5f).setIgnoreTimeScale(true);
    }
}
