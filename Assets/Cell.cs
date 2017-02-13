using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    public Cell m_topCell;
    [SerializeField]
    public Cell m_leftCell;
    [SerializeField]
    public Cell m_rightCell;
    [SerializeField]
    public Cell m_bottomCell;


    [SerializeField]
    private GameObject mp_cell;

    [SerializeField]
    private Coordinates m_coordinates;

    [SerializeField]
    public GameObject m_topCellSlot;
    [SerializeField]
    public GameObject m_leftCellSlot;
    [SerializeField]
    public GameObject m_rightCellSlot;
    [SerializeField]
    public GameObject m_bottomCellSlot;

    [SerializeField]
    private BoxCollider2D m_generationTriggerCollider;

    [SerializeField]
    private bool m_isOriginalCell = false;

    [SerializeField]
    private string m_debugCoordinates;

    // Use this for initialization
    void Start()
    {
        if (m_isOriginalCell)
        {
            m_coordinates = new Coordinates(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Body")
        {
            TriggerGeneration();
            Destroy(m_generationTriggerCollider);

        }
    }

    public void TriggerGeneration() //trigger the next 20 cells, if any do not exist generate one
    {
        int stepsAhead = 0;
        Cell m_currentStalagmite = this;

        Queue<Cell> explorationFrontier = new Queue<Cell>();
        HashSet<string> visited = new HashSet<string>();


        explorationFrontier.Enqueue(this);

        while (stepsAhead < 1000 && explorationFrontier.Count > 0)
        {
            //pop a cell of of the queue
            Cell current = explorationFrontier.Dequeue();
            CellMaster.ms_instance.AddCell(current.m_coordinates, current);


            if (visited.Contains(current.m_coordinates.GetString()))
            {
                continue;
            }

            
            Coordinates topCellCoordinates = new Coordinates(current.m_coordinates.m_x, current.m_coordinates.m_y + 1);


            Coordinates bottomCellCoordinates = new Coordinates(current.m_coordinates.m_x, current.m_coordinates.m_y - 1);


            Coordinates leftCellCoordinates = new Coordinates(current.m_coordinates.m_x - 1, current.m_coordinates.m_y);


            Coordinates rightCellCoordinates = new Coordinates(current.m_coordinates.m_x + 1, current.m_coordinates.m_y);
            
            


            if (CellMaster.ms_instance.HasCell(topCellCoordinates))
            {
                current.m_topCell = CellMaster.ms_instance.GetCell(topCellCoordinates);
            }
            else
            {
                current.m_topCell = GameObject.Instantiate(mp_cell, current.m_topCellSlot.transform.position, current.m_topCellSlot.transform.rotation, null).GetComponent<Cell>();
                current.m_topCell.m_coordinates = topCellCoordinates;
            }



            if (CellMaster.ms_instance.HasCell(bottomCellCoordinates))
            {
                current.m_bottomCell = CellMaster.ms_instance.GetCell(bottomCellCoordinates);
            }
            else
            {
                current.m_bottomCell = GameObject.Instantiate(mp_cell, current.m_bottomCellSlot.transform.position, current.m_bottomCellSlot.transform.rotation, null).GetComponent<Cell>();
                current.m_bottomCell.m_coordinates = bottomCellCoordinates;
            }



            if (CellMaster.ms_instance.HasCell(leftCellCoordinates))
            {
                current.m_leftCell = CellMaster.ms_instance.GetCell(leftCellCoordinates);
            }
            else
            {
                current.m_leftCell = GameObject.Instantiate(mp_cell, current.m_leftCellSlot.transform.position, current.m_leftCellSlot.transform.rotation, null).GetComponent<Cell>();
                current.m_leftCell.m_coordinates = leftCellCoordinates;
            }


            if (CellMaster.ms_instance.HasCell(rightCellCoordinates))
            {
                current.m_rightCell = CellMaster.ms_instance.GetCell(rightCellCoordinates);
            }
            else
            {
                current.m_rightCell = GameObject.Instantiate(mp_cell, current.m_rightCellSlot.transform.position, current.m_rightCellSlot.transform.rotation, null).GetComponent<Cell>();
                current.m_rightCell.m_coordinates = rightCellCoordinates;
            }


            CellMaster.ms_instance.AddCell(current.m_topCell.m_coordinates, current.m_topCell);

            CellMaster.ms_instance.AddCell(current.m_bottomCell.m_coordinates, current.m_bottomCell);

            CellMaster.ms_instance.AddCell(current.m_leftCell.m_coordinates, current.m_leftCell);

            CellMaster.ms_instance.AddCell(current.m_rightCell.m_coordinates, current.m_rightCell);


            explorationFrontier.Enqueue(current.m_topCell);

            explorationFrontier.Enqueue(current.m_bottomCell);

            explorationFrontier.Enqueue(current.m_leftCell);

            explorationFrontier.Enqueue(current.m_rightCell);


            visited.Add(current.m_coordinates.GetString());

            stepsAhead++;

        }


    }

    public void SetDebugCoordinates()
    {
        m_debugCoordinates = m_coordinates.GetString();
    }
}
