using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScoreObjectPool : MonoBehaviour
{
    /// <summary>
    /// Template prefab representing the object to pool.
    /// </summary>
    [SerializeField] private GameObject template;

    /// <summary>
    /// Holds reference to this Singleton class.
    /// </summary>
    private static HitScoreObjectPool instance;

    /// <summary>
    /// List pooling object instances. Allows for dynamic resizing as needed,
    /// </summary>
    private List<GameObject> pool;

    /// <summary>
    /// Initial pool size.
    /// </summary>
    private const int InitPoolSize = 5;

    /// <summary>
    /// Singleton instance of ObjectPool.
    /// </summary>
    public static HitScoreObjectPool Instance
    {
        get
        {
            // FindObjectOfType returns the first active loaded object of a
            // specified type. It is used here to get the first ObjectPool
            // instance in the scene.
            if (!instance)
                instance = FindObjectOfType(typeof(HitScoreObjectPool)) as HitScoreObjectPool;

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialise object pool with an adequate number of instances.
        pool = new List<GameObject>();
        GameObject temp;

        for (int i = 0; i < InitPoolSize; i++)
        {
            temp = Instantiate(template);
            temp.SetActive(false);
            pool.Add(temp);
        }
    }

    /// <summary>
    /// Requests a GameObject of the specified type from the underlying List.
    /// </summary>
    /// <param name="requestedObjectType">
    /// Type of GameObject requested.
    /// </param>
    /// <returns>
    /// The GameObject, if one is available, and null otherwise.
    /// </returns>
    public GameObject GetPooledObject()
    {
        // Get the first object in the pool that is not active.
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                // Set transparency.
                pool[i].GetComponent<HitScore>().ResetAlpha();

                // Set the pooled object to active and visible in the
                // Scene before returning it to the caller.
                pool[i].SetActive(true);
                //pool[i].GetComponent<Renderer>().enabled = true;
                return pool[i];
            }
        }

        // If we get here, no more objects are available. Make another one, add
        // it to the pool, and return it.
        GameObject temp = Instantiate(template);
        temp.SetActive(true);
        pool.Add(temp);
        return temp;
    }
}
