using UnityEngine;

namespace Common.Data
{
    [CreateAssetMenu(fileName = "initializeGameObjects", menuName = "Create InitializeGameObjects", order = 0)]
    public class InitializeGameObjects : ScriptableObject
    {
        [field: SerializeField] public GameObject[] NeedInitializeObjects { get; private set; }
    }
}