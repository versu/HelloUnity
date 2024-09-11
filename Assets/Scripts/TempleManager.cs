using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempleManager : MonoBehaviour
{
    /// <summary>
    /// ���̊G
    /// </summary>
    public Sprite[] templePicture = new Sprite[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �����̊G��ݒ�
    /// </summary>
    /// <param name="level"></param>
    public void SetTemplePicture(int level)
    {
        GetComponent<Image>().sprite = templePicture[level];
    }

    /// <summary>
    /// �����̑傫����ݒ�
    /// </summary>
    /// <param name="score"></param>
    /// <param name="nextScore"></param>
    public void SetTempleScale(int score, int nextScore)
    {
        var scale = 0.5f + (((float)score / (float)nextScore) / 2.0f);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
