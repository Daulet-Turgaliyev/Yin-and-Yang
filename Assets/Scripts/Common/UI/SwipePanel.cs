using Common.Containers.GameManagerServices;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Common.UI
{
    public class SwipePanel : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _mainSettingsPanel; 
        
        private Vector2 _startMainStingsPanel;
        [SerializeField] private Vector2 _endMainStingsPanel;

        private IGameManagerService _gameManagerService;

        [SerializeField] private float _speedFactor;

        private bool _isSettingsPanelOpened;
        
        [SerializeField]
        private float _animationDuration = 0.5f;

        [Inject]
        public void Construct(IGameManagerService gameManagerService)
        {
            _gameManagerService = gameManagerService;
        }
        
        private void Start()
        {
            _startMainStingsPanel = _mainSettingsPanel.anchoredPosition;
        }

        public void CloseMainSettingsPanel()
        {
            _isSettingsPanelOpened = false;
            _mainSettingsPanel.DOKill();
            _mainSettingsPanel.DOAnchorPos(_startMainStingsPanel, _animationDuration).SetEase(Ease.Linear);
        }
        
        
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;

        [SerializeField]
        private bool detectSwipeOnlyAfterRelease = false;

        [SerializeField]
        private float minDistanceForSwipe = 20f;

        private void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUpPosition = touch.position;
                    fingerDownPosition = touch.position;
                }

                if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                {
                    if(_isSettingsPanelOpened == true) return;
                    float delta = (touch.position.normalized.x - fingerUpPosition.normalized.x) * _speedFactor;
                    _gameManagerService.ChangeUpSpeed(delta);
                    
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe()
        {
            if (SwipeDistanceCheckMet())
            {
                if (IsVerticalSwipe() && VerticalMovementDistance() > 65 == true)
                {
                    if (fingerDownPosition.y - fingerUpPosition.y < 0)
                    {
                        _mainSettingsPanel.DOAnchorPos(_endMainStingsPanel, _animationDuration);
                        _isSettingsPanelOpened = true;
                    }
                }
                
                fingerUpPosition = fingerDownPosition;
            }
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMovementDistance() > minDistanceForSwipe;
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }
        
        private bool IsHorizontalSwipe()
        {
            return VerticalMovementDistance() < HorizontalMovementDistance();
        }

        private float VerticalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
        }

    }
}