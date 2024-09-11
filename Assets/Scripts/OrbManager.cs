using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    private GameObject gameManager;

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

        gameManager.GetComponent<GameManager>().GetOrb();
        Destroy(this.gameObject);
    }
}
