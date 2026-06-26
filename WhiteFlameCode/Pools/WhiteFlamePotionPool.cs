using STS2RitsuLib.Scaffolding.Content;

namespace WhiteFlame.WhiteFlameCode.Pools;

public class WhiteFlamePotionPool : TypeListPotionPoolModel
{
    public override string? TextEnergyIconPath => "res://WhiteFlame/Images/energy_whiteflame.png";
    public override string? BigEnergyIconPath => "res://WhiteFlame/Images/energy_whiteflame_big.png";
    public override string EnergyColorName => "WhiteFlame";
}
