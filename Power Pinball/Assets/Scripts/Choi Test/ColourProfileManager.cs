using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourProfileManager : MonoBehaviour
{
    /// <summary>
    /// P1's customisation choices.
    /// </summary>
    public static ColourProfile p1ColourProfile = new ColourProfile();

    /// <summary>
    /// P2's customisation choices.
    /// </summary>
    public static ColourProfile p2ColourProfile = new ColourProfile();

    // Refer to StateManager for singleton implementation documentation.
    #region Singleton Necessities
    private static ColourProfileManager instance;

    public static ColourProfileManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ColourProfileManager>();

                if (!instance)
                {
                    GameObject gObj = new GameObject();
                    gObj.name = typeof(ColourProfileManager).Name;
                    instance = gObj.AddComponent<ColourProfileManager>();
                }
            }

            return instance;
        }
    }
    #endregion

    #region Lifecycle Hooks
    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this as ColourProfileManager;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    protected void OnApplicationQuit()
    {
        instance = null;
    }
    #endregion
}
