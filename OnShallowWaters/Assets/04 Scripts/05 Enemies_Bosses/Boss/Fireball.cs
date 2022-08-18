using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float height = 5f;
    [SerializeField] private float time;
    [SerializeField] private float xDistance, zDistance;
    [SerializeField] private Vector3 randPos;
    private float lerper;
    private bool canLerp;
    private EnemyPooler pooler;

    private void Awake()
    {
        pooler = FindObjectOfType<EnemyPooler>();
    }

    private void OnEnable()
    {
        randPos.x = Random.Range(transform.position.x - xDistance, transform.position.x + xDistance);
        randPos.z = Random.Range(transform.position.z - zDistance, transform.position.z + zDistance);
        canLerp = true;
        
        StartCoroutine(Disable());
    }

    private void Update()
    {
        if (canLerp)
        {
            lerper += Time.deltaTime;
            lerper = lerper % time;
            transform.position = MathParabola.Parabola(transform.position, randPos, height, lerper / time);
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(time);
        canLerp = false;
        Transform bom = pooler.GetFromPool(ProjectileType.Boss1Bom);
        bom.position = new Vector3(randPos.x, 0, randPos.z);
        bom.gameObject.SetActive(true);
        yield return new WaitForSeconds(time + 2);
        bom.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }


}
