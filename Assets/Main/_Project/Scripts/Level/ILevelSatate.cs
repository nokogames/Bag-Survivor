using System;
using UnityEngine;
namespace _Project.Scripts.Level
{

    public interface ILevelState
    {
        public void AbleToNextLevel();
        public void NextLevel();
        public void NextLevelStarted();
        //Section
        public void AbleToNextSection();
        public void NextSection();
        public void NextSectionStarted();

    }
}