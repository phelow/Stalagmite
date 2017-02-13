using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinates {
    public int m_x;
    public int m_y;

    public Coordinates(int x,int y)
    {
        m_x = x;
        m_y = y;
    }

    public string GetString()
    {
        return m_x + "==" + m_y;
    }
}
