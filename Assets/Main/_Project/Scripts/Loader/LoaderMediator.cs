

using System;
using UnityEngine;
using UnityEngine.UI;

public class LoaderMediator : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject splashPanel;
    [SerializeField] private Animator splashAnimator;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        AssignComponents();
        InitializeValues();
        //  DontDestroyOnLoad(gameObject);
    }

    private void InitializeValues()
    {
        loadingPanel.SetActive(false);
        splashPanel.SetActive(false);
    }

    private void AssignComponents()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        bar.fillAmount = 0;
        _canvasGroup.alpha = 1f;

    }

    public void StartLoading()
    {
        loadingPanel.SetActive(true);
        _canvasGroup.alpha = 1f;
        bar.fillAmount = 0;
    }
    public void FinishLoading()
    {
        _canvasGroup.alpha = 0f;
        loadingPanel.SetActive(false);
    }

    public void Loading(float progressAmount)
    {
        bar.fillAmount = progressAmount;

    }


    public void StartSplashLoading()
    {
        //Start splash animation
        splashPanel.SetActive(true);
        _canvasGroup.alpha = 1f;
        splashAnimator.SetBool("isOpen", true);
        splashAnimator.SetBool("isClose", false);
    }
    public void StopSplashLoading()
    {
        splashAnimator.SetBool("isOpen", false);
        splashAnimator.SetBool("isClose", true);
    }
    public void FinishSplashLoading()
    {
        //Stop splash animation
        splashPanel.SetActive(false);
        _canvasGroup.alpha = 0f;

    }

}
