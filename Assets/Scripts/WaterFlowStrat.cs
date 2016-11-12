using UnityEngine;
using System.Collections;

public abstract class WaterFlowStrat {

    public virtual void flowWater(AbstractTile tile)
    {
        tile.flowWater();
    }

}
