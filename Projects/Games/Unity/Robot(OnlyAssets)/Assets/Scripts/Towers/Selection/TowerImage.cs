using UnityEngine;
using UnityEngine.UI;

public class TowerImage : MonoBehaviour
{
   
    const string towersKey = "towers";
    MoneyManager moneyManager;
    WeaponSaving weapons;

    [Header("Using Tower")]
    public int useNormaleTower = 0;

    [Header("Tower Werte")]
    public int[] towerLevel;
    public int[] towersKost;
    public int[] towerDamag;
    public int[] towerMsBetShoot;
    public float maxDamag;
    public float maxMsBetShoot;
    string[] towerWerte;

    [Header("Tower Text")]
    public Text towerLevelText;
    public Text towerDamagText;
    public Text towerHealtText;
    public Text towerPointText;
    public Text towerKostsText;

    [Header("Tower Text Buy")]
    public Text towerDamagTextBuy;
    public Text towerHealtTextBuy;
    public Text towerPointTextBuy;

    [Header("Stats - Texts")]
    public Image damShootBar;
    public Image msBar;
    public Text text_DamShoot;
    public Text text_MsShoot;

    [Header("Pages")]
    public GameObject InfoPage;
    public GameObject BuyPage;
    public GameObject StatsPage;
    

    [Header("Locks")]
    public GameObject[] locks;

    [Header("Türme")]
    public GameObject[] towers;
    Towers to;

    [Header("Upgraden")]
    public int upgradeKosten;
    public int upgradeProLevel;
    public int healtPlus;
    public int damagPlus;
    public int pointsMinus;
    public Text text_upgradeKosten;

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;


    void Start()
    {
        towerWerte = new string[3];
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        weapons = GameObject.Find("WeaponSaving").GetComponent<WeaponSaving>();
        InfoPage.SetActive(false);
        StatsPage.SetActive(false);
        BuyPage.SetActive(false);

        useNormaleTower = PlayerPrefs.GetInt("usingTower");

        for (int i = 0; i < towerLevel.Length; i++)
        {
            towerLevel[i] = PlayerPrefs.GetInt("towerLevel" + i);

            if (towerLevel[i] == 0)
            {
                towerLevel[i] = 1;
            }
        }
        ShowLock();
    }


    public void InfoOff ()
    {
        InfoPage.SetActive(false);
        BuyPage.SetActive(false);
        StatsPage.SetActive(false);
    }

    public void TowerInfoOn(int tower)
    {
        text_DamShoot.text = towerDamag[tower].ToString();
        text_MsShoot.text = towerMsBetShoot[tower].ToString();

        damShootBar.fillAmount = towerDamag[tower] / maxDamag;
        msBar.fillAmount = towerMsBetShoot[tower] / maxMsBetShoot;

        StatsPage.SetActive(true);
        useNormaleTower = tower;
        if (weapons.towers[useNormaleTower] == "false")
        {
            towerKostsText.text = towersKost[useNormaleTower].ToString();
            BuyPage.SetActive(true);
        }
        else
        {
            InfoPage.SetActive(true);
        }
    }

    public void Buy()
    {
        if(moneyManager.money >= towersKost[useNormaleTower])
        {
            GameObject.Find("OverManager(Clone)").GetComponent<OverManager>().Q2(towersKost[useNormaleTower]);
            moneyManager.money -= towersKost[useNormaleTower];
            weapons.SetToTrue(towersKey, useNormaleTower);
            BuyPage.SetActive(false);
            StatsPage.SetActive(false);
        }
        else
        {
            GameObject.Find("SceneLoader").GetComponent<GameManager>().Show_NoMoney_Error();
        }
        ShowLock();

    }

    public void TowerInfoOff()
    {
        BuyPage.SetActive(false);
        InfoPage.SetActive(false);
        StatsPage.SetActive(false);

    }
  
    void Update ()
    {
        PlayerPrefs.SetInt("usingTower", useNormaleTower);

        for (int i = 0; i < towerLevel.Length; i++)
        {
            PlayerPrefs.SetInt("towerLevel" + i, towerLevel[i]);
        }

        for (int i = 0; i < towerLevel.Length; i++)
        {
            if (useNormaleTower == i)
            {
                to = towers[i].GetComponent<Towers>();
                towerDamagText.text = to.damag.ToString();
                towerHealtText.text = to.health.ToString();
                towerPointText.text = to.kosten.ToString();
                towerLevelText.text = towerLevel[i].ToString();
                towerDamagTextBuy.text = to.damag.ToString();
                towerHealtTextBuy.text = to.health.ToString();
                towerPointTextBuy.text = to.kosten.ToString();
                int kosten = upgradeKosten;
                for (int a = 0; a < towerLevel[i]; a++)
                {
                    kosten += upgradeProLevel;
                }
                text_upgradeKosten.text = kosten.ToString();
            }
        }


    }

    public void Upgrade()
    {
        for(int i = 0; i < towerLevel.Length; i++)
        {
            if(useNormaleTower == i && towerLevel[i] <= 9)
            {
                int kosten = upgradeKosten;
                for (int a = 0; a < towerLevel[i]; a++)
                {
                    kosten += upgradeProLevel;
                }

                if (moneyManager.money >= kosten)
                {
                    moneyManager.money -= kosten;
                    to = towers[i].GetComponent<Towers>();
                    to.health += healtPlus;
                    to.damag += damagPlus;
                    to.kosten -= pointsMinus;
                    towerLevel[i]++;
                }
                else
                {
                    GameObject.Find("SceneLoader").GetComponent<GameManager>().Show_NoMoney_Error();
                }
                return;
            }
        }
        GameObject.Find("SceneLoader").GetComponent<GameManager>().Show_NoMoney_Error();
    }

    void ShowLock()
    {
        for(int i = 0; i < locks.Length; i++)
        {
            if(weapons.towers[i] == "false" && locks[i] != null)
            {
                locks[i].SetActive(true);
            }
            else if(locks[i] != null)
            {
                locks[i].SetActive(false);
            }
        }
    }
}
