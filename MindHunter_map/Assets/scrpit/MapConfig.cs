using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig  {
    //mapSizeRow，mapSizeColumn均为测试用参数
    public int mapSizeRow;
    public int mapSizeColumn;
    //最左上角的房间在网格中的X坐标
    public int startX;
    //最左上角的房间在网格中的Y坐标
    public int startY;

    public MapConfig(int mapSizeRow, int mapSizeColumn, int startX, int startY)
    {
        this.mapSizeRow = mapSizeRow;
        this.mapSizeColumn = mapSizeColumn;
        this.startX = startX;
        this.startY = startY;
    }
}
