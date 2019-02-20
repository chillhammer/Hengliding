using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raising;


//serializable container for persistent hen fields
[System.Serializable]
public class HenInfo
{
    public float sizeStat;
    public float loveStat;
    public float fitnessStat;
    public float feathersStat;
    public int breedNumber; // we have to cast this to int for the serialization to work properly

    public HenInfo(HenBreed breed, float size, float love, float fitness, float feathers) {
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
