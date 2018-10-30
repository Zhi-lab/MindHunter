using UnityEngine;
using UnityEditor;

public class PathFinder
{
    public int[,] f, g;
    //g for fighter ,f for servant
    MapConfig mapConfig;
    int rowsize, colsize;
    public PathFinder(MapConfig mapConfig)
    {
        this.mapConfig = mapConfig;
        rowsize = mapConfig.rowNum;
        colsize = mapConfig.columnNum;
    }
    public void init()
    {
        f = new int[rowsize * colsize, rowsize * colsize];
        g = new int[rowsize * colsize, rowsize * colsize];

        for (int i = 0; i < rowsize * colsize; i++)
        {

            for (int j = 0; j < rowsize * colsize; j++)
            {
                f[i, j] = -1;
            }
        }

        for (int i = 0; i < rowsize * colsize; i++)
        {
            if (i + colsize < rowsize * colsize)
            {
                f[i, i + colsize] = f[i + colsize, i] = 1;
            }

            if ((i + 1) % colsize != 0 && i + 1 < rowsize * colsize)
            {
                f[i, i + 1] = f[i + 1, i] = 1;
            }
        }

        for (int i = 0; i < rowsize * colsize; i++)
        {
            f[i, i] = 0;
        }

            for (int i = 0; i < rowsize * colsize; i++)
        {
            for (int j = 0; j < rowsize * colsize; j++)
            {
                g[i, j] = f[i, j];
            }
        }
        foreach (var room in mapConfig.roomList)
        {
            if ((room.ID) % colsize != 0 && room.ID < rowsize * colsize)
            {
                if (room.roomRight == 1)
                {
                    g[room.ID, room.ID - 1] = f[room.ID, room.ID - 1] = g[room.ID - 1, room.ID] = f[room.ID - 1, room.ID] = -1;
                }
                else if (room.roomRight == 2)
                {
                    g[room.ID, room.ID - 1] = g[room.ID - 1, room.ID] = -1;
                }
            }

            if (room.ID -1 + colsize < rowsize * colsize)
            {
                if (room.roomDown == 1)
                {
                    g[room.ID - 1, room.ID - 1 + colsize] = f[room.ID - 1, room.ID - 1 + colsize] = g[room.ID - 1+ colsize, room.ID - 1 ] = f[room.ID - 1+ colsize, room.ID - 1 ] = -1;
                }
                else if (room.roomDown == 2)
                {
                    g[room.ID - 1, room.ID - 1 + colsize] = g[room.ID - 1+ colsize, room.ID - 1 ] = -1;
                }
            }
        }
        for (var k = 0; k < rowsize * colsize; k++)
        {
            for (int i = 0; i < rowsize * colsize; i++)
            {
                for (var j = 0; j < rowsize * colsize; j++)
                {
                    if (i != j && f[k, j] > 0 && f[i, k] > 0 && (f[k, j] + f[i, k] < f[i, j] || f[i, j] == -1))
                    {
                        f[i, j] = f[k, j] + f[i, k];
                    }
                }
            }
        }

        for (var k = 0; k < rowsize * colsize; k++)
        {
            for (int i = 0; i < rowsize * colsize; i++)
            {
                for (var j = 0; j < rowsize * colsize; j++)
                {
                    if (i != j && g[k, j] > 0 && g[i, k] > 0 && (g[k, j] + g[i, k] < g[i, j] || g[i, j] == -1))
                    {
                        g[i, j] = g[k, j] + g[i, k];
                    }
                }
            }
        }
        Debug.Log("pathFinder success");
    }
    public Vector2Int? GoForward(Vector2Int fromRoom, Vector2Int toRoom,string tag)
    {
        int x;
        if (tag == "fighter")
        {
            x = GoFowardFighter(fromRoom, toRoom);
        }
        else
        {
            x = GoFowardServant(fromRoom, toRoom);
        }
        if (x == -1)
            return null;
        return new Vector2Int(x / colsize,x % colsize);
    }
    public int GoFowardFighter(Vector2Int fromRoom, Vector2Int toRoom)
    {
        int from = fromRoom.x * colsize + fromRoom.y, to = toRoom.x * colsize + toRoom.y;
        if (from == to)
            return from;
        if (g[from, to] == -1)
            return -1;
        if (from - colsize >= 0 && g[from, from - colsize] == 1 && 1 + g[from - colsize, to] == g[from, to])
            return from - colsize;
        if (from % colsize != 0 && from >= 1 && g[from, from - 1] == 1 && 1 + g[from - 1, to] == g[from, to])
            return from - 1;
        if (from + 1 % colsize != 0 && from + 1 < rowsize * colsize && g[from, from + 1] == 1 && 1 + g[from + 1, to] == g[from, to])
            return from + 1;
        if (from + colsize < colsize * rowsize && g[from, from + colsize] == 1 && 1 + g[from + colsize, to] == g[from, to])
            return from + colsize;
        return -1;
    }
    public int GoFowardServant(Vector2Int fromRoom, Vector2Int toRoom)
    {
        int from = fromRoom.x * colsize + fromRoom.y, to = toRoom.x * colsize + toRoom.y;
        if (from == to)
            return from;
        if (f[from, to] == -1)
            return -1;
        if (from - colsize >= 0 && f[from, from - colsize] == 1 && 1 + f[from - colsize, to] == f[from, to])
            return from - colsize;
        if (from % colsize != 0 && from >= 1 && f[from, from - 1] == 1 && 1 + f[from - 1, to] == f[from, to])
            return from - 1;
        if (from + 1 % colsize != 0 && from + 1 < rowsize * colsize && f[from, from + 1] == 1 && 1 + f[from + 1, to] == f[from, to])
            return from + 1;
        if (from + colsize < colsize * rowsize && f[from, from + colsize] == 1 && 1 + f[from + colsize, to] == f[from, to])
            return from + colsize;
        return -1;
    }
}