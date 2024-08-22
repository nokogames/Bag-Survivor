



using ElephantSDK;
using RollicGames.Advertisements;
using UnityEngine;

public static class CustomAddManager
{

    public static void Initialize()
    {

      
        CustomExtentions.ColoredLog("Init", Color.blue);
        RLAdvertisementManager.Instance.init();

    }
    public static void ShowBanner()
    {

        RLAdvertisementManager.Instance.loadBanner();
        CustomExtentions.ColoredLog("ShowBanner", Color.green);

    }
    public static void HideBanner()
    {

        RLAdvertisementManager.Instance.hideBanner();
        CustomExtentions.ColoredLog("ShowHide", Color.red);

    }
}