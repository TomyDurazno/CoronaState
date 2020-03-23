namespace CoronaManager
{ 
    public static class ChartColors
    {
        public const string Red = "#dc3545";
        public const string SoftBlue = "#36A2EB";
        public const string Teal = "#20c997";
        public const string Cyan = "#17a2b8";
        //public const string White = "#fff";
        public const string SoftRed = "#FF6384";
        public const string Gray_dark = "#343a40";
        public const string Primary = "#3f6ad8";
        public const string Secondary = "#6c757d";
        public const string Success = "#3ac47d";
        public const string Info = "#16aaff";
        public const string Warning = "#f7b924";
        public const string Danger = "#d92550";
        public const string Light = "#eee";
        public const string Dark = "#343a40";
        public const string Focus = "#444054";
        public const string Alternate = "#794c8a";
        public const string Gray = "#6c757d";
        public const string SoftGreen = "#4BC0C0";
        public const string SoftYellow = "#FFCD56";
        public const string SoftGray = "#C9CBCF";
        public const string Indigo = "#6610f2";
        public const string Blue = "#007bff";
        public const string Orange = "#fd7e14";
        public const string Green = "#28a745";
        public const string Purple = "#6f42c1";
        public const string Yellow = "#ffc107";
        public const string Pink = "#e83e8c";

        public static string [] GetAllColors() => Helpers.GetAllFields(typeof(ChartColors));
    }
}
