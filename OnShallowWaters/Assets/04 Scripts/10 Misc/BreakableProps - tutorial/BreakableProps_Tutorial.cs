using UnityEngine;
using System;

public class BreakableProps_Tutorial : MonoBehaviour
{
    private int _propsToBreak;
    public static event Action OnAllPropsBroken ;
    
    
    private void Start()
    {
        _propsToBreak = transform.childCount;
    }

    public void UpdatePropsToBreak()
    {
        _propsToBreak--;
        if (_propsToBreak <= 0)
        {
            if(OnAllPropsBroken != null) OnAllPropsBroken.Invoke();
        }
    }
}
