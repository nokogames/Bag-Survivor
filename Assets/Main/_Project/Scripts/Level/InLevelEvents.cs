using System;
using UnityEngine;

namespace _Project.Scripts.Level
{
    public class InLevelEvents : IDisposable
    {
        public Action onAbleToNextLevel;
        public Action onNextLevel;
        public Action onNextLevelStarted;
        public Action onShowNextLevelUI;
        //Section
        public Action onAbleToNextSection;
        public Action onNextSection;
        public Action onShowNextSectionUI;
        public Action onNextSectionStarted;



        public void Dispose()
        {
            onAbleToNextLevel = null;
            onNextLevel = null;
            onNextLevelStarted = null;


            onAbleToNextSection = null;
            onNextSection = null;
            onShowNextSectionUI = null;
            onNextSectionStarted = null;
            onShowNextLevelUI = null;
        }
    }
}