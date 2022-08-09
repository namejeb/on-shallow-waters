using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Transform damageTextPrefab;
    
    [Space][Space]
    [Header("Settings:")]
    [SerializeField] private Vector3 offsets = new Vector3(0, 2, 0);

    [SerializeField] private Color critColor = Color.yellow;
    [SerializeField] private Color nonCritColor = Color.white;
    
    
    private Vector3 _scaleNonCrit;
    private Vector3 _scaleCrit;


    private List<Transform> _damageTextPool = new List<Transform>();
    private Transform _lastSpawnedText;
    
    private Camera _cam;
    private Quaternion _camRot;

    private float _popupDuration = .6f;

    private void OnDestroy()
    {
        DashNAttack.OnHitLanded -= SpawnText;
    }

    private void Start()
    {
        DashNAttack.OnHitLanded += SpawnText;
        
        _cam = Camera.main;
        _damageTextPool.Clear();
        
        InitTexts();
    }

    private void SpawnText(Transform hitTransform, float damageDealt, bool isCrit)
    {
        if (_damageTextPool[0] == null)
        {
            InitTexts();
        }
        
        Transform textToSpawn = _damageTextPool[0];
        textToSpawn.gameObject.SetActive(true);
        
        HandleIsCrit(textToSpawn, damageDealt, isCrit);
        textToSpawn.position = hitTransform.position + offsets;
        
        _damageTextPool.RemoveAt(0);
        _lastSpawnedText = textToSpawn;
        Animate();  

        StartCoroutine(DisableText(textToSpawn, _popupDuration));
    }

    private void InitTexts()    
    {
        for (int i = 0; i < poolSize; i++)
        {
            Transform text = Instantiate(damageTextPrefab, Vector3.zero, Quaternion.identity, transform);
            _camRot = _cam.transform.rotation;
            text.LookAt(text.position + _camRot * Vector3.forward, _camRot * Vector3.up);
        
            _damageTextPool.Add(text);
            text.gameObject.SetActive(false);
        }
        
        // save scale for use when resetting
        _scaleNonCrit = _damageTextPool[0].localScale;
        _scaleCrit = _scaleNonCrit + new Vector3(.08f, .08f, .08f);
    }



    private void Animate()
    {
        Vector3 currPos = _lastSpawnedText.position;
        LeanTween.move(_lastSpawnedText.gameObject, new Vector3(currPos.x, currPos.y + 1.5f, currPos.z), _popupDuration);
    }

    private void HandleIsCrit(Transform dmgTextTransform, float damageDealt, bool isCrit)
    {
        TextMeshPro dmgText = dmgTextTransform.GetComponent<TextMeshPro>();
        dmgText.text = $"{damageDealt}";
        
        if (isCrit)
        {
            dmgText.color = critColor;
            dmgTextTransform.localScale = _scaleCrit;
        }
        else
        {
            dmgText.color = nonCritColor;
            dmgTextTransform.localScale = _scaleNonCrit;
        }
    }
    
    
    private IEnumerator DisableText(Transform textToDisable, float duration)
    {
        yield return new WaitForSeconds(duration);
        
        _damageTextPool.Add(textToDisable);
        textToDisable.gameObject.SetActive(false);
    }
}
