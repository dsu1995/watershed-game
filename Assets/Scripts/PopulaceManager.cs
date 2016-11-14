using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PopulaceManager {

    IList<Population> populaces = new List<Population>();
	
    public uint getPopulationTotal()
    {
        uint count = 0;
        foreach (Population pop in populaces)
        {
            count += pop.numPeople;
        }
        return count;
    }

    public float getPopulationOverallHappiness()
    {
        float happinessAgg = 0f;
        uint popCount = getPopulationTotal();
        if (0 == popCount)
        {
            return happinessAgg;
        }
        foreach (Population pop in populaces)
        {
            happinessAgg += pop.happiness() * pop.numPeople / popCount;
        }
        return happinessAgg;
    }

    public void hire()
    {
        throw new NotImplementedException();
    }

    public void add(Population pop)
    {
        populaces.Add(pop);
    }

    public void remove(Population pop)
    {
        populaces.Remove(pop);
    }

    public uint numPopulations()
    {
        return (uint) populaces.Count;
    }

    public Population get(uint i)
    {
        return populaces[(int)i];
    }

    public float getTotalWealth()
    {
        float acc = 0;
        foreach (Population pop in populaces)
        {
            acc += pop.wealth;
        }
        return acc;
    }

    public float getEmployementRate()
    {
        uint people = this.getPopulationTotal();
        if (0 == people )
        {
            return 0f;
        }
        uint employed = 0;
        foreach (Population pop in populaces)
        {
            employed += pop.employed;
        }
        return (float) employed / people;
    }
}
