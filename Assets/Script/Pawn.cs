using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        // 1セル移動用変数
        Chessman c;
        // 2セル移動用変数
        Chessman c2;

        int[] e = BoardManager.Instance.EnPassantMove;

        // LightSide
        if (isLightSide)
        {
            if (CurrentX != 0 && CurrentY != 7)
            {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY + 1)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isLightSide)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            if (CurrentX != 7 && CurrentY != 7)
            {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY + 1)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isLightSide)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            if (CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            // スタート地点の場合
            if (CurrentY == 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
        }

        // DarkSide
        else
        {
            if (CurrentX != 0 && CurrentY != 7)
            {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isLightSide)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            if (CurrentX != 7 && CurrentY != 0)
            {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isLightSide)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            // スタート地点の場合
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }
        }

        return r;
    }
}
