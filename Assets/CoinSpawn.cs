using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour {
    [SerializeField]
    private float m_spawnChance;

    private const float m_upperSpawnChance = 100.0f;

    [SerializeField]
    private GameObject mp_coin;

    public void TryToSpawnCoin()
    {
        if(Random.Range(0, m_upperSpawnChance) < m_spawnChance)
        {
            GameObject.Instantiate(mp_coin, transform.position, transform.rotation, transform);
        }
    }
}
