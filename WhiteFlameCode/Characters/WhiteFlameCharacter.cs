using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Characters;
using STS2RitsuLib.Scaffolding.Godot;
using System;
using System.Collections.Generic;
using WhiteFlame.WhiteFlameCode.Pools;

namespace WhiteFlame.WhiteFlameCode.Characters;

[RegisterCharacter]
public class WhiteFlameCharacter : ModCharacterTemplate<WhiteFlameCardPool, WhiteFlameRelicPool, WhiteFlamePotionPool>
{
    public override Color NameColor => new(0.5f, 0.5f, 1f);
    public override Color EnergyLabelOutlineColor => new(0.5f, 0.5f, 1f);
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 80;
    public override int StartingGold => 99;

    public override CharacterAssetProfile AssetProfile => CharacterAssetProfiles.Merge(
        CharacterAssetProfiles.Ironclad(),
        new(
            Scenes: new(
                VisualsPath: "res://WhiteFlame/Scenes/xbh/WhiteFlameVisual.tscn",
                EnergyCounterPath: "res://WhiteFlame/Scenes/xbh/WhiteFlameEnergyCounter.tscn"
            ),
            Ui: new(
                IconTexturePath: "res://WhiteFlame/Scenes/xbh/WhiteFlameIcon.svg",
                CharacterSelectBgPath: "res://WhiteFlame/Scenes/xbh/WhiteFlameBackground.tscn",
                CharacterSelectIconPath: "res://WhiteFlame/Images/xbh/WhiteFlameSelect.png",
                CharacterSelectLockedIconPath: "res://WhiteFlame/Images/xbh/WhiteFlameSelectLocked.png"
            ),
            Vfx: new(),
            Audio: new(),
            Multiplayer: new()
        ));

    public override float AttackAnimDelay => 0f;
    public override float CastAnimDelay => 0f;

    protected override NCreatureVisuals? TryCreateCreatureVisuals()
        => RitsuGodotNodeFactories.CreateFromScenePath<NCreatureVisuals>(AssetProfile.Scenes!.VisualsPath!);

    public override List<string> GetArchitectAttackVfx() => [
        "vfx/vfx_attack_blunt",
        "vfx/vfx_heavy_blunt",
        "vfx/vfx_attack_slash",
        "vfx/vfx_bloody_impact",
        "vfx/vfx_rock_shatter"
    ];
}
