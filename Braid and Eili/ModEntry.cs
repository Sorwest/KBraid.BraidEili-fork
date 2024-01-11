using HarmonyLib;
using KBraid.BraidEili.Cards;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static HarmonyLib.Code;

namespace KBraid.BraidEili;
public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;

    internal Harmony Harmony { get; }
    //internal IKokoroApi KokoroApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
    internal IDeckEntry BraidDeck { get; }
    internal IDeckEntry EiliDeck { get; }
    internal static Color BraidColor => new(0.753, 0.788, 0.902);
    internal static Color EiliColor => new("42add1");
    internal static Color BraidCardTitleColor => new("191e26");
    internal static Color EiliCardTitleColor => new("52d4ff");
    internal ISpriteEntry BasicBackground { get; }

    internal IList<string> faceSprites { get; } = [
        "blink",
        "crystallized",
        "defeat",
        "ending",
        "eyes_closed",
        "hug",
        "mini",
        "neutral",
        "serious",
        "serious_a",
        "serious_b",
        "serious_c",
        "shout",
        "shout_a",
        "shout_b",
        "shout_c",
        "unamused",
        "cheeky",
        "cheeky_a",
        "concerned",
        "happy",
        "manic",
        "sad"
    ];
    internal IList<string> charNames { get; } = [
        "braid",
        "eili"
    ];
    internal Dictionary<string, ISpriteEntry> Sprites { get; } = [

    ];
    internal static IReadOnlyList<Type> BraidStarterCardTypes { get; } = [
        typeof(BigHit),
        typeof(Driveby),
    ];

    internal static IReadOnlyList<Type> BraidCommonCardTypes { get; } = [
        typeof(Haymaker),
        typeof(LeftHook),
        typeof(LimiterOff),
        typeof(Pummel),
        typeof(ShoveIt)
    ];

    internal static IReadOnlyList<Type> EiliStarterCardTypes { get; } = [
        typeof(Bap),
        typeof(PlanAhead),
    ];

    internal static IReadOnlyList<Type> EiliCommonCardTypes { get; } = [
        typeof(StunBeam)
    ];

    internal static IReadOnlyList<Type> BraidUncommonCardTypes { get; } = [

    ];

    internal static IReadOnlyList<Type> EiliUncommonCardTypes { get; } = [

    ];

    internal static IReadOnlyList<Type> BraidRareCardTypes { get; } = [

    ];

    internal static IReadOnlyList<Type> EiliRareCardTypes { get; } = [

    ];

    internal static IEnumerable<Type> BraidCardTypes
        => BraidStarterCardTypes
        .Concat(BraidCommonCardTypes)
        .Concat(BraidUncommonCardTypes)
        .Concat(BraidRareCardTypes);
    internal static IEnumerable<Type> EiliCardTypes
        => EiliStarterCardTypes
        .Concat(EiliCommonCardTypes)
        .Concat(EiliUncommonCardTypes)
        .Concat(EiliRareCardTypes);

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
        Harmony = new(package.Manifest.UniqueName);
        //KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        //KokoroApi.RegisterTypeForExtensionData(typeof(AHurt));
        //KokoroApi.RegisterTypeForExtensionData(typeof(AAttack));
        //KokoroApi.RegisterCardRenderHook(new SpacingCardRenderHook(), 0);

        this.AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"assets/locales/Cards_{locale}.json").OpenRead()
        );
        this.Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyLocalizations)
        );
        BasicBackground = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/cards/empty_backgroud.png"));

        foreach (string name in charNames)
        {
            //talking sprites such as happy and serious
            foreach (string face in faceSprites)
            {
                IFileInfo file;
                for (int frame = 0; frame < 10; frame++)
                {
                    file = package.PackageRoot.GetRelativeFile($"assets/sprites/characters/{name}/{name}_{face}_{frame}.png");
                    if (file.Exists)
                        Sprites.Add(key: $"{name}_{face}_{frame}", value: Helper.Content.Sprites.RegisterSprite(file));
                    else
                        break;
                }
                // Panels
                file = package.PackageRoot.GetRelativeFile($"assets/sprites/panels/char_{name}.png");
                if (file.Exists && !Sprites.ContainsKey($"{name}_panel"))
                    Sprites.Add(key: $"{name}_panel", value: Helper.Content.Sprites.RegisterSprite(file));
                // Card Borders
                file = package.PackageRoot.GetRelativeFile($"assets/sprites/cardShared/border_{name}.png");
                if (file.Exists && !Sprites.ContainsKey($"{name}_border"))
                    Sprites.Add(key: $"{name}_border", value: Helper.Content.Sprites.RegisterSprite(file));

            }
            //other sprites like full body and card borders

        }
        BraidDeck = Helper.Content.Decks.RegisterDeck("Braid", new()
        {
            Definition = new() { color = BraidColor, titleColor = BraidCardTitleColor },
            DefaultCardArt = BasicBackground.Sprite,
            BorderSprite = Sprites["braid_border"].Sprite,
            Name = this.AnyLocalizations.Bind(["character", "Braid", "name"]).Localize
        });

        EiliDeck = Helper.Content.Decks.RegisterDeck("Eili", new()
        {
            Definition = new() { color = EiliColor, titleColor = EiliCardTitleColor },
            DefaultCardArt = BasicBackground.Sprite,
            BorderSprite = Sprites["eili_border"].Sprite,
            Name = this.AnyLocalizations.Bind(["character", "Eili", "name"]).Localize
        });

        /*BleedingStatus = Helper.Content.Statuses.RegisterStatus("Bleeding", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Status/Bleeding.png")).Sprite,
                color = new("BE0000")
            },
            Name = this.AnyLocalizations.Bind(["status", "Bleeding", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "Bleeding", "description"]).Localize
        });
        */
        foreach (var cardType in BraidCardTypes)
            AccessTools.DeclaredMethod(cardType, nameof(IModdedCard.Register))?.Invoke(null, [helper]);
        foreach (var cardType in EiliCardTypes)
            AccessTools.DeclaredMethod(cardType, nameof(IModdedCard.Register))?.Invoke(null, [helper]);

        Helper.Content.Characters.RegisterCharacter("Braid", new()
        {
            Deck = BraidDeck.Deck,
            Description = this.AnyLocalizations.Bind(["character", "Braid", "description"]).Localize,
            BorderSprite = Sprites["braid_panel"].Sprite,
            StarterCardTypes = BraidStarterCardTypes,
            NeutralAnimation = new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "neutral",
                Frames = [
                    Sprites["braid_neutral_0"].Sprite,
                    Sprites["braid_neutral_1"].Sprite,
                    Sprites["braid_neutral_0"].Sprite,
                    Sprites["braid_neutral_1"].Sprite,
                ]
            },
            MiniAnimation = new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "mini",
                Frames = [
                   Sprites["braid_mini_0"].Sprite
                ]
            },
        });
        Helper.Content.Characters.RegisterCharacter("Eili", new()
        {
            Deck = EiliDeck.Deck,
            Description = this.AnyLocalizations.Bind(["character", "Eili", "description"]).Localize,
            BorderSprite = Sprites["eili_panel"].Sprite,
            StarterCardTypes = EiliStarterCardTypes,
            NeutralAnimation = new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "neutral",
                Frames = [
                    Sprites["eili_neutral_0"].Sprite,
                    Sprites["eili_neutral_1"].Sprite,
                    Sprites["eili_neutral_0"].Sprite,
                    Sprites["eili_neutral_1"].Sprite,
                ]
            },
            MiniAnimation = new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "mini",
                Frames = [
                   Sprites["eili_mini_0"].Sprite
                ]
            },
        });
        //Register Extra Animations
        Helper.Content.Characters.RegisterCharacterAnimation(
            "gameover",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "gameover",
                Frames = [
                   Sprites["braid_defeat_0"].Sprite
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "gameover",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "gameover",
                Frames = [
                   Sprites["eili_defeat_0"].Sprite
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "squint",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "squint",
                Frames = [

                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "squint",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "squint",
                Frames = [

                ]
            }
        );
    }
}
