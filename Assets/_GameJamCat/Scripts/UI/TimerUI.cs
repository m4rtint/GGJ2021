using System;
using TMPro;
using UnityEngine;

namespace GameJamCat
{
    public class TimerUI : MonoBehaviour
    {
        private const string TimeSpanFormat = "mm\\:ss";
        [SerializeField] private TextMeshProUGUI _timerText = null;
        private TimeSpan _timeSpan;
        private float _maxTime = 0f;
        private float _time = 0f;
        private bool _maxTimeReached = false;

        public void Initialize(float maxTime)
        {
            _maxTime = maxTime;
            CleanUp();
        }

        public void UpdateTime(float time)
        {
            if (!_maxTimeReached)
            {
                _time = time;
                SetTimeUI(_time);
                HasTimeRunOut();
            }
            else
            {
                SetTimeUI(_maxTime);
            }
        }

        public void CleanUp()
        {
            _maxTimeReached = false;
            SetTimeUI(0f);
        }

        private void HasTimeRunOut()
        {
            _maxTimeReached = _time >= _maxTime;
        }

        private void SetTimeUI(float time)
        {
            _timeSpan = TimeSpan.FromSeconds(time);

            if (_timerText != null)
            {
                _timerText.text = _timeSpan.ToString(TimeSpanFormat);
            }
        }
    }
}
