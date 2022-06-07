using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector2 currentPos;
    private Vector2 endTouchPos;
    private bool stopTouch;

    public float swipeRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Swipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }
    }
}
