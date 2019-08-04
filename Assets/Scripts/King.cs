using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codes.Linus.IntVectors
{
    public class King : BasePiece
    {
        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);

            mMovement = new Vector3i(10, 10, 0);
        }

        public override void Kill()
        {
            base.Kill();

            mPieceManager.mIsKingAlive = false;
        }

        protected override void Move()
        {
            base.Move();

            CheckForWin();
        }

        private void CheckForWin()
        {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;

            CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);

            if (cellState == CellState.Throne)
            {
                print("Win");
                mPieceManager.ResetPieces();
            }
        }
    }
}