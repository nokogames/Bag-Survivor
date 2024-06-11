
using System;
using UnityEngine;
namespace _Project.Scripts.Character.Craft
{
    public interface ITargetable
    {
        public Transform Transform { get; }
        //public Action OnTargetChanged { get; set; }
    }
}