namespace ITGGradingSolutionAdvanced;
internal class DataLoader
{
    // this is field
    private bool _error;
    public List<Student> LoadData(string path)
    {
        // data format: Id;FirstName;LastName;Number;Class;Grades;----------;Absence;Excused;Unexcused
        using var sr = new StreamReader(path);
        var line = string.Empty;
        var students = new List<Student>();

        while ((line = sr.ReadLine()) != null)
        {
            Utils.PrintBanner("Another line starts");
            var lineArr = line.Split(';');

            if (lineArr.Length != 10)
            {
                Utils.PrintError("Not enough fields");
                continue;
            }
            Utils.PrintBanner("Parsing basics");
            students.Add(new()
            {
                Id = GuidInputWithErrorCheck(lineArr[0].Trim('\"')),
                FirstName = StringInputWithErrorCheck(lineArr[1]),
                LastName = StringInputWithErrorCheck(lineArr[2]),
                UniqueNumer = StringInputWithErrorCheck(lineArr[3]),
                Group = StringInputWithErrorCheck(lineArr[4]),
                SubjectGrades = LoadSubjectsAndGrades(lineArr[5].Split('-')),
                Absence = IntInputWithErrorCheck(lineArr[7]),
                ExcusedAbsence = IntInputWithErrorCheck(lineArr[8]),
                NonExcusedAbsence = IntInputWithErrorCheck(lineArr[9]),
            });

            if (_error)
            {
                Utils.PrintError("There was some error");
            }

            Utils.PrintBanner("Another line parsed");
        }
        return students;
    }

    private Dictionary<string, List<int>> LoadSubjectsAndGrades(string[] toSubjects)
    {
        var result = new Dictionary<string, List<int>>();
        var gradesData = toSubjects[1].Split('/');
        foreach (var sub in gradesData)
        {
            var data = sub.Trim('\"').Split(':');
            var toGrades = data[1].Trim().TrimStart('{').TrimEnd('}').Split(",");
            result.Add(data[0], toGrades.Select(IntInputWithErrorCheck).ToList());
        }
        return result;
    }

    private string StringInputWithErrorCheck(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            _error = true;
        }
        return input;
    }

    private int IntInputWithErrorCheck(string input)
    {
        int result = Utils.SafelyConvertToInt(input);
        if (result == int.MinValue)
        {
            _error = true;
        }
        return result;
    }

    private Guid GuidInputWithErrorCheck(string input)
    {
        if (Guid.TryParse(input, out Guid result))
        {
            return result;
        }
        _error = true;
        return Guid.Empty;
    }
}
