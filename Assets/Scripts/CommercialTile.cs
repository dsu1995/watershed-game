using UnityEngine;
using System.Collections;

public class CommercialTile : AbstractTile {

    private static uint JOB_LIMIT = 10;
    private static uint HIRE_RATE = 100;
    private static float PERCENT_HIRE = 0.1f;
    private static float PERCENT_FIRE = 0.50f;
    private static float WEALTH_REQUIRED = 0.15f;
    private static float POVERTY = WEALTH_REQUIRED/10;

    private uint hired;

	public override void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0)
	{
		base.Initialize(x, y, map, elevation, SurfaceWater, waterLevel);
	}

	public override float getPermeability()
	{
		return 0.5f;
	}

	public override string getType()
	{
		return "commercial";
	}

    protected override void Update()
    {
        base.Update();

        uint population = map.populace.getPopulationTotal();
        uint numPopulaces = map.populace.numPopulations();
        uint maxHirePerTile = (uint)Mathf.Max(0, Mathf.Ceil(Mathf.Max(HIRE_RATE, JOB_LIMIT - hired) / (numPopulaces + 1)));
        float totalWealth = map.populace.getTotalWealth();
        for (uint i = 0; i < numPopulaces; i++)
        {
            Population pop = map.populace.get(i);
            uint hireTarget = (uint)Mathf.Ceil(pop.numPeople * PERCENT_HIRE);
            uint hireNum = (uint)Mathf.Min(hireTarget, maxHirePerTile);
            uint failedHires = pop.giveJobs(hireNum);
            hired += (hireNum - failedHires);

            uint fireNum = 0;
            if (WEALTH_REQUIRED * population < totalWealth)
            {
                 fireNum += (uint)Mathf.Floor(pop.employed * PERCENT_FIRE);
            }
            if (pop.employed/(pop.numPeople+1) > 0.5f && pop.wealth < POVERTY * pop.numPeople)
            {
                fireNum += (uint)Mathf.Floor(pop.employed * PERCENT_FIRE);
            }
            fireNum = (uint)Mathf.Min(pop.employed, fireNum);
            hired -= fireNum;
            pop.takeJobs(fireNum);
        }
    }
}