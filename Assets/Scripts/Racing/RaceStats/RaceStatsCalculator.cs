using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaceStatsCalculator
{

    public static readonly float MIN_WINGSPAN = 0.5f;
    public static readonly float MAX_WINGSPAN = 1.5f;

    public static readonly float MAX_DRAG_MULT = 1f;
    public static readonly float MIN_DRAG_MULT = 0.5f;

    public static readonly float MIN_MASS = 0.75f;
    public static readonly float MAX_MASS = 1.5f;

    public static readonly float MIN_AUTHORITY = 1f;
    public static readonly float MAX_AUTHORITY = 1.4f;


    //modifies static values in SelectedRaceStats based on the selected list of hens
    public static void calculateStats(List<HenInfo> hens) {

        if(hens.Count == 0) {
            //can't race with zero hens
            SelectedRaceParameters.mass = 0;
            SelectedRaceParameters.controlAuthority = 0;
            SelectedRaceParameters.dragMultiplier = 0;
            SelectedRaceParameters.wingspan = 0;
            return;
        }

        float sizeSum = 0;
        float featherSum = 0;
        float fitnessSum = 0;
        float loveSum = 0;
        foreach (HenInfo hen in hens) {
            sizeSum += hen.sizeStat;
            featherSum+=hen.feathersStat;
            fitnessSum+=hen.fitnessStat;
            loveSum+=hen.loveStat;
        }

        sizeSum = Mathf.Clamp(sizeSum, 0, 100);
        featherSum = Mathf.Clamp(featherSum, 0, 100);
        fitnessSum = Mathf.Clamp(fitnessSum, 0, 100);
        loveSum = Mathf.Clamp(loveSum, 0, 100);
        
        //calculate control authority
        float authorityBoostFactor = Mathf.Sqrt(loveSum * 0.01f);
        SelectedRaceParameters.controlAuthority = MIN_AUTHORITY + ((MAX_AUTHORITY - MIN_AUTHORITY) * authorityBoostFactor);

        //calculate wingspan
        SelectedRaceParameters.wingspan = MIN_WINGSPAN + ((MAX_WINGSPAN-MIN_WINGSPAN) * fitnessSum * 0.01f);

        //calculate mass
        SelectedRaceParameters.mass = MIN_WINGSPAN + ((MAX_MASS-MIN_MASS) * sizeSum * 0.01f);

        //calculate drag multiplier
        float dragReductionFactor = 1f - Mathf.Sqrt(featherSum * 0.01f);
        SelectedRaceParameters.dragMultiplier = MAX_AUTHORITY - ((MAX_AUTHORITY - MIN_AUTHORITY) * dragReductionFactor);
    }
}
