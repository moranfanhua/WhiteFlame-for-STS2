using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using STS2RitsuLib;
using STS2RitsuLib.Patching.Core;
using STS2RitsuLib.Scaffolding.Content;
using System.Reflection;
using WhiteFlame.WhiteFlameCode.Cards;
using WhiteFlame.WhiteFlameCode.Characters;
using WhiteFlame.WhiteFlameCode.Relics;

namespace WhiteFlame.WhiteFlameCode;

[ModInitializer(nameof(WhiteFlameInitialization))]
public class Entry
{
    public const string ModId = "WhiteFlame";
    public static readonly Logger Logger = RitsuLibFramework.CreateLogger(ModId);

    public static void WhiteFlameInitialization()
    {
        var assembly = Assembly.GetExecutingAssembly();

        RitsuLibFramework.EnsureGodotScriptsRegistered(assembly, Logger);

        RitsuLibFramework.CreateContentPack(ModId)
            .Character<WhiteFlameCharacter>(entry => entry
                .AddStartingCard<WhiteFlameStrike>(4)
                .AddStartingCard<WhiteFlameDefend>(4)
                .AddStartingCard<SheildBattery>(1)
                .AddStartingCard<SheildCell>(1)
                .AddStartingRelic<Balisong>())
            .Apply();

        RitsuLibFramework.RegisterTouchOfOrobasRefinementMapping<Balisong, FactorBalisong>();
        RitsuLibFramework.RegisterArchaicToothTranscendenceMapping<SheildBattery, PhoenixKit>();
    }
}
