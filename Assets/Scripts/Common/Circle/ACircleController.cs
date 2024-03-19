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

        public void Initialize(SoundPackPreset soundPackPreset, Sprite sprite, TrailRenderer trailRendererPrefab)
        {
            SetSkin(sprite);
            
            ChangeSpeed(_gameManagerService.CurrentSpeed);
            _rotationMode = soundPackPreset.RotationMode;

            ChoseNewDirection();
            
            Instantiate(trailRendererPrefab.gameObject, transform);
            
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