using UnityEngine;
using System.Collections;

public class PipeWaterFlow : WaterFlowStrat {
    private AbstractTile pipeDestination;


    public PipeWaterFlow(AbstractTile pipeDestination)
    {
        this.pipeDestination = pipeDestination;
    }

    public override void flowWater(AbstractTile tile)
    {
        tile.sendWaterTo(pipeDestination, tile.waterLevel);
    }
}
