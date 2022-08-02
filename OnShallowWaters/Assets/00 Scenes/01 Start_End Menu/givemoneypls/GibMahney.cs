using UnityEngine;
using UnityEngine.SceneManagement;

public class GibMahney : MonoBehaviour
{
    [SerializeField] private Transform youPressedTheWrongButton;
    [SerializeField] private Transform popup1;
    [SerializeField] private Transform popup2;
    [SerializeField] private Transform bai;
    [SerializeField] private Transform mainPage;
    
    
    public void HellYe()
    {
        popup2.gameObject.SetActive(true);
        popup1.gameObject.SetActive(false);
        youPressedTheWrongButton.gameObject.SetActive(false);
    }

    public void INeedMahneyMyself()
    {
        youPressedTheWrongButton.gameObject.SetActive(true);
    }

    public void Bai()
    {
        mainPage.gameObject.SetActive(true);
        bai.gameObject.SetActive(false);
    }

    public void ExtendFreeTrial()
    {
        SceneManager.LoadSceneAsync( (int) SceneData.MainMenu );
    }
}
