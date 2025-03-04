﻿namespace FoP_IMT.Client.Shared.Styling.Constants
{
    public class ColorConstants
    {
        public ColorConstants()
        {
            Primary = Secondary = Success = Danger = Info = Warning = Light = Dark = Black = White = BackgroundPrimary = BackgroundSecondary = BackgroundLight = TextPrimary = TextDark = TextMuted = "#000000";
        }

        public string Primary { get; set; }
        public string Secondary { get; set; } // Darker as primary for highlighting
        public string Success { get; set; }
        public string Danger { get; set; }
        public string Info { get; set; }
        public string Warning { get; set; }
        public string Light { get; set; }
        public string Dark { get; set; }
        public string Black { get; set; }
        public string White { get; set; }
        public string BackgroundPrimary { get; set; }
        public string BackgroundSecondary { get; set; }
        public string BackgroundLight { get; set; } // used for tab borders
        public string TextPrimary { get; set; }
        public string TextDark { get; set; }
        public string TextMuted { get; set; }
    }
}