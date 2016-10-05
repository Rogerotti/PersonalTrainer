namespace Framework.Models
{
    /// <summary>
    /// Typ ilości pożywienia
    /// grams - standardowo dla produktów w postaci stałej gramy.
    /// milliliters - standardowo dla produktów w postaci ciekłej mililitry.
    /// piece - jedna sztuka z opakowania.
    /// package - jedno opakowanie produktu.
    /// </summary>
    public enum QuantityType
    {
        Grams,
        Milliliters,
        Piece,
        Package
    }
}
