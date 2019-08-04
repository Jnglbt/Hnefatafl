using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codes.Linus.IntVectors
{
    public class EnemyViking : BasePiece
    {
        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);

            mMovement = new Vector3i(10, 10, 0);
        }
    }
}
