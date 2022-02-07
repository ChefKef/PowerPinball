using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGScript {

public class FGRenderer : MonoBehaviour
{

    private List<GameObject> hurtboxPool;
    private List<GameObject> hitboxPool;


    void Awake()
    {
        hurtboxPool = new List<GameObject>();
        hitboxPool = new List<GameObject>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}