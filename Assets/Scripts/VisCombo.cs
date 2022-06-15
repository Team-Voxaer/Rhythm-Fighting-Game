using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisCombo : MonoBehaviour
{
    public Sprite initSprite;
    private SpriteRenderer spriteRenderer;
    // public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeImage(Sprite _sprite) {
        spriteRenderer.sprite = _sprite;
        spriteRenderer.size = new Vector2(100, 100);
    }
}
