using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Bot.Tools;
using VContainer;

namespace _Project.Scripts.Character.Bot.Controllers
{
    public class BotCraftController
    {
        [Inject] private BotGunController botGunController;
        [Inject] private BotCraftTool _craftTool;
    }
}