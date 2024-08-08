using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpgradeVisualController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Transform playerSelectionPoint;
    [SerializeField] private Transform playerDisablePoint;
    [SerializeField] private ParticleSystem playerUpgradeParticle;
    //[SerializeField] private GameObject botObj;
    [SerializeField] private Transform botSelectionPoint;
    [SerializeField] private Transform botDisablePoint;
    [SerializeField] private ParticleSystem botUpgradeParticle;

    [SerializeField] private Camera camera;

    private Tween _playerTween;
    private Tween _botTween;
    private float _jumpPower = 0.7f;
    public void PlayerSelected()
    {
        if (_playerTween != null) _playerTween.Kill();
        if (_botTween != null) _botTween.Kill();

        _playerTween = playerObj.transform.DOJump(playerSelectionPoint.position, _jumpPower, 1, .5f);
        //   _botTween = botObj.transform.DOJump(botDisablePoint.position, _jumpPower, 1, .5f);
    }
    public void BotSelected()
    {
        if (_playerTween != null) _playerTween.Kill();
        if (_botTween != null) _botTween.Kill();

        _playerTween = playerObj.transform.DOJump(playerDisablePoint.position, _jumpPower, 1, .5f);
        // _botTween = botObj.transform.DOJump(botSelectionPoint.position, _jumpPower, 1, .5f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) PlayerSelected();
        if (Input.GetKeyDown(KeyCode.O)) BotSelected();
    }
    public void PlayerUpgraded()
    {
        animator.SetTrigger("Jump");
        playerUpgradeParticle.Play();
    }
    public void BotUpgraded() => botUpgradeParticle.Play();

    internal void Activity(bool v)
    {
        playerObj.SetActive(v);
        //  botObj.SetActive(v);
        camera.enabled = v;
    }
}
