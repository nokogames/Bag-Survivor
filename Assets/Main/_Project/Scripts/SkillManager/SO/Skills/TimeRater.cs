using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.SkillManagement.SO.Skills
{
    public class TimeRater
    {

        private float _timeRate;
        private float _currentTime;
        public bool IsValid { get; private set; }
        public float CrrTime =>_currentTime;
        public TimeRater(float timeRate, float startTime = 0)
        {

            _timeRate = timeRate;
            _currentTime = startTime;
        }

        public void SetTimeRate(float timeRate)
        {
            _timeRate = timeRate;
        }
        public float Percentage => _currentTime / _timeRate;

        public bool Execute(float deltaTime)
        {
            _currentTime += deltaTime;
            IsValid = false;
            if (_currentTime >= _timeRate)
            {
                _currentTime = 0; // Zamanlayıcıyı sıfırla veya başka bir işlem yap
                IsValid = true;
            }

            // CustomExtentions.ColorLog($"Timer deltaTime {deltaTime}  ", Color.yellow);
            // CustomExtentions.ColorLog($"Timer _timeRate{_timeRate}  ", Color.green);

            return IsValid;
        }

        public static TimeRater Init(float timeRate)
        {
            return new TimeRater(timeRate);
        }
    }
}