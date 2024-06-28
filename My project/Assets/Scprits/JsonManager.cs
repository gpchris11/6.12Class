
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public static JsonManager Instance;

    List<cItemData> itemDatas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        initJsonDatas();
    }

    private void initJsonDatas()
    {
        TextAsset itemData = Resources.Load("ItemData") as TextAsset;
        itemDatas = JsonConvert.DeserializeObject<List<cItemData>>(itemData.ToString());
    }

    public string GetSpriteNameFromIdx(string _idx)
    {
        if (itemDatas == null) return string.Empty;

        return itemDatas.Find(x => x.idx == _idx).sprite;
    }
}
