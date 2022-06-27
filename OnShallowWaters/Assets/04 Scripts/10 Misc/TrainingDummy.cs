using System;
using _04_Scripts._05_Enemies_Bosses;
using Cinemachine;
using UnityEngine;

public class TrainingDummy : MonoBehaviour, IDamageable
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public void Damage(int damageAmount)
    {
        _anim.SetTrigger("HitTrigger");
    }

    public float LostHP()
    {
        throw new System.NotImplementedException();
    }
}
