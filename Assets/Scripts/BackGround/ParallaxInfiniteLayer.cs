using UnityEngine;
using System.Collections.Generic;
public class BackGround : MonoBehaviour
{
    [Header("Parallax Infinite Layer")]
    public Sprite sprite;
    public float paralaxSpeed = 0.5f;
    public int copies = 3;
    public float yOffset = 0f;

    private List<Transform> tiles = new List<Transform>();
    private float spriteWidth;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        tiles.Clear();

        GameObject temp = new GameObject("Tile");
        var sr = temp.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerID = GetComponent<SpriteRenderer>() ? GetComponent<SpriteRenderer>().sortingLayerID : 0;

        sr.transform.position = Vector3.zero;
        sr.transform.localScale = Vector3.one;
        sr.drawMode = SpriteDrawMode.Simple;

        spriteWidth = sr.bounds.size.x;

        for(int i=0; i < copies; i++)
        {
            GameObject tile = Instantiate(temp, transform);
            tile.transform.position = new Vector3(i * spriteWidth, yOffset, 0f);
            tiles.Add(tile.transform);
        }

        Destroy(temp);
    }

    void Update()
    {
        if(!cam)
        {
            return;
        }
        
        float deltaX = cam.transform.position.x * paralaxSpeed;
        transform.position = new Vector3(deltaX, yOffset, transform.position.z);

        float camRightEdge = cam.transform.position.x + cam.orthographicSize * cam.aspect;
        float camLeftEdge = cam.transform.position.x - cam.orthographicSize * cam.aspect;

        foreach(var t in tiles)
        {
            if(t.position.x + spriteWidth / 2 < camLeftEdge)
            {
                float rightMost = GetRightMost();
                t.position = new Vector3(t.position.x + spriteWidth * copies, t.position.y, t.position.z);
            }
            else if(t.position.x - spriteWidth / 2 > camRightEdge)
            {
                float leftMost = GetLeftMost();
                t.position = new Vector3(t.position.x - spriteWidth * copies, t.position.y, t.position.z);
            }
        }
    }

    float GetRightMost()
    {
        float maxX = tiles[0].position.x;
        foreach(var t in tiles)
        {
            if(t.position.x > maxX)
            {
                maxX = t.position.x;
            }
        }
        return maxX;
    }
    float GetLeftMost()
    {
        float minX = tiles[0].position.x;
        foreach(var t in tiles)
        {
            if(t.position.x < minX)
            {
                minX = t.position.x;
            }
        }
        return minX;
    }
}
