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

        [System.NonSerialized]
        private List<CatCustomisation> _catsNotInUse;

        public void Initialize()
        {
            _catsNotInUse = new List<CatCustomisation>(_catCustomizationOptions);
        }

        public CatCustomisation GetRandomCat()
        {
            if(_catsNotInUse.Count == 0)
            {
                // Deal with this properly later
                return new CatCustomisation();
            }
            int index = Random.Range(0, _catsNotInUse.Count);
            var cat = _catsNotInUse[index];
            _catsNotInUse.RemoveAt(index);
            return cat;
        }

        public void RecycleCat(CatCustomisation cat)
        {
            _catsNotInUse.Add(cat);
        }

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
