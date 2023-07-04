using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] List<Level> m_LevelPFs;
    [SerializeField] Player m_Player;
    Level m_CurrentLevel;
    int m_LevelIndex;
    List<Bot> m_BotOnMap = new List<Bot>();
    public int m_BotRemaining;
    public int BotRemaining => m_BotRemaining + m_BotOnMap.Count;
    public int CharacterAmount => m_CurrentLevel.BotAmount + 1;
    bool IsEndGame;
    private void Awake()
    {
        m_LevelIndex = PlayerPrefs.GetInt("Level", 0);
    }
    private void Start()
    {
        LoadLevel(m_LevelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }
    void OnInit()
    {
        BotPooler.Instance.Init();
        //Set vi tri player
        m_Player.transform.position = m_CurrentLevel.StartPoint.position;
        //update navmesh data
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(m_CurrentLevel.NavMeshData);
        //Spawn bot
        m_BotRemaining = m_CurrentLevel.BotAmount;
    }
    private void Update()
    {
        if (BotRemaining <= 0)
        {
            if (!IsEndGame)
            {
                UIManager.Instance.OpenUI<UITotal>();
                IsEndGame = true;
            }
        }
        SpawnBotUpdate();
    }
    #region SpawnBot
    void SpawnBotUpdate()
    {
        if (m_BotRemaining <= 0) return;
        if (m_BotOnMap.Count < m_CurrentLevel.BotSpawnPerTime)
        {
            SpawnBot();
        }
    }
    public void ReleaseBot(Bot bot)
    {
        m_BotOnMap.Remove(bot);
    }
    void SpawnBot()
    {
        float spawnRadius = Random.Range(m_CurrentLevel.MinRadius, m_CurrentLevel.MaxRadius);
        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
        if (!CheckCharacter(spawnPosition))
        {
            Bot enemy = BotPooler.Instance.Spawn(spawnPosition, Quaternion.identity);
            int levelRand = m_Player.Level + Random.Range(0, 3);
            enemy.ReBorn(levelRand);
            m_BotRemaining--;
            m_BotOnMap.Add(enemy);
        }
        else
        {
            SpawnBot();
        }
    }
    bool CheckCharacter(Vector3 a_Pos)
    {
        Collider[] hits = Physics.OverlapSphere(a_Pos, 9);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (Cache.GetCharacter(hits[i]) != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion
    void LoadLevel(int a_levelIndex)
    {
        if (m_CurrentLevel != null)
        {
            Destroy(m_CurrentLevel.gameObject);
        }
        if (a_levelIndex < m_LevelPFs.Count && a_levelIndex >= 0)
        {
            m_CurrentLevel = Instantiate(m_LevelPFs[a_levelIndex]);
        }
    }
    public void SetHadDeath(bool a_bool)
    {
        m_Player.JustDeath = a_bool;
    }
    private void OnDestroy()
    {
        NavMesh.RemoveAllNavMeshData();
    }
}
