using UnityEngine;
using System.Collections;

public class ResidentialTile : AbstractTile {

    Population population;

    void Start()
    {
        population = new Population(this, 10000);
        map.populace.add(population);
    }

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