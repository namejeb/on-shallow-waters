using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Singleton
    private static GameAssets _instance;

    public static GameAssets i
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }

            return _instance;
        }
    }
    #endregion

    [Header("Gold Shop Items:")]
    public Sprite gShop_Hp;
    public Sprite gShop_Atk;
    public Sprite gShop_Def;
}
