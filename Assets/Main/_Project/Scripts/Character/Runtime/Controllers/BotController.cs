

using System;
using System.Collections.Generic;
using _Project.Scripts.Character.Bot;
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
        private List<Transform> _botPlacePoints;
        private List<BotSM> _bots;
        private List<Sequence> _sequnce;
        private GameObject _botPrefab;

        private int _botCount = 0;
        public void Initialise(GameObject botPrefab, List<Transform> botPlacePoints)
        {
            _bots = new(botPlacePoints.Count);
            _botPrefab = botPrefab;
            _botPlacePoints = botPlacePoints;
            CreateBotes();
            _sequnce = new(_bots.Count);

        }

        private void CreateBotes()
        {
            for (int i = 0; i < _botPlacePoints.Count; i++)
            {
                var crrPoint = _botPlacePoints[i];
                var obj = GameObject.Instantiate(_botPrefab);
                obj.transform.SetParent(crrPoint);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                var botSm = obj.GetComponent<BotSM>();
                botSm.InjectDependenciesAndInitialize(_playerScope, _character);
                botSm.Initialize(crrPoint);
                _bots.Add(botSm);
            }
        }

        public void PlaceBots()
        {
            for (int i = 0; i < _botCount; i++)
            {
                var bot = _bots[i];
                bot.ChangeStatByPlayer(bot.PlaceToPlayerState);
            }
            // _bots.ForEach(bot => bot.ChangeStatByPlayer(bot.PlaceToPlayerState));
        }
        public void UnPlaceBots()
        {

        }

        internal void CraftBots()
        {
            for (int i = 0; i < _botCount; i++)
            {
                var bot = _bots[i];
                bot.ChangeStatByPlayer(bot.CraftState);
            }

            //_bots.ForEach(bot => bot.ChangeStatByPlayer(bot.CraftState));
        }

        internal void AttackBots()
        {
            for (int i = 0; i < _botCount; i++)
            {
                var bot = _bots[i];
                bot.ChangeStatByPlayer(bot.AttackState);
            }
            // _bots.ForEach(bot => bot.ChangeStatByPlayer(bot.AttackState));

        }

        internal void SetBotCount(int botCount) 
        {

            _botCount = botCount;
        }
    }
}