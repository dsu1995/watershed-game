using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public void add(Population pop)
    {
        populaces.Add(pop);
    }

    public void remove(Population pop)
    {
        populaces.Remove(pop);
    }
}
