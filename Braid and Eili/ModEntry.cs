using HarmonyLib;
using KBraid.BraidEili.Cards;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;

    internal Harmony Harmony { get; }
    //internal IKokoroApi KokoroApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    internal IDeckEntry BraidDeck { get; }
    internal Color BraidColor => new Color(192, 201, 230);
    internal Color BraidCardTitleColor = new Color(0, 0, 0);

    internal ISpriteEntry EiliCardBorder { get; }

    internal static IReadOnlyList<Type> StarterCardTypes { get; } = [
        typeof(BigHit),
        typeof(Driveby),
    ];

    internal static IReadOnlyList<Type> CommonCardTypes { get; } = [
        typeof(Haymaker),
        typeof(LeftHook),
        typeof(LimiterOff),
        typeof(Pummel),
        typeof(ShoveIt),
        typeof(Bap),
        typeof(PlanAhead),
        typeof(StunBeam)
    ];

    internal static IReadOnlyList<Type> UncommonCardTypes { get; } = [

    ];

    internal static IReadOnlyList<Type> RareCardTypes { get; } = [

    ];

    internal static IEnumerable<Type> AllCardTypes
        => StarterCardTypes
            .Concat(CommonCardTypes);
            //.Concat(UncommonCardTypes)
            //.Concat(RareCardTypes);

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

        BraidDeck = Helper.Content.Decks.RegisterDeck("Braid", new()
        {
            Definition = new() { color = BraidColor, titleColor = BraidCardTitleColor },
            DefaultCardArt = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/cards/empty_backgroud.png")).Sprite,
            BorderSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/cardShared/border_braid.png")).Sprite,
            Name = this.AnyLocalizations.Bind(["character", "name"]).Localize
        });

        EiliCardBorder = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/cardShared/border_Eili.png"));

        //ShieldCostOff = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/ShieldCostOff.png"));
        //ShieldCostOn = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/ShieldCostOn.png"));

        //BatIcon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Icons/Bat.png"));
        //BatSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Bat.png"));

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
        BloodMirrorStatus = Helper.Content.Statuses.RegisterStatus("BloodMirror", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Status/BloodMirror.png")).Sprite,
                color = new("FF7F7F")
            },
            Name = this.AnyLocalizations.Bind(["status", "BloodMirror", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "BloodMirror", "description"]).Localize
        });
        TransfusionStatus = Helper.Content.Statuses.RegisterStatus("Transfusion", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Status/Transfusion.png")).Sprite,
                color = new("267F00")
            },
            Name = this.AnyLocalizations.Bind(["status", "Transfusion", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "Transfusion", "description"]).Localize
        });
        TransfusingStatus = Helper.Content.Statuses.RegisterStatus("Transfusing", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Status/Transfusing.png")).Sprite,
                color = new("267F00")
            },
            Name = this.AnyLocalizations.Bind(["status", "Transfusing", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "Transfusing", "description"]).Localize
        });
        */
        foreach (var cardType in AllCardTypes)
            AccessTools.DeclaredMethod(cardType, nameof(IBraidCard.Register))?.Invoke(null, [helper]);

        Helper.Content.Characters.RegisterCharacter("Braid", new()
        {
            Deck = BraidDeck.Deck,
            Description = this.AnyLocalizations.Bind(["character", "description"]).Localize,
            BorderSprite = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/panels/char_braid.png")).Sprite,
            StarterCardTypes = StarterCardTypes,
            NeutralAnimation = new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "neutral",
                Frames = [
                    Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/characters/Braid/braid_neutral_0.png")).Sprite,
                    Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/characters/Braid/braid_neutral_1.png")).Sprite,
                ]
            },
            MiniAnimation = new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "mini",
                Frames = [
                    Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/sprites/characters/Braid/braid_mini_0.png")).Sprite
                ]
            }
        });
    }
}
