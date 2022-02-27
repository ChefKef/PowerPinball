using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover : MonoBehaviour
{
    /// <summary>
    /// Base point value to be added to player's score when this component is
    /// interacted with.
    /// </summary>
    [SerializeField] private int points; // 50

    [SerializeField] private GameObject ui;
    private SEAudioSource seAudioSource;

    public float cooldown = .1f; //Time until the rollover can be activated again.
    private float counter;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        seAudioSource = GetComponent<SEAudioSource>();
        counter = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            counter -= Time.deltaTime;
            if(counter <= 0)
            {
                active = true;
            }
        }
    }

    // OnTriggerEnter used because Rollovers are Triggers in the scene.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.gameObject.GetComponent<PinballManager>())
            {
                PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
                counter = cooldown;
                active = false;

                // Play SE.
                seAudioSource.PlayAudio();

                // Update player score.
                GameManager.issuePoints(points, ballsManager.player);

                // Request a TMPro object from the object pool and parent it to the
                // UIManager GameObject in the Hierarchy.
                GameObject pooledObject = HitScoreObjectPool.Instance.GetPooledObject();
                pooledObject.transform.SetParent(ui.transform);

                // Modify the text values so they correspond to the component's
                // value and position onscreen.
                HitScore hitScore = pooledObject.GetComponent<HitScore>();
                hitScore.SetText(points.ToString());
                hitScore.SetPosition(transform.position);
            }
        }
    }
}
