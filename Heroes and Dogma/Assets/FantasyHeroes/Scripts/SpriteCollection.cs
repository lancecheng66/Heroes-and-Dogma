#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Collect sprites from specified path
    /// </summary>
    [ExecuteInEditMode]
    public class SpriteCollection : MonoBehaviour
    {
        public Object SpritePath;

        [Header("Body Parts")]
        public List<SpriteGroupEntry> Head;
        public List<SpriteGroupEntry> Ears;
        public List<SpriteGroupEntry> Hair;
        public List<SpriteGroupEntry> HairShort;
        public List<SpriteGroupEntry> Eyebrows;
        public List<SpriteGroupEntry> Eyes;
        public List<SpriteGroupEntry> Mouth;
        public List<SpriteGroupEntry> Beard;
        public List<SpriteGroupEntry> BodyArmL;
        public List<SpriteGroupEntry> BodyArmR;
        public List<SpriteGroupEntry> BodyForearmL;
        public List<SpriteGroupEntry> BodyForearmR;
        public List<SpriteGroupEntry> BodyHandL;
        public List<SpriteGroupEntry> BodyHandR;
        public List<SpriteGroupEntry> BodyLeg;
        public List<SpriteGroupEntry> BodyPelvis;
        public List<SpriteGroupEntry> BodyShin;
        public List<SpriteGroupEntry> BodyTorso;

        [Header("Equipment")]
        public List<SpriteGroupEntry> Helmet;
        public List<SpriteGroupEntry> Back;
        public List<SpriteGroupEntry> MeleeWeapon1H;
        public List<SpriteGroupEntry> MeleeWeapon2H;
        public List<SpriteGroupEntry> Shield;
        public List<SpriteGroupEntry> BowArrow;
        public List<SpriteGroupEntry> BowLimb;
        public List<SpriteGroupEntry> BowRiser;
        public List<SpriteGroupEntry> ArmorArmL;
        public List<SpriteGroupEntry> ArmorArmR;
        public List<SpriteGroupEntry> ArmorForearmL;
        public List<SpriteGroupEntry> ArmorForearmR;
        public List<SpriteGroupEntry> ArmorHandL;
        public List<SpriteGroupEntry> ArmorHandR;
        public List<SpriteGroupEntry> ArmorLeg;
        public List<SpriteGroupEntry> ArmorPelvis;
        public List<SpriteGroupEntry> ArmorShin;
        public List<SpriteGroupEntry> ArmorTorso;

        /// <summary>
        /// Called automatically when something was changed
        /// </summary>
        public void OnValidate()
        {
            Refresh();
        }

        /// <summary>
        /// Read all sprites from specified path again
        /// </summary>
        public void Refresh()
        {
            var path = AssetDatabase.GetAssetPath(SpritePath);

            Head = LoadSprites(path + "/BodyParts/Head");
            Ears = LoadSprites(path + "/BodyParts/Ears");
            Hair = LoadSprites(path + "/BodyParts/Hair");
            HairShort = LoadSprites(path + "/BodyParts/HairShort");
            Eyebrows = LoadSprites(path + "/BodyParts/Eyebrows");
            Eyes = LoadSprites(path + "/BodyParts/Eyes");
            Mouth = LoadSprites(path + "/BodyParts/Mouth");
            Beard = LoadSprites(path + "/BodyParts/Beard");
            BodyArmL = LoadSprites(path + "/BodyParts/Body", "ArmL");
            BodyArmR = LoadSprites(path + "/BodyParts/Body", "ArmR");
            BodyForearmL = LoadSprites(path + "/BodyParts/Body", "ForearmL");
            BodyForearmR = LoadSprites(path + "/BodyParts/Body", "ForearmR");
            BodyHandL = LoadSprites(path + "/BodyParts/Body", "HandL");
            BodyHandR = LoadSprites(path + "/BodyParts/Body", "HandR");
            BodyLeg = LoadSprites(path + "/BodyParts/Body", "Leg");
            BodyPelvis = LoadSprites(path + "/BodyParts/Body", "Pelvis");
            BodyShin = LoadSprites(path + "/BodyParts/Body", "Shin");
            BodyTorso = LoadSprites(path + "/BodyParts/Body", "Torso");
            Helmet = LoadSprites(path + "/Equipment/Helmet");
            ArmorArmL = LoadSprites(path + "/Equipment/Armor", "ArmL");
            ArmorArmR = LoadSprites(path + "/Equipment/Armor", "ArmR");
            ArmorForearmL = LoadSprites(path + "/Equipment/Armor", "ForearmL");
            ArmorForearmR = LoadSprites(path + "/Equipment/Armor", "ForearmR");
            ArmorHandL = LoadSprites(path + "/Equipment/Armor", "HandL");
            ArmorHandR = LoadSprites(path + "/Equipment/Armor", "HandR");
            ArmorLeg = LoadSprites(path + "/Equipment/Armor", "Leg");
            ArmorPelvis = LoadSprites(path + "/Equipment/Armor", "Pelvis");
            ArmorShin = LoadSprites(path + "/Equipment/Armor", "Shin");
            ArmorTorso = LoadSprites(path + "/Equipment/Armor", "Torso");
            Back = LoadSprites(path + "/Equipment/Back");
            MeleeWeapon1H = LoadSprites(path + "/Equipment/MeleeWeapon1H", 1);
            MeleeWeapon2H = LoadSprites(path + "/Equipment/MeleeWeapon2H", 1);
            Shield = LoadSprites(path + "/Equipment/Shield");
            BowArrow = LoadSprites(path + "/Equipment/Bow", "Arrow");
            BowLimb = LoadSprites(path + "/Equipment/Bow", "Limb");
            BowRiser = LoadSprites(path + "/Equipment/Bow", "Riser");
        }

        private static List<SpriteGroupEntry> LoadSprites(string path, int nesting = 0)
        {
            return Directory.GetFiles(path, "*.png", SearchOption.AllDirectories).Select(i => new SpriteGroupEntry(GetCollectionName(i, nesting), Path.GetFileNameWithoutExtension(i), AssetDatabase.LoadAssetAtPath<Sprite>(i))).ToList();
        }

        private static List<SpriteGroupEntry> LoadSprites(string path, string spriteName, int nesting = 0)
        {
            var list = new List<SpriteGroupEntry>();

            foreach (var image in Directory.GetFiles(path, "*.png", SearchOption.AllDirectories))
            {
                var p = image;
                var entries = AssetDatabase.LoadAllAssetsAtPath(image).Where(i => i is Sprite && i.name == spriteName).Cast<Sprite>()
                    .Select(i => new SpriteGroupEntry(GetCollectionName(p, nesting), Path.GetFileNameWithoutExtension(p), i));

                list.AddRange(entries);
            }

            return list;
        }

        private static string GetCollectionName(string path, int nesting)
        {
            var parent = Directory.GetParent(path);

            for (var i = 0; i < nesting; i++)
            {
                if (parent != null) parent = parent.Parent;
            }

            if (parent == null) throw new NotSupportedException();

            return parent.Name;
        }
    }
}

#endif