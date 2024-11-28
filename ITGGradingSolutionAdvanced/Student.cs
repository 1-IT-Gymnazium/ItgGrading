namespace ITGGradingSolutionAdvanced;
internal class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UniqueNumer { get; set; } = null!;
    public string Group { get; set; } = null!;
    public Dictionary<string, List<int>> SubjectGrades { get; set; } = [];
    public int Absence { get; set; }
    public int ExcusedAbsence { get; set; }
    public int NonExcusedAbsence { get; set; }
}
