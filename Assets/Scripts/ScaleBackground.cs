using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour
{
    SpriteRenderer bgSprite;
    // Start is called before the first frame update
    void Start()
    {
        bgSprite = GetComponent<SpriteRenderer>();
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float bgHeight = bgSprite.sprite.bounds.size.y;
        float bgWidth = bgSprite.sprite.bounds.size.x;

        float ScaleX = cameraWidth / bgWidth;
        float ScaleY = cameraHeight / bgHeight;

        transform.localScale = new Vector3(ScaleX, ScaleY, 1f);

    }
}
