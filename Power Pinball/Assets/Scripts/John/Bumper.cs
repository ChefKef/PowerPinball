using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour, IFlashable
{
    /// <summary>
    /// Base point value to be added to player's score when this component is
    /// interacted with.
    /// </summary>
    [SerializeField] private int points;

    [SerializeField] private GameObject ui;
    private SEAudioSource seAudioSource;

    // Custom board component variables.
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float flashDuration;
    private SpriteRenderer spriteRenderer;

    private CircleCollider2D hitReg;
    public float elasticity = 5f; //How much rebound a shot will have when hitting the bumper.
    public float minimumLaunch = 50f; //The minimum amount of force with witch the ball will be launched after hitting the bumper
    public float maximumLaunch = 600f; //Used to keep the ball from clipping out of bounds. Change only with exstensive testing. Going over 600 is asking for trouble.
    void Start()
    {
        seAudioSource = GetComponent<SEAudioSource>();
        hitReg = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PinballManager>())
        {
            PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
            float launchMagnitude = ballsManager.movementMagnitue();
            launchMagnitude *= elasticity;
            if (launchMagnitude < minimumLaunch)
            {
                launchMagnitude = minimumLaunch;
            }
            if(launchMagnitude > maximumLaunch)
            {
                launchMagnitude = maximumLaunch;
            }
            Debug.Log("Launch magnitude: " + launchMagnitude);
            Vector3 ballPos = collision.gameObject.transform.position;
            Vector3 bumperPos = gameObject.transform.position;
            Vector2 ballDir = new Vector2(ballPos.x - bumperPos.x, ballPos.y - bumperPos.y);
            ballDir.Normalize();
            ballDir *= launchMagnitude;
            ballsManager.setVelocity(ballDir);

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

            // Switch sprites so the component appears to flash.
            StartCoroutine("Flash");
        }
    }

    public IEnumerator Flash()
    {
        spriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.sprite = sprites[0];
    }
}
