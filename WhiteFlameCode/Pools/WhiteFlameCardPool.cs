using Godot;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Utils;

namespace WhiteFlame.WhiteFlameCode.Pools;

public class WhiteFlameCardPool : TypeListCardPoolModel
{
    public override string Title => "WhiteFlame";
    public override string EnergyColorName => "WhiteFlame";

    // 描述中使用的能量图标。大小为24x24。
    public override string? TextEnergyIconPath => "res://WhiteFlame/Images/energy_whiteflame.png";
    // tooltip和卡牌左上角的能量图标。大小为74x74。
    public override string? BigEnergyIconPath => "res://WhiteFlame/Images/energy_whiteflame_big.png";

    // 卡池的主题色（RGB）
    public override Color DeckEntryCardColor => new(0.5f, 0.5f, 1f);
    // 能量表盘文字轮廓颜色
    public override Color EnergyOutlineColor => new(0.5f, 0.5f, 1f);
    // 卡框换色（HSV色彩空间）
    private static readonly Material? _poolFrameMaterial = MaterialUtils.CreateHsvShaderMaterial(0.67f, 0.5f, 1f);
    public override Material? PoolFrameMaterial => _poolFrameMaterial;

    // 卡池为有色卡池
    public override bool IsColorless => false;
}
