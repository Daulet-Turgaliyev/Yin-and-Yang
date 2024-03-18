using System;
using Tools;
using UnityEngine;

namespace Common.Cell_System
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private string _whiteCellLayerString;
        [SerializeField] private string _blackCellLayerString;

        private SpriteRenderer _spriteRenderer;

        private Sprite _whiteSprite;
        private Sprite _blackSprite;
        
        public ReactiveProperty<CellState> cellState { get; } = new();
    
        private CellCounter _cellCounter;
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Sprite whiteSprite, Sprite blackSprite, int orderInLayer)
        {
            _spriteRenderer.sortingOrder = orderInLayer;
            _whiteSprite = whiteSprite;
            _blackSprite = blackSprite;
        }
        
        public void SetCellState(CellState cellState, CellCounter counter)
        {
            _cellCounter = counter;
            this.cellState.Value = cellState;
        
            ChangeCellState();
        }
    
        public void ChangeCellState()
        {
            switch (cellState.Value)
            {
                case CellState.Black:
                    SetWhiteCell();
                    break;
                case CellState.White:
                    SetBlackCell();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
            _cellCounter.ChangeScore(cellState.Value);
        }

        private void SetBlackCell()
        {
            gameObject.layer = LayerMask.NameToLayer(_blackCellLayerString);
            cellState.Value = CellState.Black;
            _spriteRenderer.sprite = _blackSprite;
        }

        private void SetWhiteCell()
        {
            gameObject.layer = LayerMask.NameToLayer(_whiteCellLayerString);
            cellState.Value = CellState.White;
            _spriteRenderer.sprite = _whiteSprite;
        }
    }



    public enum CellState
    {
        White,
        Black
    }
}