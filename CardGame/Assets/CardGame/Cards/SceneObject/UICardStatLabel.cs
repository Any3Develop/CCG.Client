using UnityEngine;

namespace CardGame.Cards
{
    public class UICardStatLabel : UICardLabel
    {
        [SerializeField] private SpriteRenderer _iconPlaceHolder;

        public void SetIcon(Sprite value)
        {
            _iconPlaceHolder.sprite = value;
        }
    }
}