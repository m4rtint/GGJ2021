using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCat
{
    /// <summary>
    /// This serves as a runtime replacement for the CSV
    /// </summary>
    [CreateAssetMenu]
    public class DialogueOptions : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private string _pathToCSV;
#endif
        public List<CatCustomisation> _catCustomizationOptions;

        

    }

    [System.Serializable]
    public struct CatCustomisation
    {
        public string _catName;
        public string _catNameAnswer;
        public string _catFoodAnswer;
        public string _catActivityAnswer;
        public Food _food;
        public Toy _toy;
        public string _flavourText;
    }

    public enum Food
    {
        Tuna,
        Salmon,
        Chicken,
        Pumpkin,
        DryKibble,
        WetKibble,
        Kibble, //Use this for both?
        Bones,
        Beef
    } 

    public enum Toy
    {
        YarnBalls,
        Hijinks,
        CardboardBox,
        FishingRod,
        CatnipSack,
        ScratchingPost
    }
}
