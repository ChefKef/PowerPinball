using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardIdleAnimation : MonoBehaviour
{
    private IFlashable[] components;
    private float elapsedTime;
    [SerializeField] private float interval;

    // Start is called before the first frame update
    void Start()
    {
        // Get all board components in the scene.
        components = GetComponentsInChildren<IFlashable>();
        elapsedTime = interval;

        // When coming from the gameplay screen, timeScale is set to zero when
        // a round is complete. Reset timeScale to 1 here so the animations 
        // can play.
        if (Time.timeScale == 0) Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Randomly flash a board component if the interval has passed.
        if (elapsedTime >= interval)
        {
            elapsedTime = 0;
            StartCoroutine(components[Random.Range(0, components.Length)].Flash());
        }
        else elapsedTime += Time.deltaTime;
    }
}
