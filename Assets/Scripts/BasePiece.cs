using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Codes.Linus.IntVectors
{

    public class BasePiece : EventTrigger
    {

        [HideInInspector]
        public Color mColor = Color.clear;

        protected Cell mOriginalCell = null;
        protected Cell mCurrentCell = null;

        protected RectTransform mRectTransform = null;
        protected PieceManager mPieceManager;

        protected Cell mTargetCell = null;

        protected Vector3i mMovement = Vector3i.one;
        protected List<Cell> mHighlightedCells = new List<Cell>();

        public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            mPieceManager = newPieceManager;

            mColor = newTeamColor;
            GetComponent<Image>().color = newSpriteColor;
            mRectTransform = GetComponent<RectTransform>();
        }

        public void Place(Cell newCell)
        {
            mCurrentCell = newCell;
            mOriginalCell = newCell;
            mCurrentCell.mCurrentPiece = this;

            transform.position = newCell.transform.position;
            gameObject.SetActive(true);
        }

        public virtual void Reset()
        {
            Kill();

            Place(mOriginalCell);
        }

        public virtual void Kill()
        {
            mCurrentCell.mCurrentPiece = null;

            gameObject.SetActive(false);
        }

        public void CheckForTarget()
        {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;
            
            if (currentX + 2 < 10 && mCurrentCell.mBoard.mAllCells[currentX + 2, currentY].mCurrentPiece != null)
            {
                //mTargetCell.RemovePiece();

                print("OK");
                CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX + 2, currentY, this);
                if (cellState == CellState.Friendly || cellState == CellState.Throne)
                {
                    CellState cellState2 = mCurrentCell.mBoard.ValidateCell(currentX + 1, currentY, this);
                    if (cellState2 == CellState.Enemy)
                        mCurrentCell.mBoard.mAllCells[mCurrentCell.mBoardPosition.x + 1, currentY].RemovePiece();
                }
            }

            if (currentX - 2 > 0 && mTargetCell.mBoard.mAllCells[currentX - 2, currentY] != null)
            {
                print("OK");
                CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX - 2, currentY, this);
                if (cellState == CellState.Friendly || cellState == CellState.Throne)
                {
                    CellState cellState2 = mCurrentCell.mBoard.ValidateCell(currentX - 1, currentY, this);
                    if (cellState2 == CellState.Enemy)
                        mCurrentCell.mBoard.mAllCells[mCurrentCell.mBoardPosition.x - 1, currentY].RemovePiece();
                }
            }

            if (currentY + 2 < 10 && mTargetCell.mBoard.mAllCells[currentX, currentY + 2] != null)
            {
                print("OK");
                CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY + 2, this);
                if (cellState == CellState.Friendly || cellState == CellState.Throne)
                {
                    CellState cellState2 = mCurrentCell.mBoard.ValidateCell(currentX, currentY + 1, this);
                    if (cellState2 == CellState.Enemy)
                        mCurrentCell.mBoard.mAllCells[mCurrentCell.mBoardPosition.x, currentY + 1].RemovePiece();
                }
            }

            if (currentY - 2 > 0 && mTargetCell.mBoard.mAllCells[currentX, currentY - 2] != null)
            {
                print("OK");
                CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY - 2, this);
                if (cellState == CellState.Friendly || cellState == CellState.Throne)
                {
                    CellState cellState2 = mCurrentCell.mBoard.ValidateCell(currentX, currentY - 1, this);
                    if (cellState2 == CellState.Enemy)
                        mCurrentCell.mBoard.mAllCells[mCurrentCell.mBoardPosition.x, currentY - 1].RemovePiece();
                }
            }
        }

        #region Movement
        private void CreateCellPath(int xDirection, int yDirection, int movement)
        {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;

            for (int i = 1; i <= movement; i++)
            {
                currentX += xDirection;
                currentY += yDirection;

                CellState cellState = CellState.None;
                cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);

                if ((this.GetType() == typeof(King) && cellState == CellState.Throne))
                {
                    mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                    break;
                }

                if (cellState == CellState.Enemy || cellState != CellState.Free || cellState == CellState.Throne)
                    break;
                

                mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
            }
        }

        protected virtual void CheckPathing()
        {
            // Horizontal
            CreateCellPath(1, 0, (int)mMovement.x);
            CreateCellPath(-1, 0, (int)mMovement.x);

            // Vertical
            CreateCellPath(0, 1, (int)mMovement.y);
            CreateCellPath(0, -1, (int)mMovement.y);

            // Upper diagonal
            CreateCellPath(1, 1, (int)mMovement.z);
            CreateCellPath(-1, 1, (int)mMovement.z);

            // Lower diagonal
            CreateCellPath(-1, -1, (int)mMovement.z);
            CreateCellPath(1, -1, (int)mMovement.z);
        }

        protected void ShowCells()
        {
            foreach (Cell cell in mHighlightedCells)
                cell.mOutlineImage.enabled = true;
        }

        protected void ClearCells()
        {
            foreach (Cell cell in mHighlightedCells)
                cell.mOutlineImage.enabled = false;

            mHighlightedCells.Clear();
        }

        protected virtual void Move()
        {
            //mTargetCell.RemovePiece();
            

            mCurrentCell.mCurrentPiece = null;

            mCurrentCell = mTargetCell;
            mCurrentCell.mCurrentPiece = this;

            transform.position = mCurrentCell.transform.position;
            //mCurrentCell.mBoard.mAllCells[mCurrentCell.mBoardPosition.x + 2, mCurrentCell.mBoardPosition.y].RemovePiece();
            CheckForTarget();
            mTargetCell = null;
        }
        #endregion

        #region Events
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            CheckPathing();

            ShowCells();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            transform.position += (Vector3)eventData.delta;

            foreach (Cell cell in mHighlightedCells)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
                {
                    mTargetCell = cell;
                    break;
                }

                mTargetCell = null;
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            
            ClearCells();

            if (!mTargetCell)
            {
                transform.position = mCurrentCell.gameObject.transform.position;
                return;
            }

            Move();

            mPieceManager.SwitchSides(mColor);
        }
        #endregion
    }
}