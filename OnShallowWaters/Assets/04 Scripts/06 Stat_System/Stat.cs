using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue = 1;
    
    //[HideInInspector]
    [SerializeField] private List<int> modifiers =  new List<int>();

    [Space][Space]
    [Header("Caps:")]
    [SerializeField] private bool hasCap;
    [SerializeField] [Range(1f, 20f)] private float cap = 1;

    public int PrevModifierByBoon { get; set; }

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
                currentValue += modifiers[i];
            }   
            
            return currentValue;
        }
    }
    
    public Stat(int baseValue)
    {
        this.baseValue = baseValue;
    }

    //Main functions to modify stats
    public void AddModifier(int modifier)
    {
        bool isOverCap = (this.CurrentValue / (float) this.baseValue) > cap;
        
        if (hasCap && isOverCap)
        {
            Debug.LogError("exceed cap");
            return;
        }
        
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
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
