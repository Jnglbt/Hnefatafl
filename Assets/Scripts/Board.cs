using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Codes.Linus.IntVectors
{
    public enum CellState
    {
        None,
        Friendly,
        Enemy,
        Free,
        OutOfBounds,
        Throne
    }

    public class Board : MonoBehaviour
    {

        public GameObject mCellPrefab;
        public Sprite[] mSnow;
        public Sprite[] mEnemySnow;
        public Sprite mThrone;


        [HideInInspector]
        public Cell[,] mAllCells = new Cell[11, 11];

        public void Create()
        {
            #region Create

            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    GameObject newCell = Instantiate(mCellPrefab, transform);

                    RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                    mAllCells[x, y] = newCell.GetComponent<Cell>();
                    mAllCells[x, y].Setup(new Vector2i(x, y), this);
                }
            }

            #endregion
            #region Sprites
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    if ((y == 0 && x == 0) || (y == 0 && x == 10) || (y == 10 && x == 0) || (y == 10 && x == 10) || (y == 5 && x == 5))
                        mAllCells[x, y].GetComponent<Image>().sprite = mThrone;
                    else if ((y > 2 && y < 8 && x == 0) || (y > 2 && y < 8 && x == 10) || (x > 2 && x < 8 && y == 0) || (x > 2 && x < 8 && y == 10)
                        || (y == 1 && x == 5) || (y == 9 && x == 5) || (y == 5 && x == 1) || (y == 5 && x == 9))
                        mAllCells[x, y].GetComponent<Image>().sprite = mEnemySnow[Random.Range(0, mEnemySnow.Length)];
                    else
                        mAllCells[x, y].GetComponent<Image>().sprite = mSnow[Random.Range(0, mSnow.Length)];
                }
            }
            #endregion
        }

        public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
        {

            if (targetX < 0 || targetX > 10)
                return CellState.OutOfBounds;

            if (targetY < 0 || targetY > 10)
                return CellState.OutOfBounds;

            if ((targetY == 0 && targetX == 0) || (targetY == 0 && targetX == 10) || (targetY == 10 && targetX == 0) || (targetY == 10 && targetX == 10))// || (targetY == 5 && targetX == 5))
                return CellState.Throne;

            Cell targetCell = mAllCells[targetX, targetY];

            if (targetCell.mCurrentPiece != null)
            {
                if (checkingPiece.mColor == targetCell.mCurrentPiece.mColor)
                    return CellState.Friendly;

                if (checkingPiece.mColor != targetCell.mCurrentPiece.mColor)
                    return CellState.Enemy;
            }
            

            return CellState.Free;
        }
    }
}