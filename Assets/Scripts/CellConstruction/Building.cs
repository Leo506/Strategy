using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Construction
{

    public class Building : MonoBehaviour
    {
        public Vector2Int Size;

        private SpriteRenderer spriteRenderer;

        public void SetColor(bool available)
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            if (available)
                spriteRenderer.color = Color.green;
            else
                spriteRenderer.color = Color.red;
        }
    }
}