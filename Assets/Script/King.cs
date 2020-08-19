using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int x, y;

        // Top side
        x = CurrentX - 1;
        y = CurrentY + 1;
        if (CurrentY != 7)
        {
            // Kingの上の3面を確認
            for (int i = 0; i < 3; i++)
            {
                if (x >= 0 || x < 8)
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

                x++;
            }
        }

        // Down side
        x = CurrentX - 1;
        y = CurrentY - 1;
        if (CurrentY != 0)
        {
            // Kingの下の3面を確認
            for (int i = 0; i < 3; i++)
            {
                if (x >= 0 || x < 8)
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

                x++;
            }
        }

        // Kingの右を確認
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
            {
                r[CurrentX + 1, CurrentY] = true;
            }
            else if (isLightSide != c.isLightSide)
            {
                r[CurrentX + 1, CurrentY] = true;
            }
        }

        // Kingの左を確認
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
            {
                r[CurrentX - 1, CurrentY] = true;
            }
            else if (isLightSide != c.isLightSide)
            {
                r[CurrentX - 1, CurrentY] = true;
            }
        }

        return r;
    }
}
