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
    public Coordinates m_coordinates;


    [SerializeField]
    public GameObject m_bottomRightCellSlot;


    [SerializeField]
    public GameObject m_bottomLeftCellSlot;

    [SerializeField]
    private BoxCollider2D m_generationTriggerCollider;

    [SerializeField]
    private bool m_isOriginalCell = false;

    [SerializeField]
    private string m_debugCoordinates;

    [SerializeField]
    private MeshRenderer m_debugMeshRenderer;

    [SerializeField]
    private GameObject m_spikeSpawnSpot;

    [SerializeField]
    private GameObject m_coinSpawnSpot;

    [SerializeField]
    private GameObject m_platformSpawnSpot;

    private GameObject m_ownedSpike = null;
    private GameObject m_ownedCoin = null;
    private GameObject m_ownedPlatform = null;

    [SerializeField]
    private GameObject mp_spike;

    [SerializeField]
    private GameObject mp_coin;

    [SerializeField]
    private GameObject mp_platform;



    public bool m_alive;

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

        Queue<Coordinates> explorationFrontier = new Queue<Coordinates>();
        HashSet<string> visited = new HashSet<string>();

        CellMaster.ms_instance.AddCell(this.m_coordinates, this);
        explorationFrontier.Enqueue(this.m_coordinates);

        //This system doesn't work and needs to be fixed results in some weird combinatorial explosion at a central location

        while (stepsAhead < 200 && explorationFrontier.Count > 0)
        {
            //pop a cell of of the queue
            Coordinates currentCoordinates = explorationFrontier.Dequeue();

            Cell currentCell = CellMaster.ms_instance.GetCell(currentCoordinates);

            stepsAhead++;

            if (visited.Contains(currentCoordinates.GetString()))
            {
                continue;
            }

            int alive = 0;
            int dead = 0;

            for (int xMod = -1; xMod <= 1; xMod++)
            {
                for (int yMod = -1; yMod <= 1; yMod++)
                {

                    Coordinates cellCoordinate = new Coordinates(currentCoordinates.m_x + xMod, currentCoordinates.m_y + yMod);
                    Cell cell = null;
                    if (CellMaster.ms_instance.HasCell(cellCoordinate))
                    {
                        //tally alive or dead

                        cell = CellMaster.ms_instance.GetCell(cellCoordinate);


                        if (cell.m_alive)
                        {
                            alive++;
                        }
                        else
                        {
                            dead++;
                        }
                    }
                    else
                    {
                        dead++;
                        cell = GameObject.Instantiate(mp_cell, currentCell.transform.position + new Vector3(4 * xMod, 4 * yMod, 0), transform.rotation, null).GetComponent<Cell>();
                        CellMaster.ms_instance.AddCell(cellCoordinate, cell);

                        if (Random.Range(0, 100) < 50)
                        {
                            cell.m_alive = true;
                        }
                    }

                    cell.m_coordinates = cellCoordinate;

                    explorationFrontier.Enqueue(cellCoordinate);
                }





            }

            if (currentCell.m_generationTriggerCollider != null)
            {

                if (currentCell.m_alive)
                {
                    if (alive < 2)
                    {
                        currentCell.m_alive = false;
                    }

                    if (alive > 3)
                    {
                        currentCell.m_alive = false;
                    }

                    if (alive == 2)
                    {
                        //spawn a platform
                        if (m_ownedPlatform == null)
                        {
                            m_ownedPlatform = GameObject.Instantiate(mp_platform, currentCell.m_platformSpawnSpot.transform.position, currentCell.m_platformSpawnSpot.transform.rotation, null);
                            Destroy(currentCell.m_ownedSpike);
                            Destroy(currentCell.m_ownedCoin);
                        }
                    }

                    if (alive == 3)
                    {
                        //spawn a spike
                        if (currentCell.m_ownedSpike == null)
                        {
                            currentCell.m_ownedSpike = GameObject.Instantiate(mp_spike, currentCell.m_spikeSpawnSpot.transform.position, currentCell.m_spikeSpawnSpot.transform.rotation, null);
                            Destroy(currentCell.m_ownedPlatform);
                            Destroy(currentCell.m_ownedCoin);
                        }
                    }
                }
                else
                {
                    if (alive == 3)
                    {
                        m_alive = true;
                    }

                    if (alive == 0)
                    {
                        //spawn a coin
                        if (currentCell.m_ownedCoin == null)
                        {
                            currentCell.m_ownedCoin = GameObject.Instantiate(mp_coin, currentCell.m_coinSpawnSpot.transform.position, currentCell.m_coinSpawnSpot.transform.rotation, null);
                            Destroy(currentCell.m_ownedPlatform);
                            Destroy(currentCell.m_ownedSpike);
                        }
                    }
                }


            }
            visited.Add(currentCoordinates.GetString());


        }
    }





    private void SetPlatform()
    {

    }

    private void SetSpike()
    {

    }

    private void SetEmpty()
    {

    }

    private void SetCoin()
    {

    }


    public void SetDebugCoordinates()
    {
        m_debugCoordinates = m_coordinates.GetString();
    }
}
