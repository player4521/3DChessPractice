using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int x, y;

        // 右移動
        x = CurrentX;
        while (true)
        {
            x++;
            if (x >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[x, CurrentY];
            if (c == null)
            {
                r[x, CurrentY] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[x, CurrentY] = true;

                    break;
                }
            }
        }

        // 左移動
        x = CurrentX;
        while (true)
        {
            x--;
            if (x < 0)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[x, CurrentY];
            if (c == null)
            {
                r[x, CurrentY] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[x, CurrentY] = true;

                    break;
                }
            }
        }

        // 上移動
        x = CurrentY;
        while (true)
        {
            x++;
            if (x >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, x];
            if (c == null)
            {
                r[CurrentX, x] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[CurrentX, x] = true;

                    break;
                }
            }
        }

        // 下移動
        x = CurrentY;
        while (true)
        {
            x--;
            if (x < 0)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, x];
            if (c == null)
            {
                r[CurrentX, x] = true;
            }
            else
            {
                if (c.isLightSide != isLightSide)
                {
                    r[CurrentX, x] = true;

                    break;
                }
            }
        }

        x = CurrentX;
        y = CurrentY;
        // 上右
        while (true)
        {
            x++;
            y++;
            if (x >= 8 || y >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[x, y];

            if (c == null)
            {
                r[x, y] = true;
            }
            else
            {
                if (isLightSide != c.isLightSide)
                {
                    r[x, y] = true;
                    break;
                }
            }
        }

        x = CurrentX;
        y = CurrentY;
        // 上左
        while (true)
        {
            x--;
            y++;
            if (x < 0 || y >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[x, y];

            if (c == null)
            {
                r[x, y] = true;
            }
            else
            {
                if (isLightSide != c.isLightSide)
                {
                    r[x, y] = true;
                    break;
                }
            }
        }

        x = CurrentX;
        y = CurrentY;
        // 下右
        while (true)
        {
            x++;
            y--;
            if (x >= 8 || y < 0)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[x, y];

            if (c == null)
            {
                r[x, y] = true;
            }
            else
            {
                if (isLightSide != c.isLightSide)
                {
                    r[x, y] = true;
                    break;
                }
            }
        }

        x = CurrentX;
        y = CurrentY;
        // 下左
        while (true)
        {
            x--;
            y--;
            if (x < 0 || y < 0)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[x, y];

            if (c == null)
            {
                r[x, y] = true;
            }
            else
            {
                if (isLightSide != c.isLightSide)
                {
                    r[x, y] = true;
                    break;
                }
            }
        }

        return r;
    }
}
