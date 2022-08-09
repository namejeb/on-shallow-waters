using System.Collections;
using UnityEngine;


public class VFXCurrency : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0f, 1f, 1f);

    private enum Type { Gold, Soul }
    private EnemyPooler _enemyPooler;

    private UpdateCurrencies _updateCurrencies;

    private void OnDestroy()
    {
        DashNAttack.OnSpawnCurrency -= Spawn;
    }

    private void Start()
    {
        _enemyPooler = EnemyPooler.Instance;
        _updateCurrencies = UpdateCurrencies.Instance;

        DashNAttack.OnSpawnCurrency += Spawn;
    }
    
    public void Spawn(Transform hitTransform, CurrencyType currencyType)
    {
        Transform vfxTransform = null;
        if (currencyType == CurrencyType.GOLD)
        {
            vfxTransform = _enemyPooler.GetFromPool(VFXCurrencyType.Gold);
        }
        else
        {
            vfxTransform = _enemyPooler.GetFromPool(VFXCurrencyType.Soul);
        }
        vfxTransform.gameObject.SetActive(true);
        vfxTransform.position = hitTransform.position;
        Animate(vfxTransform, hitTransform);
    }

    private void Animate(Transform vfxTransform, Transform hitTransform)
    {
        float duration = .5f;
        
        float newY = hitTransform.position.y + 5f;
        LeanTween.moveY(vfxTransform.gameObject, newY, duration);
        StartCoroutine(MoveToPlayer(vfxTransform, duration));
    }

    
    private IEnumerator MoveToPlayer(Transform vfxTransform, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Transform playerTransform = transform;
        Vector3 playerPos = playerTransform.position + _offset;
        
        while (!IsReachedPlayer(vfxTransform, playerPos))
        {
            playerPos = transform.position;
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
