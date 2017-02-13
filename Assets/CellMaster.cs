using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMaster : MonoBehaviour {
    public static CellMaster ms_instance;
    Dictionary<string, Cell> m_cells;

    void Awake()
    {
        ms_instance = this;
        m_cells = new Dictionary<string, Cell>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
    public void AddCell(Coordinates coordinates, Cell cell)
    {
        if (m_cells.ContainsKey(coordinates.GetString()))
        {
            return;
        }
        m_cells.Add(coordinates.GetString(), cell);
        cell.SetDebugCoordinates();
    }

    public bool HasCell(Coordinates coordinates)
    {
        return m_cells.ContainsKey(coordinates.GetString());
    }


    public Cell GetCell(Coordinates coordinates)
    {
        return m_cells[coordinates.GetString()];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
