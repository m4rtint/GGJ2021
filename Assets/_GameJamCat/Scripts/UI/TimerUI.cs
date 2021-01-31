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
        private float _time = 0f;
        private bool _maxTimeReached = false;

        private void CleanUp()
        {
            _maxTimeReached = false;
        }

        private void OnEnable()
        {
            CleanUp();
        }
        
        private void Update()
        {
            
        }

        private bool HasTimeRunOut()
        {
            return false;
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
