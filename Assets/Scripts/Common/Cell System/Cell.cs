using System;
using System.Collections;
using System.Collections.Generic;
using Common.Cell_System;
using Common.Environment_System;
using Tools;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellSettings _whiteCellSettings;
    [SerializeField] private CellSettings _blackCellSettings;

    private SpriteRenderer _spriteRenderer;

    public ReactiveProperty<CellState> CellState { get; } = new();
    
    private CellCounter _cellCounter;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCellState(CellState cellState, CellCounter counter)
    {
        _cellCounter = counter;
        CellState.Value = cellState;
        
        ChangeCellState();
    }
    
    public void ChangeCellState()
    {
        switch (CellState.Value)
        {
            case global::CellState.Black:
                SetWhiteCell();
                break;
            case global::CellState.White:
                SetBlackCell();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _cellCounter.ChangeScore(CellState.Value);
    }

    private void SetBlackCell()
    {
        _spriteRenderer.color = _blackCellSettings.BaseColor;
        gameObject.layer = LayerMask.NameToLayer(_blackCellSettings.BaseLayerMask);
        CellState.Value = global::CellState.Black;
    }

    private void SetWhiteCell()
    {
        _spriteRenderer.color = _whiteCellSettings.BaseColor;
        gameObject.layer = LayerMask.NameToLayer(_whiteCellSettings.BaseLayerMask);
        CellState.Value = global::CellState.White;
    }
}

[Serializable]
public class CellSettings
{
    [field:SerializeField] public Color BaseColor { get; private set; }
    [field:SerializeField] public string BaseLayerMask { get; private set; }
}

public enum CellState
{
    White,
    Black
}