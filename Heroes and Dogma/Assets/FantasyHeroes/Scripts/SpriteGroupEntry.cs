using System;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    [Serializable]
    public class SpriteGroupEntry
    {
        public string Collection;
        public string Name;
        public Sprite Sprite;

        public SpriteGroupEntry(string collection, string name, Sprite sprite)
        {
            Collection = collection;
            Name = name;
            Sprite = sprite;
        }
    }
}