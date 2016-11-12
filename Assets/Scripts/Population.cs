using UnityEngine;
using System.Collections;

public class Population {
    private static float EMPLOYEMENT_WEIGHTING = 30;
    private static float WEALTH_WEIGHTING = 30;
    private static float WEALTH_MID = 100;
    private static float WATER_WEIGHTING = 40;
    private static float TOTAL_WEIGHTING = EMPLOYEMENT_WEIGHTING + WEALTH_WEIGHTING + WATER_WEIGHTING;
    private static float BASE_GROWTH_RATE = 1;

    public uint numPeople
    {
        get; private set;
    }
    private uint employed;
    private float wealth;
    public uint popCapacity;

    private AbstractTile home;

    Population(AbstractTile home, uint popCapacity)
    {
        numPeople = 0;
        employed = 0;
        wealth = 0f;
        this.home = home;
        this.popCapacity = popCapacity;
    }

    public float taxRate = 0.2f;

    public float salary = 20f;

    public float collectTax()
    {
        float tax = wealth * employed * taxRate;
        wealth -= tax;
        return tax;
    }

    public void work()
    {
        wealth += employed * salary;
    }

    public void updatePopulation()
    {
        float happyModifier = (happiness() * 2f) - 1f;
        uint unemployed = numPeople - employed;
        if (happyModifier < 0f)
        {
            numPeople += (uint)Mathf.Ceil(unemployed * happyModifier);
        } else
        {
            numPeople = (uint)Mathf.Min(popCapacity, numPeople + numPeople*BASE_GROWTH_RATE*happyModifier);
        }
    }

    public uint giveJobs(uint jobs)
    {
        employed += jobs;
        uint surplus = 0;
        if (employed > numPeople)
        {
            surplus = employed - numPeople;
            employed = numPeople;
        }
        return surplus;
    }

    public uint takeJobs(uint jobs)
    {
        uint shortage = 0;
        if (employed < jobs)
        {
            shortage = jobs - employed;
            employed = 0;
        } else
        {
            employed -= jobs;
        }
        return shortage;
    }

    public float happiness()
    {
        float employmentSatisfaction = EMPLOYEMENT_WEIGHTING * (employed / numPeople);
        float wealthSatisfaction = WEALTH_WEIGHTING * ( wealth / (wealth + WEALTH_MID) );
        float waterSatisfaction = WATER_WEIGHTING * ((home.displayedWaterLevel() > 0) ? 0f : 1f);
        return (employmentSatisfaction + wealthSatisfaction + waterSatisfaction) / TOTAL_WEIGHTING;
    }

}