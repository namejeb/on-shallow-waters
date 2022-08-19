using System.Collections;
using UnityEngine;


public class VFXPickups : MonoBehaviour
{
    public enum PickupType { Gold, Soul, Health }
    
    private Vector3 _offset = new Vector3(0f, 1f, 1f);
    public SoundData pickUpSFX;

    private EnemyPooler _enemyPooler;

    /* -- UI to update -- */ 
    
    //currency
    private UpdateCurrencies _updateCurrencies;
   
    // health
    private PlayerHealthBar _playerHealthBar;
    private PlayerStats _playerStats;
    
    private void OnDestroy()
    {
        DashNAttack.OnSpawnPickup -= Spawn;
    }

    private void Start()
    {
        _enemyPooler = EnemyPooler.Instance;
        _updateCurrencies = UpdateCurrencies.Instance;
        
        _playerHealthBar = GetComponent<PlayerHealthBar>();
        _playerStats = GetComponent<PlayerStats>();

        DashNAttack.OnSpawnPickup += Spawn;
    }
    
    public void Spawn(Transform hitTransform, PickupType pickupType)
    {
        Transform vfxTransform = null;
        vfxTransform = _enemyPooler.GetFromPool(pickupType);

        vfxTransform.gameObject.SetActive(true);
        vfxTransform.position = hitTransform.position;
        Animate(vfxTransform, hitTransform, pickupType);
    }

    private void Animate(Transform vfxTransform, Transform hitTransform, PickupType pickupType)
    {
        float duration = .5f;
        
        float newY = hitTransform.position.y + 5f;
        LeanTween.moveY(vfxTransform.gameObject, newY, duration);
        StartCoroutine(MoveToPlayer(vfxTransform, duration, pickupType));
    }

    
    private IEnumerator MoveToPlayer(Transform vfxTransform, float delay, PickupType pickupType)
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
        SoundManager.instance.PlaySFX(pickUpSFX, "PickUpGoldSFX");
        HandleUpdateUI( pickupType );
    }

    private void HandleUpdateUI(PickupType pickupType)
    {
        switch (pickupType)
        {
            case PickupType.Health:
                _playerHealthBar.SetHealth( _playerStats.Currhp, true );
                break;
            case PickupType.Soul:
                 _updateCurrencies.UpdateUI();
                 break;
        }
    }

    private bool IsReachedPlayer(Transform vfxTransform, Vector3 playerPos)
    {
        if (Vector3.Distance(vfxTransform.position, playerPos + _offset) > .1f)
            return false;
        
        return true;
    }
}
