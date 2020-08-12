using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i;

        // 右移動
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[i, CurrentY] = true;

                    break;
                }
            }
        }

        // 左移動
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, CurrentY];
            if (c == null)
            {
                r[i, CurrentY] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[i, CurrentY] = true;

                    break;
                }
            }
        }

        // 上移動
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[CurrentX, i] = true;

                    break;
                }
            }
        }

        // 下移動
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, i];
            if (c == null)
            {
                r[CurrentX, i] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[CurrentX, i] = true;

                    break;
                }
            }
        }

        return r;
    }
}
