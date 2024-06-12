using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class PlayerAnimationController
    {
        [Inject] private CharacterGraphics _characterGraphics;

        public void SetCraftStatus(bool status)
        {
            _characterGraphics.CraftStatus(status);
        }

        internal void OnGunShooted()
        {
            _characterGraphics.OnShoot();
        }
    }
}