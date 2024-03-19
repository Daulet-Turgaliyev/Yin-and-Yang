using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "soundPackPreset", menuName = "Sound Pack/Sound Pack Preset", order = 0)]
    public class SoundPackPreset : ScriptableObject
    {
        [field: SerializeField] public string SoundPackName { get; private set; }
        [field: SerializeField] public Sprite SoundLogo { get; private set; }
        
        [field: SerializeField] public float CircleSize { get; private set; }
        [field: SerializeField] public float CellSize { get; private set; }
        [field: SerializeField] public RotationMode RotationMode { get; private set; }
        
        [field: SerializeField] public Sprite WhiteCircleSkin { get; private set; }
        [field: SerializeField] public Sprite BlackCircleSkin { get; private set; }
        
        [field: SerializeField] public Sprite[] WhiteCellSkin { get; private set; }
        [field: SerializeField] public Sprite[] BlackCellSkin { get; private set; }
        
        [field: SerializeField] public SoundPackData SoundPackData { get; private set; }
        
        [field: SerializeField] public TrailRenderer WhiteTrailPrefabs { get; private set; }
        [field: SerializeField] public TrailRenderer BlackTrailPrefabs { get; private set; }

        public Sprite GetRandomWhiteCellSkin()
        {
            int randomIndex = Random.Range(0, WhiteCellSkin.Length);
            return WhiteCellSkin[randomIndex];
        }
        public Sprite GetRandomBlackCellSkin()
        {
            int randomIndex = Random.Range(0, BlackCellSkin.Length);
            return BlackCellSkin[randomIndex];
        }
    }
}