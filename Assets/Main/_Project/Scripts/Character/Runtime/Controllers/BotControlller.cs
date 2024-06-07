

using System.Collections.Generic;
using _Project.Scripts.Character.Bot;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Character.Runtime.Controllers
{

    public class BotController
    {
        private List<Transform> _botPlacePoints;
        private List<BotSM> _bots;
        private List<Sequence> _sequnce;

        public void Initialise(List<BotSM> bots, List<Transform> botPlacePoints)
        {
            _bots = bots;
            _botPlacePoints = botPlacePoints;
            _sequnce = new(_bots.Count);

        }

        public void PlaceBots()
        {
            for (int i = 0; i < _bots.Count; i++)
            {
                var crrBot = _bots[i];
                var sequence = _sequnce[i];
                if (sequence != null) sequence.Kill();

            }
        }
        public void UnPlaceBots()
        {

        }
    }
}