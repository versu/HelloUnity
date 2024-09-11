using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �I�[�u�̍ő吔
    /// </summary>
    private const int MAX_ORB = 10;

    /// <summary>
    /// �I�[�u���������鎞��
    /// </summary>
    private const int RESPAWN_TIME = 5;

    public GameObject orbPrefab;

    public GameObject canvasGame;

    public GameObject textScore;

    // ���݂̃X�R�A
    private int score = 0;

    // ���x���A�b�v�܂ł̕K�v�ȃX�R�A
    private int nextScore = 100;

    // ���݂̃I�[�u��
    private int currentOrb = 0;

    // �O��I�[�u�𐶐���������
    private DateTime lastDateTime;

    // Start is called before the first frame update
    void Start()
    {
        // ����̃I�[�u����
        currentOrb = 10;
        for (int i = 0; i < currentOrb; i++)
        {
            CreateOrb();
        }

        lastDateTime = DateTime.UtcNow;

        RefreshScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (MAX_ORB <= currentOrb)
        {
            return;
        }

        var timespan = DateTime.UtcNow - lastDateTime;
        while (TimeSpan.FromSeconds(RESPAWN_TIME) <= timespan)
        {
            CreateNewOrb();
            timespan -= TimeSpan.FromSeconds(RESPAWN_TIME);
        }
    }

    public void GetOrb()
    {
        lastDateTime = DateTime.UtcNow;
        score += 1;
        RefreshScoreText();
        currentOrb--;
    }

    public void CreateNewOrb()
    {
        lastDateTime = DateTime.UtcNow;
        if (MAX_ORB <= currentOrb)
        {
            return;
        }

        CreateOrb();
        currentOrb++;
    }

    private void CreateOrb()
    {
        var orb = Instantiate(orbPrefab);
        orb.transform.SetParent(canvasGame.transform, false);
        orb.transform.localPosition = new Vector3(
            x: UnityEngine.Random.Range(-300.0f, 300.0f),
            y: UnityEngine.Random.Range(-140.0f, -500.0f),
            z: 0f
            );
    }

    private void RefreshScoreText()
    {
        textScore.GetComponent<Text>().text = $"���F{score} / {nextScore}";
    }
}
