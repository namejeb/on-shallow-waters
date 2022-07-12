using UnityEngine;



public class ShopTriggerCollider : MonoBehaviour
{
    public GameObject Player; 
    [SerializeField] private Shop Shop;
    private void OnTriggerEnter(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (other.gameObject.CompareTag("Player"))
        {
            Shop.Show(shopCustomer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (other.gameObject.CompareTag("Player"))
        {
            Shop.Hide();
        }
    }
}
