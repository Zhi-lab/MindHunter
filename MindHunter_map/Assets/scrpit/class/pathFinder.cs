using UnityEngine;
using UnityEditor;

public class PathFinder
{
    MapConfig mapConfig;
    public int[,] f;
    int rowsize, colsize;
    public PathFinder()
    {
        rowsize = 4;
        colsize = 4;
    }
    public void init()
    {
        f = new int[16, 16];

        for (int i = 0; i < 16; i++)
        {

            for (int j = 0; j < 16; j++)
            {
                f[i, j] = -1;
            }
        }

        for (int i = 0; i < 16; i++)
        {
            if (i + 4 < 16)
            {
                f[i, i + 4] = f[i + 4, i] = 1;
            }

            if (i % 4 != 3 && i < 15)
            {
                f[i, i + 1] = f[i + 1, i] = 1;
            }
        }
        f[1, 0] = f[2, 1] = f[6, 2] = f[9, 5] = f[12, 8] = f[7, 6] = f[10, 11] = -1;
        f[0, 1] = f[1, 2] = f[2, 6] = f[5, 9] = f[8, 12] = f[6, 7] = f[11, 10] = -1;
        for (var k = 0; k < 16; k++)
        {
            for (int i = 0; i < 16; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    if (i != j && f[k, j] > 0 && f[i, k] > 0 && (f[k, j] + f[i, k] < f[i, j] || f[i, j] == -1))
                    {
                        f[i, j] = f[k, j] + f[i, k];
                    }
                }
            }
        }
        Debug.Log("pathFinder success");
        Debug.Log(GoToward(new Vector2Int(0, 0), new Vector2Int(0, 1)));
    }
    public int GoToward(Vector2Int fromRoom, Vector2Int toRoom)
    {
        int from = fromRoom.y * rowsize + fromRoom.x, to = toRoom.y * rowsize + toRoom.y;
        if (from == to)
            return from;
        if (f[from, to] == -1)
            return -1;
        if (from - rowsize >= 0 && f[from, from - rowsize] == 1 && 1 + f[from - rowsize, to] == f[from, to])
            return from - rowsize;
        if (from % rowsize != 0 && from > 1 && f[from, from - 1] == 1 && 1 + f[from - 1, to] == f[from, to])
            return from - 1;
        if (from + 1 % rowsize != 0 && from + 1 < rowsize * colsize && f[from, from + 1] == 1 && 1 + f[from + 1, to] == f[from, to])
            return from - rowsize;
        if (from + rowsize < rowsize * colsize && f[from, from + rowsize] == 1 && 1 + f[from + rowsize, to] == f[from, to])
            return from - 1;
        return -1;
    }
}