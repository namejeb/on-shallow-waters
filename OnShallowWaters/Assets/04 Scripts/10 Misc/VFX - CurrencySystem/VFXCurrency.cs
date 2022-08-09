using System.Collections;
using UnityEngine;


public class VFXCurrency : MonoBehaviour
{
    [SerializeField] private Type currencyType;
    private Vector3 _offset = new Vector3(0f, 1f, 1f);

    private enum Type { Gold, Soul }
    private EnemyPooler _enemyPooler;

    private UpdateCurrencies _updateCurrencies;

    private void Start()
    {
        _enemyPooler = EnemyPooler.Instance;
        _updateCurrencies = UpdateCurrencies.Instance;
    }
    
    public void Spawn()
    {
        Transform vfxTransform = null;
        
        if (currencyType == Type.Gold)
        {
            vfxTransform = _enemyPooler.GetFromPool(VFXCurrencyType.Gold);
        }
        else
        {
            vfxTransform = _enemyPooler.GetFromPool(VFXCurrencyType.Soul);
        }
        vfxTransform.gameObject.SetActive(true);
        vfxTransform.position = transform.position;
        Animate(vfxTransform);
    }

    private void Animate(Transform vfxTransform)
    {
        float duration = .5f;
        
        float newY = transform.position.y + 5f;
        LeanTween.moveY(vfxTransform.gameObject, newY, duration);
        StartCoroutine(MoveToPlayer(vfxTransform, duration));
    }

    
    private IEnumerator MoveToPlayer(Transform vfxTransform, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Transform playerTransform = PlayerHandler.Instance.transform;
        Vector3 playerPos = playerTransform.position + _offset;
        
        while (!IsReachedPlayer(vfxTransform, playerPos))
        {
            playerPos = PlayerHandler.Instance.transform.position;
            vfxTransform.position = Vector3.MoveTowards(vfxTransform.position, playerPos + _offset, 8f * Time.deltaTime);
            yield return null;
        }
        vfxTransform.gameObject.SetActive(false);
        _updateCurrencies.UpdateUI();
    }

    private bool IsReachedPlayer(Transform vfxTransform, Vector3 playerPos)
    {
        if (Vector3.Distance(vfxTransform.position, playerPos + _offset) > .1f)
            return false;
        
        return true;
    }
}
