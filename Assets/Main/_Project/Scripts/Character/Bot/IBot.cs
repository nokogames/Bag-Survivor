using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.Character.Bot
{

    public interface IBot
    {
       public Transform PlayerPlacePoint{ get; set; }
       public Transform Transform {get;}
    }

}