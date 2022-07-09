using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue = 1;
    
    //[HideInInspector]
    [SerializeField] private List<float> modifiers =  new List<float>();

    [Space][Space]
    [Header("Caps:")]
    [SerializeField] private bool hasCap;
    [SerializeField] [Range(1f, 20f)] private float cap = 1;
    
    public int BaseValue 
    { 
        get => baseValue; 
    }

    public int CurrentValue
    {
        get
        {
            int currentValue = baseValue;
            
            for (int i = 0; i < modifiers.Count; i++)
            {
                currentValue += (int) modifiers[i];
            }   
            
            return currentValue;
        }
    }
    
    public Stat(int baseValue)
    {
        this.baseValue = baseValue;
    }

    //Main functions to modify stats
    public void AddModifier(float modifier)
    {
        bool isOverCap = (this.CurrentValue / (float) this.baseValue) > cap;
        
        if (hasCap && isOverCap)
        {
            Debug.LogError("exceed cap");
            return;
        }
        
        modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier)
    {
        modifiers.Remove(modifier);
    }

    public void ModifyBaseValue(int amount)
    {
        bool isOverCap = (this.baseValue + amount / baseValue) > cap;

        if (hasCap && isOverCap)
        {
            Debug.LogError("base cannot be over cap");
            return;
        }
        
        this.baseValue = amount;
    }
}
