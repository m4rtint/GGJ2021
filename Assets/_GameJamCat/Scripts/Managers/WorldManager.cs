using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJamCat
{
    public class WorldManager : MonoBehaviour
    {
        private readonly IStateManager _stateManager = StateManager.Instance;
        
        [Title("Properties")]
        [SerializeField]
        private int _lives = 3;

        [Title("Managers")] 
        [SerializeField] private PlayerController _playerController = null;
        [SerializeField] private CatManager _catManager = null;
        [SerializeField] private UIManager _uiManager = null;

        private const float MaxTime = 120f;
        private float _currentTime = 0f;
        private bool _hasTimerRunOut = false;
        
        public int Lives
        {
            get => _lives;
            set
            {
                _lives = value;
                _uiManager.SetLives(value);
            }
        }
        
        private void OnEnable()
        {
            if (_catManager != null)
            {
                _catManager.Initialize();
                _catManager.OnGeneratedSelectedCatToFind += HandleOnGeneratedSelectedCatToFind;
            }

            if (_uiManager != null)
            {
                _uiManager.Initialize(Lives);
            }
        }

        private void Start()
        {
            _stateManager.SetState(State.Pregame);
        }

        private void Update()
        {
            if (_hasTimerRunOut)
            {
                if (_stateManager.GetState() != State.EndGame)
                {
                    _stateManager.SetState(State.EndGame);
                }
            }

            _currentTime += Time.deltaTime;
            HasTimeRunOut();
            if (_uiManager != null)
            {
                _uiManager.UpdateTimer(_currentTime);
            }
        }

        private void HasTimeRunOut()
        {
            _hasTimerRunOut = _currentTime >= MaxTime;
        }

        private void OnDisable()
        {
            if (_catManager != null)
            {
                _catManager.CleanUp();
                _catManager.OnGeneratedSelectedCatToFind -= HandleOnGeneratedSelectedCatToFind;
            }

            if (_uiManager != null)
            {
                _uiManager.CleanUp();
            }
        }

        #region delegate
        private void HandleOnGeneratedSelectedCatToFind(CatBehaviour cat)
        {
            // TODO - update dossier
        }
        #endregion
    }
}
