using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameJamCat
{
    public class PregameDialogueBoxBehaviour : MonoBehaviour
    {
        private const float DelayBeforeShowingDialogueBox = 1f;
        private const float ScaleAnimationDuration = 1f;
        private const float DurationOfReadTimeAfterAnimationCompletion = 3f;
        private const string PregameMessage =
            "... Thanks again detective for helping us. We couldn’t find the cat all day, and there’s only an hour left until the adoption... Please help us find it!";
     
        
        public event Action OnDialogueCompleted;
        
        [SerializeField] private DialogueBoxBehaviour _dialogueBoxBehaviour = null;

        private Transform _dialogueBehaviourContainer = null;

        public void StartAnimation()
        {
            _dialogueBehaviourContainer.DOScale(Vector3.one, ScaleAnimationDuration).SetEase(Ease.OutBack).SetDelay(DelayBeforeShowingDialogueBox).OnComplete(() =>
            {
                _dialogueBoxBehaviour.ReadText(PregameMessage);
            });
        }

        public void Initialize()
        {
            _dialogueBoxBehaviour.text = string.Empty;
        }

        private void HandleOnReadComplete()
        {
            StartCoroutine(WaitForSecondsBeforeFadingIn(DurationOfReadTimeAfterAnimationCompletion));
        }

        private void Awake()
        {
            if (_dialogueBoxBehaviour != null)
            {
                _dialogueBoxBehaviour.OnReadCompleted += HandleOnReadComplete;
                _dialogueBehaviourContainer = _dialogueBoxBehaviour.transform.parent;
                _dialogueBehaviourContainer.localScale = Vector3.zero;
            }
        }

        private void OnDestroy()
        {
            if (_dialogueBoxBehaviour != null)
            {
                _dialogueBoxBehaviour.OnReadCompleted -= HandleOnReadComplete;
            }
        }

        private IEnumerator WaitForSecondsBeforeFadingIn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _dialogueBehaviourContainer.DOScale(Vector3.zero, ScaleAnimationDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                if (OnDialogueCompleted != null)
                {
                    OnDialogueCompleted();
                }
            });
        }
    }
}
