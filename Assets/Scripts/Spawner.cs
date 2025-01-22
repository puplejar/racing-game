using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnPos;
    public List<GameObject> Obstacles;
    public GameObject gas;
    
    [Space(20)]
    public float spawnMaxTime;
    public float gasSpawnTime;
    public int spawnMinCount = 1;
    public int spawnMaxCount = 1;
    
    private float currentTime;
    private float currentGasSpawnTime;
    private bool gasSpawn = false;
    
    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    void FixedUpdate()
    {
        if (gm.isLive)
        {
            currentTime += Time.fixedDeltaTime;
            currentGasSpawnTime += Time.fixedDeltaTime;

            if (currentTime >= spawnMaxTime)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    void OnDrawGizmos()
    {
        // 미리 설정한 콜라이더의 위치와 크기
        Vector3 colliderPos = new Vector3(0, 0, 0); // 위치
        Vector3 colliderSize = new Vector3(1, 1, 1); // 크기

        Gizmos.color = Color.green; // 박스 색상 설정
        Gizmos.DrawWireCube(transform.position + colliderPos, colliderSize);
    }

    IEnumerator Spawn()
    {
        int count = spawnPos.Count;
        int posRand = Random.Range(0, count);
        int spawnRand = Random.Range(spawnMinCount, spawnMaxCount);
        
        
        //가스 생성 가능여부
        if (currentGasSpawnTime > gasSpawnTime)
        {
            gasSpawn = true;
            currentGasSpawnTime = 0;
        }
        
        //위치에 생성 가능여부, 중복 생성 방지
        List<bool> spawned = new List<bool>();
        for (int i = 0; i < count; i++)
        {
            spawned.Add(false);
        }
        
        
        for (int i = 0; i < spawnRand; i++)
        {
            while (spawned[posRand] == true)
            {
                posRand = Random.Range(0, count);
            }

            if (gasSpawn)
            {
                Instantiate(gas, spawnPos[posRand].transform.position, Quaternion.identity);
                gasSpawn = false;
                spawned[posRand] = true;
                continue;
            }

            if (Obstacles.Count != 0)
            {
                Instantiate(Obstacles[Random.Range(0, Obstacles.Count)], spawnPos[posRand].transform.position,
                    Quaternion.identity);
                spawned[posRand] = true;
            }
        }

        currentTime = 0;

        yield return null;
    }
}
