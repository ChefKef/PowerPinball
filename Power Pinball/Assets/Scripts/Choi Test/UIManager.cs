using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.transform.position = new Vector3(50, 50);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + GameManager.scoreP1;
    }
}
