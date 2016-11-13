using UnityEngine;
using System.Collections;

public class ResidentialTile : AbstractTile {

    Population population;

	public void Initialize(uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
	{
		base.Initialize(x, y, map, elevation, waterLevel);
	}

    public void Initialize(out Population population, uint x, uint y, TileMap map, float elevation, float waterLevel = 0)
    {
        population = new Population(this, 10000);
        this.population = population;
        base.Initialize(x, y, map, elevation, waterLevel);
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
}