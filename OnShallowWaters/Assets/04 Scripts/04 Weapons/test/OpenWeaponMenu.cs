using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenWeaponMenu : MonoBehaviour
{
    [SerializeField] private ChoosingWeapon[] choosingWeaponSo ;
    [SerializeField] private Transform menu;

    [Space] [Space]
    [Header("Texts: ")]
    [SerializeField] private TextMeshProUGUI wepName;
    [SerializeField] private TextMeshProUGUI wepDesc;


    [Space] [Space] 
    [Header("Bars: ")] 
    [SerializeField] private Slider atkBar;
    [SerializeField] private Slider rangeBar;
    [SerializeField] private Slider speedBar;

    [SerializeField] private MeshFilter modelMesh;

    private float _maxVal = 10f;

    public void OpenMenu()
    {
        menu.gameObject.SetActive(true);
    }

    public void GetCutlass()
    {
        GetWeaponElements(0);
        //LoadWepScene();
    }

    public void GetDagger()
    {
        GetWeaponElements(1);
       //UnloadWepScene();
    }

    private void GetWeaponElements(int index)
    {
        ChoosingWeapon so = choosingWeaponSo[index];
        
        SetText(wepName, so.weaponName);
        SetText(wepDesc, so.weaponDescription);
        
        SetBar(atkBar, so.weaponDamage / _maxVal);
        SetBar(rangeBar, so.weaponDamageRange / _maxVal);
        SetBar(speedBar, so.weaponDamageSpeed / _maxVal);

        modelMesh.mesh = so.weaponModel.GetComponent<MeshFilter>().sharedMesh;
    }

    private void SetText(TextMeshProUGUI textField, string text)
    {
        textField.text = text;
    }

    private void SetBar(Slider slider, float value)
    {
        slider.value = value;
    }

    private void LoadWepScene()
    {
        SceneManager.LoadSceneAsync("Choose_weapon", LoadSceneMode.Additive);
    }

    private void UnloadWepScene()
    {
        SceneManager.UnloadSceneAsync("Choose_weapon");
    }
}
