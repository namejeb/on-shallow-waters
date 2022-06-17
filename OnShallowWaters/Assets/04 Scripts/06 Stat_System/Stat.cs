using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue = 1;
    private List<int> _modifiers = new List<int>();

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
            
            for (int i = 0; i < _modifiers.Count; i++)
            {
                currentValue += _modifiers[i];
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
        
        _modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        _modifiers.Remove(modifier);
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
