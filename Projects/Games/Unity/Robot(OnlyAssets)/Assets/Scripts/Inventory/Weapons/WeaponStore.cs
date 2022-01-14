using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStore : MonoBehaviour
{
    const string key = "weapons";
    public string isBought = "false";
    bool isFalse = false;

    [Header("Waffe im gebrauch")]
    public int useWeapon = 0;

    int selected = 0;
    WeaponSaving weapons;
    MoneyManager moneyManager;
    public GameObject buyinfo;

    [Header("Buttons & Images")]
    public Button[] Weaponbuttons;
    public Image InfoImage;
    public Image EquiImage;
    public Image StartImage;
    public GameObject[] locks;

    [Header("Waffen Kosten - Name")]
    public int[] weaponsKost;
    public Text text_weaponKost;
    public string[] weaponNames;
    public Text text_weaponName;

    [Header("Waffen Damag")]
    public Text selectText;

    [Header("Stats - Data")]
    public int[] weaponDamag;
    public float[] msBetweenShoots;
    public string[] weaponsAbility;

    [Header("Stats - Texts")]
    public Image damShootBar;
    public Image msBar;
    public Image damSecBar;
    public Text text_DamShoot;
    public Text text_ShoSecond;
    public Text text_DamSecond;
    public Text text_ability;

    [Header("Videos")]
    public GameObject[] videos;

    public string isGunBought = "fuck";

    void Start()
    {
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        weapons = GameObject.Find("WeaponSaving").GetComponent<WeaponSaving>();
        useWeapon = PlayerPrefs.GetInt("useWeapon");
        selected = useWeapon;
        buyinfo.SetActive(false);       
        ChangeInfoImage();
        ChangeEquippedImage();
        ShowLock();
        selectText.text = "Select";
        text_weaponName.text = weaponNames[selected];
        StartImage.sprite = Weaponbuttons[selected].image.sprite;
    }

    void Update()
    {
        isBought = weapons.GetStringAt(key, selected);
    }

    public void TapOnGun (int element)
    {
        ShowStats(element);
        ShowVideo(element);
        selected = element;
        isGunBought = weapons.GetStringAt(key, selected);
        if(isGunBought == "true")
        {
            selectText.text = "Select";
        }
        else
        {
            selectText.text = "Buy";
        }
        ChangeInfoImage();
        text_weaponName.text = weaponNames[selected];
    }

    public void Select()
    {
        if(isBought == "true")
        {
            useWeapon = selected;
            PlayerPrefs.SetInt("useWeapon", useWeapon);
            ChangeEquippedImage();
            StartImage.sprite = Weaponbuttons[selected].image.sprite;
        }

        else
        {
            OpenBuyInfo();
        }
    }

    void OpenBuyInfo ()
    {
        buyinfo.SetActive(true);
        for (int i = 0; i < weaponsKost.Length; i++)
        {
            if (i == selected)
            {
                text_weaponKost.text = weaponsKost[i].ToString();
            }
        }
    }

    public void CancelBuy()
    {
        buyinfo.SetActive(false);
    }

    public void Buy()
    {
        if(moneyManager.money >= weaponsKost[selected])
        {
            GameObject.Find("OverManager(Clone)").GetComponent<OverManager>().Q2(weaponsKost[selected]);
            moneyManager.money -= weaponsKost[selected];
            weapons.SetToTrue(key, selected);
            buyinfo.SetActive(false);
            ShowLock();
        }
        else
        {
            GameObject.Find("SceneLoader").GetComponent<GameManager>().Show_NoMoney_Error();
        }
    }

    void ShowLock ()
    {
        for(int i = 0; i < Weaponbuttons.Length; i++)
        {
            if(weapons.GetStringAt(key, i) == "false")
            {
                locks[i].SetActive(true);
            }
            else
            {
                locks[i].SetActive(false);
            }
        }
    }

    void ChangeInfoImage()// die Waffe die gerade ausgewählt wurde
    {
        InfoImage.sprite = Weaponbuttons[selected].image.sprite;
    }

    void ChangeEquippedImage()
    {
        EquiImage.sprite = Weaponbuttons[useWeapon].image.sprite;
    }
    
    void ShowStats(int gun)
    {
        damShootBar.fillAmount = weaponDamag[gun] / 100f; // weaponDamag[gun] / 100
        msBar.fillAmount = msBetweenShoots[gun] / 2000f;
        damSecBar.fillAmount = (weaponDamag[gun] * (10000f / msBetweenShoots[selected])) / 100f;
       
        text_DamShoot.text = "~" + weaponDamag[gun].ToString();
        text_ShoSecond.text = "~" + msBetweenShoots[gun].ToString();
        text_DamSecond.text = "~" + (weaponDamag[gun] * (10000 / msBetweenShoots[selected])).ToString("F2");
        text_ability.text = weaponsAbility[gun];
    }

    void ShowVideo(int videoNR)
    {
        for(int i = 0; i < videos.Length; i++)
        {
            videos[i].SetActive(false);
        }
        videos[videoNR].SetActive(true);
    }
}
