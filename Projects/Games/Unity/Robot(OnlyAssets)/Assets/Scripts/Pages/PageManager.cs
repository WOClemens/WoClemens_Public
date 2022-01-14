using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    [Header("Pages")]
    public GameObject startPage;
    public GameObject towerPage;
    public GameObject Playerpage;
    public GameObject shopPage;
    public GameObject arenaPage;
    public GameObject weaponPage;
    public GameObject specialPage;
    public GameObject towerInvent;
    public GameObject questsPage;

    public Animator anim;

    enum PageState
    {
        Start,
        Towers,
        Players,
        Shop,
        Arena,
        Weapon,
        Special,
        Quests,
    }

    void Start()
    {
        SetPageState(PageState.Start);
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.Start:
                startPage.SetActive(true);
                towerPage.SetActive(false);
                Playerpage.SetActive(false);
                shopPage.SetActive(false);
                arenaPage.SetActive(false);
                weaponPage.SetActive(false);
                specialPage.SetActive(false);
                towerInvent.SetActive(true);
                questsPage.SetActive(false);
                break;
            case PageState.Towers:
                startPage.SetActive(false);
                towerPage.SetActive(true);
                Playerpage.SetActive(false);
                shopPage.SetActive(false);
                arenaPage.SetActive(false);
                weaponPage.SetActive(false);
                specialPage.SetActive(false);
                towerInvent.SetActive(true);
                questsPage.SetActive(false);
                break;
            case PageState.Players:
                startPage.SetActive(false);
                towerPage.SetActive(false);
                Playerpage.SetActive(true);
                shopPage.SetActive(false);
                arenaPage.SetActive(false);
                weaponPage.SetActive(false);
                specialPage.SetActive(false);
                towerInvent.SetActive(false);
                questsPage.SetActive(false);
                break;
            case PageState.Shop:
                startPage.SetActive(false);
                towerPage.SetActive(false);
                Playerpage.SetActive(false);
                shopPage.SetActive(true);
                arenaPage.SetActive(false);
                weaponPage.SetActive(false);
                specialPage.SetActive(false);
                towerInvent.SetActive(false);
                questsPage.SetActive(false);
                break;
            case PageState.Arena:
                startPage.SetActive(false);
                towerPage.SetActive(false);
                Playerpage.SetActive(false);
                shopPage.SetActive(false);
                arenaPage.SetActive(true);
                weaponPage.SetActive(false);
                specialPage.SetActive(false);
                towerInvent.SetActive(false);
                questsPage.SetActive(false);
                break;
            case PageState.Weapon:
                startPage.SetActive(false);
                towerPage.SetActive(false);
                Playerpage.SetActive(false);
                shopPage.SetActive(false);
                arenaPage.SetActive(false);
                weaponPage.SetActive(true);
                specialPage.SetActive(false);
                towerInvent.SetActive(false);
                questsPage.SetActive(false);
                break;
            case PageState.Special:
                startPage.SetActive(false);
                towerPage.SetActive(false);
                Playerpage.SetActive(false);
                shopPage.SetActive(false);
                arenaPage.SetActive(false);
                weaponPage.SetActive(false);
                specialPage.SetActive(true);
                towerInvent.SetActive(false);
                questsPage.SetActive(false);
                break;
            case PageState.Quests:
                startPage.SetActive(false);
                towerPage.SetActive(false);
                Playerpage.SetActive(false);
                shopPage.SetActive(false);
                arenaPage.SetActive(false);
                weaponPage.SetActive(false);
                specialPage.SetActive(false);
                towerInvent.SetActive(false);
                questsPage.SetActive(true);
                break;
        }
    }
    
    public void TowersOn ()
    {
        anim.ResetTrigger("isStart");
        anim.SetTrigger("isTower");
        SetPageState(PageState.Towers);
    }

    public void StartOn ()
    {
        anim.ResetTrigger("isTower");
        anim.SetTrigger("isStart");
        SetPageState(PageState.Start);
    }


    public void PlayerOn ()
    {
        SetPageState(PageState.Players);
    }

    public void ShopOn ()
    {
        SetPageState(PageState.Shop);
    }
    
    public void ArenaOn ()
    {
        SetPageState(PageState.Arena);
    }

    public void WeaponOn()
    {
        SetPageState(PageState.Weapon);
    }
    public void SpecialsOn()
    {
        SetPageState(PageState.Special);
    }
    public void QuestsOn()
    {
        SetPageState(PageState.Quests);
    }

    void Update()
    {
        
    }
}
