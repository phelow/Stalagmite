using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour {
    [SerializeField]
    private GameObject m_nextStalagmiteSlot;

    [SerializeField]
    private List<CoinSpawn> m_coinSpawnSlots;

    public Stalagmite m_nextStalagmite;

	// Use this for initialization
	void Start () {
        GenerateFeatures();
	}

    private void GenerateFeatures()
    {
        foreach(CoinSpawn spawn in m_coinSpawnSlots)
        {
            spawn.TryToSpawnCoin();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeChildStalagmite()
    {
        m_nextStalagmite = GameObject.Instantiate(GameManager.ms_instance.mp_stalagmiteChoices[Random.Range(0, GameManager.ms_instance.mp_stalagmiteChoices.Count)], m_nextStalagmiteSlot.transform.position, m_nextStalagmiteSlot.transform.rotation, null).GetComponent<Stalagmite>();
        
    }

    public void TriggerGeneration() //trigger the next 20 stalagmites, if any do not exist generate one
    {
        int stepsAhead = 0;
        Stalagmite m_currentStalagmite = this;

        while(stepsAhead < 100)
        {
            if(m_currentStalagmite.m_nextStalagmite == null)
            {
                m_currentStalagmite.MakeChildStalagmite();
            }
            stepsAhead++;

            m_currentStalagmite = m_currentStalagmite.m_nextStalagmite;
        }

    }
}
