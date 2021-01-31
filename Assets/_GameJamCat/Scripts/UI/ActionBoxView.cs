using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamCat
{
    public class ActionBoxView : MonoBehaviour
    {
        private const float AnimationDuration = 0.5f;
        private float _animationActionBoxDistance = 0;

        [Title("CatNameBox")]
        [SerializeField]
        private Image _catNameBox = null;

        [Title("CatName")]
        [SerializeField]
        private TMP_Text _catName = null;

        [Title("ActionBox")]
        [SerializeField]
        private Image _actionBoard = null;

        [Title("ChatButton")]
        [SerializeField]
        private Image _chatButton = null;
        
        [Title("ClaimButton")]
        [SerializeField]
        private Image _claimButton = null;

        [Title("ClaimText")]
        [SerializeField]
        private TMP_Text _claimText = null;

        [Title("ChatText")]
        [SerializeField]
        private TMP_Text _chatText = null;

        //[Title("Placeholder Values")]
        //[SerializeField] private Texture2D _defaultTex;

        //[Title("Poster Cat Image")]
        //[SerializeField] private RawImage _catImage = null;
        //[Title("Name")]
        //[SerializeField] private TMP_Text _catName = null;
        //[Title("Likes")]
        //[SerializeField] private Image _catLikesImage = null;
        //[SerializeField] private TMP_Text _catLikes = null;
        //[Title("Cativities")]
        //[SerializeField] private Image _catActivitiesImage = null;
        //[SerializeField] private TMP_Text _cativities = null;

        //[Title("Sprites")]
        //[SerializeField] Sprite _tunaIcon;
        //[SerializeField] Sprite _salmonIcon, _chickenIcon, _pumpkinIcon, _dryKibbleIcon, _wetKibbleIcon, _kibbleIcon, _boneIcon, _beefIcon = null;
        //[SerializeField] Sprite _yarnBallsIcon, _cardboardBoxIcon, _fishingRodIcon, _catnipSackIcon, _hijinksIcon, _scratchingPostIcon, _laserIcon, _tennisBall = null;

        //private float _animationDossierDistance = 0;

        //Tentative Use Case from UI Manager
        public void SetNewCat(CatCustomisation catCustomisation)
        {        
            SetName(catCustomisation._catName);
        }

        public void Initialize()
        {
            var rectTransform = GetComponent<RectTransform>();
            // Top
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -_animationActionBoxDistance);
            // Bottom
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -_animationActionBoxDistance);
        }

        public void SetActionBoxOpen(bool isCurrentlyOpen)
        {
            var moveDirection = isCurrentlyOpen ? Vector3.up : Vector3.down;
            transform.DOBlendableLocalMoveBy(moveDirection * _animationActionBoxDistance, AnimationDuration, true);
        }

        private void SetName(string catName)
        {
            if (_catName != null)
            {
                _catName.text = catName;
            }
        }

        //private void SetUIElement(TMP_Text element, string label)
        //{
        //    if (element != null && !string.IsNullOrEmpty(label))
        //    {
        //        element.text = string.Format(ActionBoardText, label);
        //    }
        //}

        private void SetUIElement(RawImage element, Texture2D image)
        {
            if (element != null && image != null)
            {
                element.texture = image;
            }
        }

        private void SetUIElement(Image element, Sprite image)
        {
            if (element != null && image != null)
            {
                element.sprite = image;
            }
        }

        private void Awake()
        {
            _animationActionBoxDistance = Screen.height * 0.9f;
        }
    }
}
