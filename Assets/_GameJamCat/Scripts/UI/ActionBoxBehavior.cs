using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GameJamCat
{
    public class ActionBoxBehavior : MonoBehaviour
    {
        private const string OpenLabel = "open";
        private const string CloseLabel = "close";
        //private const string InstructionalLabel = "Press {0} to {1} Dossier";

        [SerializeField] private ActionBoxView _actionMenuView = null;
        //[SerializeField] private TMP_Text _pressToOpenClose = null;

        //[Title("Properties")]
        //[SerializeField] private KeyCode _dossierButton = KeyCode.Tab;

        private bool _isActionBoxOpen = true;

        public event Action<bool> OnActionBoxStateChange;

        public bool IsActionBoxOpen
        {
            get => _isActionBoxOpen;
            set
            {
                _isActionBoxOpen = value;
                if (OnActionBoxStateChange != null)
                {
                    OnActionBoxStateChange(_isActionBoxOpen);
                }
            }
        }

        public void SetTargetCat(CatCustomisation catCustomization)
        {
            if (_actionMenuView != null)
            {
                _actionMenuView.SetNewCat(catCustomization);
            }
        }

        public void Initialize()
        {
            if (_actionMenuView != null)
            {
                _actionMenuView.Initialize();
                _actionMenuView.SetActionBoxOpen(_isActionBoxOpen);
            }

            TriggerOpenCloseText();
        }

        public void TriggerOpenCloseText()
        {
            var openOrClose = IsActionBoxOpen ? CloseLabel : OpenLabel;
            
        }

        public void UpdateActionBoxView()
        {
            if (_actionMenuView != null)
            {
                _actionMenuView.SetActionBoxOpen(_isActionBoxOpen);
            }
        }

        ///// <summary>
        ///// Set Instruction Label
        ///// </summary>
        ///// <param name="turnOn">turn on off label</param>
        //public void SetInstructionLabel(bool turnOn)
        //{
        //    if (_pressToOpenClose != null)
        //    {
        //        _pressToOpenClose.gameObject.SetActive(turnOn);
        //    }
        //}

        public void CleanUp()
        {

        }

        private void Update()
        {
            IsActionBoxOpen = (StateManager.Instance.GetState() == State.Dialogue);
            UpdateActionBoxView();
        }



    }
}

