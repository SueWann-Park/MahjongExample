using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    GameObject[] man;
    GameObject[] pin;
    GameObject[] sou;
    GameObject[] win;
    GameObject[] sam;

    Sprite white;

    static int listCount = 0;

    public static List<GameObject> selectedList;
    void Start()
    {
        selectedList = new List<GameObject>();
        white = Resources.Load<Sprite>("Tiles/_white");

        MakeLine(man, 9, "Man", 4);
        MakeLine(pin, 9, "Pin", 2.5f);
        MakeLine(sou, 9, "Sou", 1);
        MakeLine(win, 4, "Win", 4, 3);
        MakeLine(sam, 3, "Zam", 2.5f, 3);
    }

    private void MakeLine(GameObject[] line, int size, string name, float height, float width = -8)
    {
        GameObject manParent = new GameObject();
        manParent.name = name;

        man = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            man[i] = new GameObject();
            man[i].name = name + (i + 1).ToString();
            man[i].transform.parent = manParent.transform;

            AddChild(man[i].transform, "background");
            AddChild(man[i].transform, man[i].name);

            man[i].transform.position = new Vector3(width + i, height, 0);

            man[i].AddComponent<Tile_Button>();
        }
    }

    private void AddChild(Transform parent, string contentName)
    {
        GameObject back = new GameObject();
        back.name = contentName == "background" ? contentName : "content";
        back.transform.parent = parent.transform;

        SpriteRenderer bsr = back.AddComponent<SpriteRenderer>();
        if(contentName == "background")
        {
            bsr.sprite = white;
        }
        else
        {
            bsr.sprite = Resources.Load<Sprite>("Tiles/" +contentName);
        }

        bsr.sortingLayerName = "Object";
        bsr.sortingOrder = contentName == "background" ? 0 : 1;
    }

    public static bool Tile_AddList(GameObject origin_tile)
    {
        if (selectedList.Count == 14)
            return false;

        GameObject list_tile = Instantiate(origin_tile);
        list_tile.name += listCount.ToString();
        listCount++;
        Destroy(list_tile.GetComponent<Tile_Button>());

        Tile_List tileList = list_tile.AddComponent<Tile_List>();
        selectedList.Add(list_tile);
        tileList.origin = origin_tile.GetComponent<Tile_Button>();

        SortList();

        return true;
    }

    public static void RemoveObjectFromList(GameObject tile)
    {
        foreach(GameObject temp in selectedList)
        {
            if(temp == tile)
            {
                selectedList.Remove(tile);
                SortList();
                return;
            }
        }
    }

    static int Comparer(GameObject go1, GameObject go2)
    {
        return go1.name.CompareTo(go2.name);
    }

    public static void SortList()
    {
        selectedList.Sort(Comparer);

        for(int i = 0; i < selectedList.Count; i++)
        {
            Vector3 current = new Vector3(-7.2f + i * 1.1f, -3.5f, 0);
            selectedList[i].transform.position = current;
            selectedList[i].GetComponent<Tile_Button>().startPos = current;
        }
    }
}
