using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance;


    [SerializeField] List<Sprite> allSprites;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public Sprite GetSprite(string _spriteName)
    {
        int count = allSprites.Count;
        for (int i = 0; i < count; i++) 
        {
            if(_spriteName == allSprites[i].name)
            {
                return allSprites[i];
            }
        }
        return null;
    }
}
