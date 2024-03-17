using TMPro;
using UnityEngine;

namespace Common.Cell_System
{
    public class CellCounter : MonoBehaviour
    {
        private int _whiteCount;
        private int _blackCount;

        [SerializeField] private TextMeshProUGUI _blackText;
        [SerializeField] private TextMeshProUGUI _whiteText;

        public void AddWhite()
        {
            _whiteCount++;
        }
    
        public void AddBlack()
        {
            _blackCount++;
        }

        public void UpdateTexts()
        {
            _blackText.text = _whiteCount.ToString();
            _whiteText.text = _blackCount.ToString();
        }

        public void ChangeScore(CellState cellState)
        {
            if (cellState == CellState.Black)
            {
                _whiteCount--;
                _blackCount++;
            }
            else
            {
                _whiteCount++;
                _blackCount--;
            }

            UpdateTexts();
        }
    }
}
