


using RollicGames.Advertisements;

public static class CustomAddManager
{

    public static void Initialize()
    {
#if !UNITY_EDITOR
        RLAdvertisementManager.Instance.init();
#endif
    }
    public static void ShowBanner()
    {
#if !UNITY_EDITOR
        RLAdvertisementManager.Instance.loadBanner();
#endif
    }
    public static void HideBanner()
    {
#if !UNITY_EDITOR
        RLAdvertisementManager.Instance.hideBanner();
#endif
    }
}