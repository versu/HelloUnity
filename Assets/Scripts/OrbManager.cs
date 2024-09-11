using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{
    // ÉIÅ[ÉuÇÃäG
    public Sprite[] orbPictures = new Sprite[3];

    private GameObject gameManager;
    private ORB_KIND orbKind;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find(nameof(GameManager));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TourchOrb()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        switch (orbKind)
        {
            case ORB_KIND.BLUE:
                gameManager.GetComponent<GameManager>().GetOrb(1);
                break;
            case ORB_KIND.GREEN:
                gameManager.GetComponent<GameManager>().GetOrb(5);
                break;
            case ORB_KIND.PURPLE:
                gameManager.GetComponent<GameManager>().GetOrb(10);
                break;

        }

        Destroy(this.gameObject);
    }

    public void SetKind(ORB_KIND kind)
    {
        this.orbKind = kind;

        switch (orbKind)
        {
            case ORB_KIND.BLUE:
                GetComponent<Image>().sprite = orbPictures[0];
                break;
            case ORB_KIND.GREEN:
                GetComponent<Image>().sprite = orbPictures[1];
                break;
            case ORB_KIND.PURPLE:
                GetComponent<Image>().sprite = orbPictures[2];
                break;
        }
    }
}

public enum ORB_KIND 
{
    BLUE,
    GREEN,
    PURPLE
}

