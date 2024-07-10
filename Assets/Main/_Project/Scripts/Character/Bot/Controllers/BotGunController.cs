using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Bot.Gun;
using VContainer;

namespace _Project.Scripts.Character.Bot.Controllers
{
    public class BotGunController
    {
        [Inject] private ICharacter _character;
        [Inject] private BasicBotGun _botGun;
        [Inject] private UpgradedBotGun _upgradedGun;

        // public void UpdateGun(BotGunBase botGunBase)
        // {
        //     _botGun = botGunBase;
        // }

        public void FixedTick()
        {
            // _botGun.FixedTick();
            if (_upgradedGun.gameObject.activeSelf) _botGun.FixedTick();
        }
    }
}