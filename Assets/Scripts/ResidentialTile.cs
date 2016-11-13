using UnityEngine;
using System.Collections;

public class ResidentialTile : AbstractTile {

    private static float DEFAULT_WATER_THRESHOLD_LEVEL = 20f;

    Population population;

    public ResidentialTile()
    {
        population = new Population(this, 10000);
    }

    public void Initialize(uint x, uint y, TileMap map, float elevation, GameObject SurfaceWater, float waterLevel = 0)
    {
        map.populace.add(population);
        base.Initialize(x, y, map, elevation, SurfaceWater, waterLevel, DEFAULT_WATER_THRESHOLD_LEVEL);
    }

    public override float getPermeability()
	{
		return 0.5f;
	}

	public override string getType()
	{
		return "residential";
	}

    protected override void Update()
    {
        base.Update();

        // Maybe we should switch the order of these two
        population.work();
        population.updatePopulation();
    }

    public override float income()
    {
        return base.income() + population.collectTax();
    }

    void onDestroy()
    {
        map.populace.remove(population);
    }
}