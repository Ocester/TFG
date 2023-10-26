using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CollectableVegetablesSO", menuName = "Scriptable Object/Collectable Item", order = 1)]
public class CollectableVegetablesSO : ScriptableObject
{
        public string carrot = "carrot";
        public Sprite carrotImg;
        public string corn = "corn";
        public Sprite cornImg;
        public string tomato = "tomato";
        public Sprite tomatoImg;
        public string pumpkin = "pumpkin";
        public Sprite pumpkinImg;
        public string aubergine = "aubergine";
        public Sprite aubergineImg;
        public string lettuce = "lettuce";
        public Sprite lettuceImg;
        public string cucumber = "cucumber";
        public Sprite cucumberImg;
    
}
