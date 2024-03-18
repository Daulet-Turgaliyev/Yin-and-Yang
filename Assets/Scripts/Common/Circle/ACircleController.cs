using System;
using Common.Cell_System;
using Common.Containers.GameManagerServices;
using Common.Environment_System;
using Common.Sounds;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Common.Circle
{
    public abstract class ACircleController : MonoBehaviour
    {
        private ISettingsPanelService _settingsService;
        private ISoundManagerService _soundManagerService;
        private IGameManagerService _gameManagerService;

        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private float _speed;
    
        private Vector2 _movementDirection;

        private Rigidbody2D _rigidbody2D;

        [Inject]
        public void Constructor(
            ISettingsPanelService settingsService,
            ISoundManagerService soundManagerService,
            IGameManagerService gameManagerService)
        {
            _soundManagerService = soundManagerService;
            _settingsService = settingsService;
            _gameManagerService = gameManagerService;
        }
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            ChoseNewDirection();
        }

        private void Start()
        {
            ChangeSpeed(_gameManagerService.CurrentSpeed);
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _movementDirection * _speed;
            _rigidbody2D.angularVelocity = _speed * 2;
        }

        public void ChangeSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetSkin(Sprite sprite)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = sprite;
        }
        
        private void ChoseNewDirection()
        {
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;

            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            _movementDirection = new Vector2(x, y);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Cell cell))
            {
                cell.ChangeCellState();
            }
            
            if (other.gameObject.CompareTag("Wall"))
            {
                _soundManagerService.PlayRandomWallSound();
            }

            if (_settingsService.IsNeedVibration == true)
            {
                Vibration.Vibrate(30);
            }
            
            Vector2 normal = other.contacts[0].normal;
            _movementDirection = Vector2.Reflect(_movementDirection, normal).normalized;
            float randomAngle = Random.Range(-30, 30) * Mathf.Deg2Rad;
            _movementDirection = new Vector2(Mathf.Cos(randomAngle) * _movementDirection.x - Mathf.Sin(randomAngle) * _movementDirection.y,
                Mathf.Sin(randomAngle) * _movementDirection.x + Mathf.Cos(randomAngle) * _movementDirection.y).normalized;
        }
    }
}
