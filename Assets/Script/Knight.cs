using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        // 上右
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);
        // 上左
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);
        // 右上
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);
        // 右下
        KnightMove(CurrentX + 2, CurrentY - 1, ref r);
        // 下右
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);
        // 下左
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);
        // 左上
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);
        // 左下
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;
    }

    /* ref
         * 参照渡しでdeep copy
     */
    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            c = BoardManager.Instance.Chessmans[x, y];
            if (c == null)
            {
                r[x, y] = true;
            }
            else if (isLightSide != c.isLightSide)
            {
                r[x, y] = true;
            }
        }
    }
}
