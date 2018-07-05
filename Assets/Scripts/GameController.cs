using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Photon.MonoBehaviour
{

    public Vector3 spawnPos;
    public GameObject ast1;
    public GameObject ast2;
    public GameObject ast3;
    public GameObject Enemy;
    public GameObject spawnPositionBoss;
    public GameObject Boss;
    public float waveWait;
    public float startWait;
    public int countAst;
    public float spawnWait;
    private int Waves;
    private int score;
    private GameObject curBoss;

    void Start()
    {
        Waves = 0;
        score = 0;
        curBoss = null;
        StartCoroutine (SpawnWaves());
    }

    public void addScore(int s)
    {
        score += s;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 40), "Wave " + Waves.ToString() +"\nScore " + score.ToString());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (curBoss == null)
            {
                Waves++;
                for (int i = 0; i < countAst; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnPos.x, spawnPos.x), spawnPos.y, spawnPos.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject ast = ast1;
                    switch (Mathf.CeilToInt(Random.Range(0, 3)))
                    {
                        case 1:
                            if (photonView.isMine) PhotonNetwork.Instantiate("Asteroid1", spawnPosition, spawnRotation, 0);
                            break;
                        case 2:
                            if (photonView.isMine) PhotonNetwork.Instantiate("Asteroid2", spawnPosition, spawnRotation, 0);
                            break;
                        case 3:
                            if (photonView.isMine) PhotonNetwork.Instantiate("Asteroid3", spawnPosition, spawnRotation, 0);
                            break;
                    }
                    if (i == 1 || i == 10)
                    {
                        if(photonView.isMine) PhotonNetwork.Instantiate("fighter04", new Vector3(spawnPosition.x, spawnPosition.y - 5, spawnPosition.z), spawnRotation, 0);
                    }
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            if(Waves == 2)
            {
                Waves++;
                yield return new WaitForSeconds(waveWait);
                if(photonView.isMine)
                    curBoss = PhotonNetwork.Instantiate("fighter03", spawnPositionBoss.transform.position, Quaternion.Euler(Vector3.zero), 0);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
