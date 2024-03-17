using UnityEngine;

namespace Common.Environment_System
{
    public class SpriteScaler : MonoBehaviour
    {
        private void Start()
        {
            AdjustSize();
        }

        private void AdjustSize()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            if (rectTransform == null)
            {
                Debug.LogError("FitToScreen script requires a RectTransform component.");
                return;
            }

            // Получаем размер экрана
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Определяем новый размер для RectTransform, чтобы он соответствовал размеру экрана
            rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);

            // Дополнительно, можно настроить позицию, если требуется
            rectTransform.anchoredPosition = Vector2.zero;
        }

        // Опционально, если требуется подгонять размер при изменении размеров экрана
        private void Update()
        {
            AdjustSize();
        }
    }
}