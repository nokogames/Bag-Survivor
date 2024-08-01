

using System;
using System.Collections.Generic;
using _Project.Scripts.Character.Bot;
using _Project.Scripts.Character.Runtime.SerializeData;
using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime.Controllers
{

    public class BotController
    {
        [Inject] private LifetimeScope _playerScope;
        [Inject] private ICharacter _character;
        [Inject] private PlayerUIData _playerUIData;
        private List<Transform> _botPlacePoints;
        private List<BotSM> _bots;
        private List<Sequence> _sequnce;
        private GameObject _botPrefab;
        private ReciveType _reciveType;
        private int _botCount = 0;
        private float _botCoolDown = 3f;
        private float _botFightableTime = 3f;
        private float _crrFightTime = 0;
        private float _crrCoolTime = 0;
        private bool _isCoolDown;
        private bool _isPlaced;
        public void Initialise(GameObject botPrefab, List<Transform> botPlacePoints)
        {
            // _bots = new(botPlacePoints.Count);
            // _botPrefab = botPrefab;
            // _botPlacePoints = botPlacePoints;
            // CreateBotes();
            // _sequnce = new(_bots.Count);
            // _playerUIData.coolDownPanel.SetActive(false);

        }
        public void SetCoolDown(float botCoolDown, float botFightableTime)
        {
            // _botCoolDown = botCoolDown;
            // _botFightableTime = botFightableTime;
        }
        private void CreateBotes()
        {
            // for (int i = 0; i < _botPlacePoints.Count; i++)
            // {
            //     var crrPoint = _botPlacePoints[i];
            //     var obj = GameObject.Instantiate(_botPrefab);
            //     obj.transform.SetParent(crrPoint);
            //     obj.transform.localPosition = Vector3.zero;
            //     obj.transform.localRotation = Quaternion.identity;
            //     var botSm = obj.GetComponent<BotSM>();
            //     botSm.InjectDependenciesAndInitialize(_playerScope, _character);
            //     botSm.Initialize(crrPoint);
            //     _bots.Add(botSm);
            // }
        }

        public void PlaceBots()
        {
            // _reciveType = ReciveType.Place;
            // PlaceBot();
            // // _bots.ForEach(bot => bot.ChangeStatByPlayer(bot.PlaceToPlayerState));
        }

        private void PlaceBot()
        {
            // if (_isPlaced) return;
            // _isPlaced = true;
            // for (int i = 0; i < _botCount; i++)
            // {
            //     var bot = _bots[i];
            //     bot.ChangeStatByPlayer(bot.PlaceToPlayerState);
            // }
        }

        internal void CraftBots()
        {
            // _reciveType = ReciveType.Craft;
            // if (_isCoolDown) return;
            // _isPlaced = false;
            // for (int i = 0; i < _botCount; i++)
            // {
            //     var bot = _bots[i];
            //     bot.ChangeStatByPlayer(bot.CraftState);
            // }

            // //_bots.ForEach(bot => bot.ChangeStatByPlayer(bot.CraftState));
        }

        internal void AttackBots()
        {
            // _reciveType = ReciveType.Attac;
            // if (_isCoolDown) return;
            // _isPlaced = false;
            // for (int i = 0; i < _botCount; i++)
            // {
            //     var bot = _bots[i];
            //     bot.ChangeStatByPlayer(bot.AttackState);
            // }
            // // _bots.ForEach(bot => bot.ChangeStatByPlayer(bot.AttackState));

        }

        internal void SetBotCount(int botCount)
        {

          //  _botCount = botCount;
        }

        public void Tick()
        {
            // if (!_isPlaced)
            // {
            //     // _playerUIData.coolDownPanel.SetActive(false);
            //     _crrFightTime += Time.deltaTime;
            //     _playerUIData.coolDownImg.fillAmount = 1f - _crrFightTime / _botFightableTime;
            // }
            // if (_crrFightTime > _botFightableTime)
            // {
            //     PlaceBot();
            //     _isCoolDown = true;
            //     _crrFightTime = 0;
            //     _playerUIData.coolDownPanel.SetActive(true);
            // }
            // if (_isPlaced && _isCoolDown)
            // {
            //     _crrCoolTime += Time.deltaTime;
            //     _playerUIData.coolDownImg.fillAmount = _crrCoolTime / _botCoolDown;
            //     if (_crrCoolTime > _botCoolDown)
            //     {
            //         _isCoolDown = false;
            //         _crrCoolTime = 0;
            //         //   _playerUIData.coolDownPanel.SetActive(false);
            //         switch (_reciveType)
            //         {
            //             case ReciveType.Attac:
            //                 AttackBots();
            //                 break;
            //             case ReciveType.Craft:
            //                 CraftBots();
            //                 break;
            //             case ReciveType.Place:
            //                 PlaceBots();
            //                 break;

            //         }
            //     }
            // }
        }


        public enum ReciveType
        {

            Place = default,
            Attac,
            Craft,

        }
    }
}