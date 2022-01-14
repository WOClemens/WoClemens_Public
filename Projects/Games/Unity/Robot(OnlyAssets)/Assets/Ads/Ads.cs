using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    MoneyManager mManager;

    private string store_id_android = "3607711";
    private string rewarded_video = "rewardedVideo";
    private string video_ad = "video";

    [Header("Shope Ad One")]
    public int shopOneAd;
    public int shopOneAdAmmount;
    public int shopOneAdAmmountMoney;
    bool isOne = false;
    public Text AdOne_Text;
    public GameObject Object_AdOne_CollectButton;

    [Header("Shope Ad Two")]
    public int shopTwoAd;
    public int shopTwoAdAmmount;
    public int shopTwoAdAmmountMoney;
    public Text AdTwo_Text;
    public GameObject Object_AdTwo_CollectButton;

    [Header("Shope Ad Three")]
    public int shopThreeAd;
    public int shopThreeAdAmmount;
    public int shopThreeAdAmmountMoney;
    public Text AdThree_Text;
    public GameObject Object_AdThree_CollectButton;

    [Header("Shope Ad Four")]
    public int shopFourAd;
    public int shopFourAdAmmount;
    public int shopFourAdAmmountMoney;
    public Text AdFour_Text;
    public GameObject Object_AdFour_CollectButton;

    void Start()
    {
        mManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        Advertisement.Initialize(store_id_android, false);

        shopOneAd = PlayerPrefs.GetInt("AdOne");
        shopTwoAd = PlayerPrefs.GetInt("AdTwo");
        shopThreeAd = PlayerPrefs.GetInt("AdThree");
        shopFourAd = PlayerPrefs.GetInt("AdFour");

        AdOne_Text.text = shopOneAd.ToString() + " / " + shopOneAdAmmount.ToString();
        AdTwo_Text.text = shopTwoAd.ToString() + " / " + shopTwoAdAmmount.ToString();
        AdThree_Text.text = shopThreeAd.ToString() + " / " + shopThreeAdAmmount.ToString();
        AdFour_Text.text = shopFourAd.ToString() + " / " + shopFourAdAmmount.ToString();

        Object_AdOne_CollectButton.SetActive(false);
        Object_AdTwo_CollectButton.SetActive(false);
        Object_AdThree_CollectButton.SetActive(false);
        Object_AdFour_CollectButton.SetActive(false);
        if (shopOneAd >= shopOneAdAmmount)
        {
            Object_AdOne_CollectButton.SetActive(true);
        }
        if (shopTwoAd >= shopTwoAdAmmount)
        {
            Object_AdTwo_CollectButton.SetActive(true);
        }
        if (shopThreeAd >= shopThreeAdAmmount)
        {
            Object_AdThree_CollectButton.SetActive(true);
        }
        if (shopFourAd >= shopFourAdAmmount)
        {
            Object_AdFour_CollectButton.SetActive(true);
        }
    }

    public void PlayMoneyAd()
    {
        if(Advertisement.IsReady(rewarded_video))
        {
            // Show an ad:
            Advertisement.Show(rewarded_video);
            mManager.money += 1000;
        }
    }

    public void PlayAdOne()
    {
        if (Advertisement.IsReady(rewarded_video) && shopOneAd < shopOneAdAmmount)
        {
            // Show an ad:
            Advertisement.Show(rewarded_video);
            shopOneAd++;
        }
        if(shopOneAd >= shopOneAdAmmount)
        {
            Object_AdOne_CollectButton.SetActive(true);
        }

        AdOne_Text.text = shopOneAd.ToString() + " / " + shopOneAdAmmount.ToString();
        PlayerPrefs.SetInt("AdOne", shopOneAd);
    }

    public void PlayAdTwo()
    {
        if (Advertisement.IsReady(rewarded_video) && shopTwoAd < shopTwoAdAmmount)
        {
            // Show an ad:
            Advertisement.Show(rewarded_video);
            shopTwoAd++;
        }
        if (shopTwoAd >= shopTwoAdAmmount)
        {
            Object_AdTwo_CollectButton.SetActive(true);
        }

        AdTwo_Text.text = shopTwoAd.ToString() + " / " + shopTwoAdAmmount.ToString();
        PlayerPrefs.SetInt("AdTwo", shopTwoAd);
    }

    public void PlayAdThree()
    {
        if (Advertisement.IsReady(rewarded_video) && shopThreeAd < shopThreeAdAmmount)
        {
            // Show an ad:
            Advertisement.Show(rewarded_video);
            shopThreeAd++;
        }
        if (shopThreeAd >= shopThreeAdAmmount)
        {
            Object_AdThree_CollectButton.SetActive(true);
        }

        AdThree_Text.text = shopThreeAd.ToString() + " / " + shopThreeAdAmmount.ToString();
        PlayerPrefs.SetInt("AdThree", shopThreeAd);
    }

    public void PlayAdFour()
    {
        if (Advertisement.IsReady(rewarded_video) && shopThreeAd < shopThreeAdAmmount)
        {
            // Show an ad:
            Advertisement.Show(rewarded_video);
            shopFourAd++;
        }
        if (shopFourAd >= shopFourAdAmmount)
        {
            Object_AdFour_CollectButton.SetActive(true);
        }

        AdFour_Text.text = shopFourAd.ToString() + " / " + shopFourAdAmmount.ToString();
        PlayerPrefs.SetInt("AdFour", shopFourAd);
    }

    public void GeldPlusTest()
    {
        mManager.money += 1000;
    }

    public void CollectAd(int number)
    {
        if(number == 1)
        {
            if (shopOneAd >= shopOneAdAmmount)
            {
                mManager.money += shopOneAdAmmountMoney;
                shopOneAd = 0;
                Object_AdOne_CollectButton.SetActive(false);
            }
        }
        else if (number == 2)
        {
            if (shopTwoAd >= shopTwoAdAmmount)
            {
                mManager.money += shopTwoAdAmmountMoney;
                shopTwoAd = 0;
                Object_AdTwo_CollectButton.SetActive(false);
            }
        }
        else if(number == 3)
        {
            if (shopThreeAd >= shopThreeAdAmmount)
            {
                mManager.money += shopThreeAdAmmountMoney;
                shopThreeAd = 0;
                Object_AdThree_CollectButton.SetActive(false);
            }
        }
        else if(number == 4)
        {
            if (shopFourAd >= shopFourAdAmmount)
            {
                mManager.money += shopFourAdAmmountMoney;
                shopFourAd = 0;
                Object_AdFour_CollectButton.SetActive(false);
            }
        }

        AdOne_Text.text = shopOneAd.ToString() + " / " + shopOneAdAmmount.ToString();
        AdTwo_Text.text = shopTwoAd.ToString() + " / " + shopTwoAdAmmount.ToString();
        AdThree_Text.text = shopThreeAd.ToString() + " / " + shopThreeAdAmmount.ToString();
        AdFour_Text.text = shopFourAd.ToString() + " / " + shopFourAdAmmount.ToString();

        PlayerPrefs.SetInt("AdOne", shopOneAd);
        PlayerPrefs.SetInt("AdTwo", shopTwoAd);
        PlayerPrefs.SetInt("AdThree", shopThreeAd);
        PlayerPrefs.SetInt("AdFour", shopFourAd);
    }

    public void GoToUrl (string url)
    {
        Application.OpenURL(url);
    }

    /*public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning(“The ad did not finish due to an error.”);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }*/

}