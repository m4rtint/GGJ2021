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
        
        //TimeManagement
        private const float MaxTime = 120f;
        private float _currentTime = 0f;

        private void OnEnable()
        {
            if (_catManager != null)
            {
                _catManager.Initialize();
                _catManager.OnGeneratedSelectedCatToFind += HandleOnGeneratedSelectedCatToFind;
            }

            if (_uiManager != null)
            {
                _uiManager.Initialize(MaxTime);
            }
        }

        private void Start()
        {
            _stateManager.SetState(State.Pregame);
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_uiManager != null)
            {
                _uiManager.UpdateTimer(_currentTime);
            }
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
