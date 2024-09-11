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
    private const int RESPAWN_TIME = 1;

    /// <summary>
    /// �ő储�����x��
    /// </summary>
    private const int MAX_LEVEL = 2;

    public GameObject orbPrefab;
    public GameObject smokePrefab;
    public GameObject kusudamaPrefab;
    public GameObject canvasGame;
    public GameObject textScore;
    public GameObject imageTemple;

    // ���݂̃X�R�A
    private int score = 0;

    // ���x���A�b�v�܂ł̕K�v�ȃX�R�A
    private int nextScore = 10;

    // ���݂̃I�[�u��
    private int currentOrb = 0;

    //�@���݂̂����̃��x��
    private int templeLevel = 0;

    // �O��I�[�u�𐶐���������
    private DateTime lastDateTime;

    private int[] nextScoreTable = new int[] { 10, 100, 1000 };
 
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
        nextScore = nextScoreTable[templeLevel];
        imageTemple.GetComponent<TempleManager>().SetTemplePicture(templeLevel);
        imageTemple.GetComponent<TempleManager>().SetTempleScale(score, nextScore);

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

    public void GetOrb(int getScore)
    {
        if (score < nextScore)
        {
            score += getScore;

            lastDateTime = DateTime.UtcNow;

            if (nextScore < score)
            {
                score = nextScore;
            }

            TempleLevelUp();
            RefreshScoreText();
            imageTemple.GetComponent<TempleManager>().SetTempleScale(score, nextScore);

            // �Q�[���N���A����
            if (score == nextScore && templeLevel == MAX_LEVEL)
            {
                CrearEffect();
            }
        }

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

        int kind = UnityEngine.Random.Range(0, templeLevel + 1);
        switch (kind)
        {
            case 0:
                orb.GetComponent<OrbManager>().SetKind(ORB_KIND.BLUE);
                break;
            case 1:
                orb.GetComponent<OrbManager>().SetKind(ORB_KIND.GREEN);
                break;
            case 2:
                orb.GetComponent<OrbManager>().SetKind(ORB_KIND.PURPLE);
                break;
        }
    }

    private void RefreshScoreText()
    {
        textScore.GetComponent<Text>().text = $"���F{score} / {nextScore}";
    }

    // ���̃��x���Ǘ�
    private void TempleLevelUp()
    {
        if (nextScore <= score && templeLevel < MAX_LEVEL)
        {
            templeLevel++;
            score = 0;

            TempleLevelUpEffect();

            nextScore = nextScoreTable[templeLevel];
            imageTemple.GetComponent<TempleManager>().SetTemplePicture(templeLevel);
        }
    }

    // ���̃��x���A�b�v���̉��o
    private void TempleLevelUpEffect()
    {
        var smoke = Instantiate(smokePrefab);
        smoke.transform.SetParent (canvasGame.transform, false);
        smoke.transform.SetSiblingIndex(2); // ���̏d�ˏ����w��

        // 0.5�b��ɍ폜
        Destroy(smoke, 0.5f);
    }

    // �����Ō�܂ň�������̉��o
    private void CrearEffect()
    {
        var kusudama = Instantiate(kusudamaPrefab);
        kusudama.transform.SetParent(canvasGame.transform, false);
    }
}
