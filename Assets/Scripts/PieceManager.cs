using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codes.Linus.IntVectors
{
    public class PieceManager : MonoBehaviour
    {
        [HideInInspector]
        public bool mIsKingAlive = true;

        public GameObject mPiecePrefab;

        private List<BasePiece> mWhitePieces = null;
        private List<BasePiece> mBlackPieces = null;
        private List<BasePiece> mKingPiece = null;

        public GameObject mTurnWhite;
        public GameObject mTurnBlack;

        /*private string[] mWhiteOrder = new string[13]
        {
                        "V",
                   "V", "V", "V",
              "V", "V", "K", "V", "V",
                   "V", "V", "V",
                        "V"
        };*/

        private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"V", typeof(Viking)},
        {"E", typeof(EnemyViking)},
        {"K", typeof(King)}
    };

        public void Setup(Board board)
        {
            mWhitePieces = CreatePieces(12, Color.white, new Color32(80, 124, 159, 255), board);
            mBlackPieces = CreatePieces(24, Color.black, new Color32(210, 95, 64, 255), board);
            mKingPiece = CreatePieces(1, Color.white, new Color32(212, 175, 55, 255), board);

            PlacePieces(12, mWhitePieces, board);
            PlacePieces(24, mBlackPieces, board);
            PlacePieces(1, mKingPiece, board);

            SwitchSides(Color.black);
        }

        private List<BasePiece> CreatePieces(int count, Color teamColor, Color32 spriteColor, Board board)
        {
            List<BasePiece> newPieces = new List<BasePiece>();
            if (count == 1)
            {
                GameObject newPieceObject = Instantiate(mPiecePrefab);
                newPieceObject.transform.SetParent(transform);

                newPieceObject.transform.localScale = new Vector3(1, 1, 1);
                newPieceObject.transform.localRotation = Quaternion.identity;

                Type pieceType = mPieceLibrary["K"];

                BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);
                newPieces.Add(newPiece);

                newPiece.Setup(teamColor, spriteColor, this);
            }
            else if (count == 12)
            {
                for (int i = 0; i < 12; i++)
                {
                    GameObject newPieceObject = Instantiate(mPiecePrefab);
                    newPieceObject.transform.SetParent(transform);

                    newPieceObject.transform.localScale = new Vector3(1, 1, 1);
                    newPieceObject.transform.localRotation = Quaternion.identity;

                    Type pieceType = mPieceLibrary["V"];

                    BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);
                    newPieces.Add(newPiece);

                    newPiece.Setup(teamColor, spriteColor, this);
                }
            }
            else if (count == 24)
            {
                for (int i = 0; i < 24; i++)
                {
                    GameObject newPieceObject = Instantiate(mPiecePrefab);
                    newPieceObject.transform.SetParent(transform);

                    newPieceObject.transform.localScale = new Vector3(1, 1, 1);
                    newPieceObject.transform.localRotation = Quaternion.identity;

                    Type pieceType = mPieceLibrary["E"];

                    BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);
                    newPieces.Add(newPiece);

                    newPiece.Setup(teamColor, spriteColor, this);
                }
            }
            return newPieces;
        }

        private void PlacePieces(int count, List<BasePiece> pieces, Board board)
        {
            int i = 0;
            if (count == 1)
            {
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        if ((x == 5 && y == 5))
                        {
                            pieces[i].Place(board.mAllCells[x, y]);
                            i++;
                        }

                    }
                }
            }
            if (count == 12)
            {
                //int i = 0;
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        if ((x == 3 && y == 5) || (x == 4 && y == 4) || (x == 4 && y == 5) || (x == 4 && y == 6) || (x == 5 && y == 3) || (x == 5 && y == 4)
                            || (x == 5 && y == 6) || (x == 5 && y == 7) || (x == 6 && y == 4) || (x == 6 && y == 5) || (x == 6 && y == 6) || (x == 7 && y == 5))
                        {
                            pieces[i].Place(board.mAllCells[x, y]);
                            i++;
                        }

                    }
                }
            }
            if (count == 24)
            {
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        if ((x == 3 && y == 0) || (x == 4 && y == 0) || (x == 5 && y == 0) || (x == 6 && y == 0) || (x == 7 && y == 0) || (x == 5 && y == 1)
                            || (x == 3 && y == 10) || (x == 4 && y == 10) || (x == 5 && y == 10) || (x == 6 && y == 10) || (x == 7 && y == 10) || (x == 5 && y == 9)
                            || (y == 3 && x == 0) || (y == 4 && x == 0) || (y == 5 && x == 0) || (y == 6 && x == 0) || (y == 7 && x == 0) || (y == 5 && x == 1)
                            || (y == 3 && x == 10) || (y == 4 && x == 10) || (y == 5 && x == 10) || (y == 6 && x == 10) || (y == 7 && x == 10) || (y == 5 && x == 9))
                        {
                            pieces[i].Place(board.mAllCells[x, y]);
                            i++;
                        }
                    }
                }
            }
        }

        private void SetInteractive(List<BasePiece> allPieces, bool value)
        {
            foreach (BasePiece piece in allPieces)
                piece.enabled = value;
        }

        public void SwitchSides(Color color)
        {
            if (!mIsKingAlive)
            {
                ResetPieces();

                mIsKingAlive = true;

                color = Color.black;
            }

            bool isBlackTurn = color == Color.white ? true : false;

            if (isBlackTurn)
            {
                mTurnWhite.SetActive(false);
                mTurnBlack.SetActive(true);
            }
            else
            {
                mTurnWhite.SetActive(true);
                mTurnBlack.SetActive(false);
            }

            SetInteractive(mWhitePieces, !isBlackTurn);
            SetInteractive(mKingPiece, !isBlackTurn);
            SetInteractive(mBlackPieces, isBlackTurn);
        }

        public void ResetPieces()
        {
            foreach (BasePiece piece in mWhitePieces)
                piece.Reset();

            foreach (BasePiece piece in mBlackPieces)
                piece.Reset();

            foreach (BasePiece piece in mKingPiece)
                piece.Reset();
        }
        
    }
}