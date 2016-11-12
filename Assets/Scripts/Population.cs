using UnityEngine;
using System.Collections;

public class Population {
    private static float EMPLOYEMENT_WEIGHTING = 30;
    private static float WEALTH_WEIGHTING = 30;
    private static float WEALTH_MID = 100;
    private static float WATER_WEIGHTING = 40;
    private static float TOTAL_WEIGHTING = EMPLOYEMENT_WEIGHTING + WEALTH_WEIGHTING + WATER_WEIGHTING;
    private static float BASE_GROWTH_RATE = 1;

    public int numPeople
    {
        get; private set;
    }
    private int employed;
    private float wealth;
    public int popCapacity;

    private AbstractTile home;

    Population(AbstractTile home, int popCapacity)
    {
        numPeople = 0;
        employed = 0;
        wealth = 0f;
        this.home = home;
        this.popCapacity = popCapacity;
    }

    public float taxRate;

    public float salary;

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
        int unemployed = numPeople - employed;
        if (happyModifier < 0f)
        {
            numPeople += (int)Mathf.Ceil(unemployed * happyModifier);
        } else
        {
            numPeople = (int)Mathf.Min(popCapacity, numPeople + numPeople*BASE_GROWTH_RATE*happyModifier);
        }
    }

    public int giveJobs(int jobs)
    {
        employed += jobs;
        int surplus = 0;
        if (employed > numPeople)
        {
            surplus = employed - numPeople;
            employed = numPeople;
        }
        return surplus;
    }

    public int takeJobs(int jobs)
    {
        employed -= jobs;
        int shortage = 0;
        if (employed < 0)
        {
            shortage = -1 * employed;
            employed = 0;
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