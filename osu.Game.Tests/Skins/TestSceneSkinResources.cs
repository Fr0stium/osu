// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Extensions;
using osu.Framework.Testing;
using osu.Game.Audio;
using osu.Game.Database;
using osu.Game.Skinning;
using osu.Game.Tests.Resources;
using osu.Game.Tests.Visual;

namespace osu.Game.Tests.Skins
{
    [HeadlessTest]
    public class TestSceneSkinResources : OsuTestScene
    {
        [Resolved]
        private SkinManager skins { get; set; }

        private ISkin skin;

        [BackgroundDependencyLoader]
        private void load()
        {
            var imported = skins.Import(new ImportTask(TestResources.OpenResource("Archives/ogg-skin.osk"), "ogg-skin.osk")).GetResultSafely();
            skin = imported.PerformRead(skinInfo => skins.GetSkin(skinInfo));
        }

        [Test]
        public void TestRetrieveOggSample() => AddAssert("sample is non-null", () => skin.GetSample(new SampleInfo("sample")) != null);
    }
}
