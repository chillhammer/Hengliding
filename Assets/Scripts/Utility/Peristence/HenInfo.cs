using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raising;


//serializable container for persistent hen fields
[System.Serializable]
public class HenInfo
{

    //Increment this number every time you add, modify, or reorder fields in this class
    //To prevent trying to read old JSON saves from an incompatible format
    public static int VERSION_NUMBER = 2;


    public string name;
    public float sizeStat;
    public float loveStat;
    public float fitnessStat;
    public float feathersStat;
    public int breedNumber; // we have to cast this to int for the serialization to work properly

    public HenInfo(string henName, HenBreed breed, float size, float love, float fitness, float feathers) {
        this.name = henName;
        this.breedNumber = (int) breed;
        this.sizeStat = size;
        this.loveStat = love;
        this.fitnessStat = fitness;
        this.feathersStat = feathers;
    }

    public HenInfo(Hen hen) {
        this.breedNumber = (int) hen.breed;
        //TODO use correct stats
        this.sizeStat = hen.size.value;
        this.loveStat = hen.love.value;
        this.fitnessStat = hen.fitness.value;
        this.feathersStat = hen.featherQuality.value;
    }
    

  
}
