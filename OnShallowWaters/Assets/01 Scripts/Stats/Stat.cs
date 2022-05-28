using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    private List<int> _modifiers = new List<int>();
    
    public int BaseValue 
    { 
        get => baseValue; 
    }

    public int CurrentValue
    {
        get
        {
            int currentValue = baseValue;
            
            foreach (int modifier in _modifiers)
            {
                currentValue += modifier;
            }

            return currentValue;
        }
    }

    // public Stat()
    // {
    //     this.baseValue = 0;
    // }
    //
    
    public Stat(int baseValue)
    {
        this.baseValue = baseValue;
    }

    //Main functions to modify stats
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            _modifiers.Add(modifier);
        }
    }
}
