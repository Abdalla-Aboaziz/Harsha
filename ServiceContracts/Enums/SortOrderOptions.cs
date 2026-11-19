namespace ServiceContracts.Enums;

public class SortOrderOptions : Enumeration
{
    public static readonly SortOrderOptions Ascending = new SortOrderOptions("Ascending", 1);
    public static readonly SortOrderOptions Descending = new SortOrderOptions("Descending", 2);
    public SortOrderOptions(string description, int value) : base(description, value)
    {
    }
}
