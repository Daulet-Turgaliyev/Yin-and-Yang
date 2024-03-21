using System;
using System.Collections;
using Common.Cell_System;
using Common.Containers.GameManagerServices;
using Common.Data;
using Common.Environment_System;
using Common.Sounds;
using Cysharp.Threading.Tasks;
using Tools;
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

        private RotationMode _rotationMode;
    
        private Vector2 _movementDirection;

        private Rigidbody2D _rigidbody2D;

        private Vector2 _bottomLeft;
        private Vector2 _topRight;
        
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
            _bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            _topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            
            _bottomLeft -= Vector2.one;
            _topRight += Vector2.one;
        }

        private void Update()
        {
            Vector2 position = transform.position;

            if (position.x < _bottomLeft.x || position.x > _topRight.x || position.y < _bottomLeft.y || position.y > _topRight.y)
            {
                transform.position = new Vector2((_bottomLeft.x + _topRight.x) / 2, (_bottomLeft.y + _topRight.y) / 2);
            }
        }


        public void Initialize(SoundPackPreset soundPackPreset, Sprite sprite)
        {
            SetSkin(sprite);
            
            ChangeSpeed(_gameManagerService.CurrentSpeed);
            _rotationMode = soundPackPreset.RotationMode;

            ChoseNewDirection();
            
            switch (_rotationMode)
            {
                case RotationMode.Random:
                    StartCoroutine(RotationRandom());
                    break;
                case RotationMode.Direction:
                    StartCoroutine(RotationDirection());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator RotationRandom()
        {
            while (true)
            {
                _rigidbody2D.angularVelocity = _speed * 2;
                _rigidbody2D.velocity = _movementDirection * _speed;
                yield return Yielders.FixedUpdate;
            }
        }
        
        
        private IEnumerator RotationDirection()
        {
            while (gameObject.GetCancellationTokenOnDestroy().IsCancellationRequested == false)
            {
                _rigidbody2D.velocity = _movementDirection * _speed;


                if (_rigidbody2D.velocity != Vector2.zero)
                {
                    float angle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                
                yield return Yielders.FixedUpdate;
            }
        }

        public void ChangeSpeed(float speed)
        {
            _speed = speed;
        }

        private void SetSkin(Sprite sprite)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
                Vibration.Vibrate(10);
            }
            
            Vector2 normal = other.contacts[0].normal;
            _movementDirection = Vector2.Reflect(_movementDirection, normal).normalized;
            float randomAngle = Random.Range(-30, 30) * Mathf.Deg2Rad;
            _movementDirection = new Vector2(Mathf.Cos(randomAngle) * _movementDirection.x - Mathf.Sin(randomAngle) * _movementDirection.y,
                Mathf.Sin(randomAngle) * _movementDirection.x + Mathf.Cos(randomAngle) * _movementDirection.y).normalized;
        }
    }
}

public enum RotationMode
{
    Random,
    Direction
}