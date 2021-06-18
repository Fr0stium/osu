// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Graphics.UserInterface
{
    public enum HoverSampleSet
    {
        [Description("default")]
        Default,

        [Description("button")]
        Button,

        [Description("softer")]
        Soft,

        [Description("toolbar")]
        Toolbar,

        [Description("songselect")]
        SongSelect,

        [Description("scrolltotop")]
        ScrollToTop
    }
}
