using System;
using UnityEngine;

namespace GameJamCat
{
    public class UIManager : MonoBehaviour
    {
        private readonly IStateManager _stateManager = StateManager.Instance;

        [SerializeField] private DossierViewBehaviour _dossierView = null;
        [SerializeField] private ScreenTransitionViewBehaviour _transitionViewBehaviour = null;
        [SerializeField] private TimerUI _timer = null;
        [SerializeField] private EndGameMenu _endgameViewBehaviour = null;
        [SerializeField] private GameObject _crossHair = null;

        private float _maxTime = 0f;
        public event Action OnTimerRanOut;

        /// <summary>
        /// Initialize UIManager, setup values here
        /// </summary>
        public void Initialize(float maxTime)
        {
            if (_dossierView != null)
            {
                _dossierView.Initialize();
                _dossierView.OnDossierStateChange += HandleOnDossierStateChange;
            }
            
            _stateManager.OnStateChanged += HandleOnStateChange;
            
            if (_transitionViewBehaviour != null)
            {
                _transitionViewBehaviour.OnCompleteFade += HandleOnFadeComplete;
            }

            if (_endgameViewBehaviour != null)
            {
                _endgameViewBehaviour.Initialize();
            }

            if (_crossHair != null)
            {
                _crossHair.gameObject.SetActive(false);
            }

            if (_timer != null)
            {
                _timer.Initialize(maxTime);
            }

            _maxTime = maxTime;
        }

        public void UpdateTimer(float currentTime)
        {
            if (_timer != null)
            {
                _timer.UpdateTime(currentTime);

                if (currentTime > _maxTime)
                {
                    OnTimerRanOut?.Invoke();
                }
            }
        }

        /// <summary>
        /// Reset UI Values here, unsubscribe or reset values here
        /// </summary>
        public void CleanUp()
        {
            _stateManager.OnStateChanged -= HandleOnStateChange;
            if (_transitionViewBehaviour != null)
            {
                _transitionViewBehaviour.OnCompleteFade -= HandleOnFadeComplete;
            }

            if (_timer != null)
            {
                _timer.CleanUp();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        private void OnPregameSet()
        {
            if (_transitionViewBehaviour != null) 
            { 
                _transitionViewBehaviour.SwitchBlackScreen(true); 
            }

            SetCrossHairState(false);
        }        
        
        private void OnEndGameSet()
        {
            SetCrossHairState(false);

            if (_endgameViewBehaviour != null)
            {
                _endgameViewBehaviour.DisplayEndPanel(true); //placeholder boolean
            }
        }

        private void SetCrossHairState(bool state)
        {
            if (_crossHair != null)
            {
                _crossHair.SetActive(state);
            }
        }

        private void OnDialogueSet()
        {
            SetCrossHairState(false);
        }

        #region Delegate

        private void HandleOnStateChange(State state)
        {
            switch (state)
            {
                case State.Pregame:
                    OnPregameSet();
                    break;
                case State.Play:
                    break;
                case State.Dialogue:
                    OnDialogueSet();
                    break;
                case State.EndGame:
                    OnEndGameSet();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void HandleOnFadeComplete()
        {
            _stateManager.SetState(State.Play);
        }
        
        private void HandleOnDossierStateChange(bool isOpen)
        {
            SetCrossHairState(!isOpen);
        }
        #endregion 
    }
}
