using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Utilities
{
    public class FadeInTransparency : MonoBehaviour
    {
        [SerializeField] private float multiplier = 1f;
        [SerializeField] private SpriteRenderer spriteToEdit;
        [SerializeField] private Image imageToEdit;

        [SerializeField] private bool shouldRunEffect;
        private Color col;


        private void Start()
        {
            if (TryGetComponent(out SpriteRenderer _spr))
            {
                spriteToEdit = GetComponent<SpriteRenderer>();
                col = spriteToEdit.color;
            }

            if (TryGetComponent(out Image _img))
            {
                imageToEdit = GetComponent<Image>();
                col = imageToEdit.color;
            }
        }


        private void Update()
        {
            if (shouldRunEffect)
            {
                if (spriteToEdit && !spriteToEdit.color.a.Equals(1))
                {
                    spriteToEdit.color += new Color(0, 0, 0, 1 * multiplier * Time.deltaTime);
                }
                else if (imageToEdit && !imageToEdit.color.a.Equals(1))
                {
                    imageToEdit.color += new Color(0, 0, 0, 1 * multiplier * Time.deltaTime);
                }
            }
        }


        public void RunEffect()
        {
            shouldRunEffect = true;
        }
    }
}