using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager ms_instance;
    [SerializeField]
    public List<GameObject> mp_stalagmiteChoices;

    [SerializeField]
    private Text m_ScoreText;

    private int m_score = 0;
    private int m_highScore = 0;

    void Awake()
    {
        ms_instance = this;
    }

	// Use this for initialization
	void Start () {
        m_highScore = PlayerPrefs.GetInt("HighScore", 0);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void KillPlayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        PlayerPrefs.SetInt("HighScore", (m_score > m_highScore ? m_score : m_highScore));
    }

    public void GainCoin()
    {
        m_score++;
        m_ScoreText.text = "" + (m_score < m_highScore ? m_highScore - m_score : m_score);
    }
}
