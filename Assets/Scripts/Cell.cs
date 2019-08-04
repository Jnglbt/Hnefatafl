using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Codes.Linus.IntVectors
{
    public class Cell : MonoBehaviour
    {

        public Image mOutlineImage;

        [HideInInspector]
        public Vector2i mBoardPosition = Vector2i.zero;
        [HideInInspector]
        public Board mBoard = null;
        [HideInInspector]
        public RectTransform mRectTransform = null;
        [HideInInspector]
        public BasePiece mCurrentPiece = null;
        [HideInInspector]
        public BasePiece mTargetPiece = null;

        public void Setup(Vector2i newBoardPosition, Board newBoard)
        {
            mBoardPosition = newBoardPosition;
            mBoard = newBoard;

            mRectTransform = GetComponent<RectTransform>();
        }

        public void RemovePiece()
        {
            if (mCurrentPiece != null)
            {
                mCurrentPiece.Kill();
            }
        }
    }
}
