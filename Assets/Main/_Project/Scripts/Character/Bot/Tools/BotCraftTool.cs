using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Craft;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Bot.Tools
{

    public class BotCraftTool : MonoBehaviour
    {


        [SerializeField] private ParticleSystem shootParticle;
        [SerializeField] private Transform shootPoint;
        [Inject] private ICharacter _character;

        private float _shootTimeRate = .5f;
        private int _bulletPoolIndex = 1;
        private float _craftPercentage = 1f;

        // [Inject] private BotAnimationController _botAnimController;

        internal void FixedTick()
        {

            if (_character.Craftable == null) return;
            Crafting();

        }

        float _crrTime = 0;
        private void Crafting()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _shootTimeRate) return;
            _crrTime = 0;
            Craft();


        }
        private void Craft()
        {
            Crafted();
            PlayParticle();
        }

        private void PlayParticle()
        {
            if (shootParticle != null) shootParticle.Play();
        }

        private void Crafted()
        {
            if (_character.Craftable == null) return;

            _character.Craftable.Craft(_craftPercentage);

        }



    }
}
