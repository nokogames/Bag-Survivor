using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace _Project.Scripts.Character.Bot
{

    public class BotUIMediator : MonoBehaviour
    {
        [SerializeField] private Image debugImg;
        public void SetDebugImgColor(Color color)=>debugImg.color = color;
    }

}