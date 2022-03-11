using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourProfileManager : MonoBehaviour
{
    public static ColourProfile p1ColourProfile = new ColourProfile();
    public static ColourProfile p2ColourProfile = new ColourProfile();

    private static ColourProfileManager instance;

    #region Singleton Necessities
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
