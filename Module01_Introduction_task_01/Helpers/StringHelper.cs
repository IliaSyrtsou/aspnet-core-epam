namespace Northwind.Helpers {
    public static class StringExtensions {
        public static int? ToNullableInt (
            this string str,
            int? defaultValue = null
        ) => int.TryParse (str, out var value) ? value : defaultValue;
    }
}