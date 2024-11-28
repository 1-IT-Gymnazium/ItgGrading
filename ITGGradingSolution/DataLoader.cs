namespace ITGGradingSolution;
internal class DataLoader
{
    // this is field
    private bool _error;
    public List<(Guid, string, string, string, string, string[], int[][], int, int, int)> LoadData(string path)
    {
        // data format: Id;FirstName;LastName;Number;Class;Grades;----------;Absence;Excused;Unexcused
        using StreamReader sr = new StreamReader(path);
        string? line = string.Empty;
        List<(Guid, string, string, string, string, string[], int[][], int, int, int)> students =
            new List<(Guid, string, string, string, string, string[], int[][], int, int, int)>();

        while ((line = sr.ReadLine()) != null)
        {
            Utils.PrintBanner("Another line starts");
            string[] lineArr = line.Split(';');

            if (lineArr.Length != 10)
            {
                Utils.PrintError("Not enough fields");
                continue;
            }
            Utils.PrintBanner("Parsing basics");

            Guid id = GuidInputWithErrorCheck(lineArr[0].Trim('\"'));
            string first = StringInputWithErrorCheck(lineArr[1]);
            string last = StringInputWithErrorCheck(lineArr[2]);
            string uniqueNumber = StringInputWithErrorCheck(lineArr[3]);
            string group = StringInputWithErrorCheck(lineArr[4]);

            string[] toSubjects = lineArr[5].Split('-');
            int subjectNumber = IntInputWithErrorCheck(toSubjects[0]);

            Utils.PrintBanner("Parsing grades");
            string[] subjects = new string[subjectNumber];
            int[][] grades = new int[subjectNumber][];

            LoadSubjectsAndGrades(toSubjects, subjects, grades);

            Utils.PrintBanner("Parsing absence");
            int absence = IntInputWithErrorCheck(lineArr[7]);
            int excused = IntInputWithErrorCheck(lineArr[8]);
            int nonExcused = IntInputWithErrorCheck(lineArr[9]);

            if (!_error)
            {
                students.Add((id, first, last, uniqueNumber, group, subjects, grades, absence, excused, nonExcused));
            }
            else
            {
                Utils.PrintError("There was some error");
            }

            Utils.PrintBanner("Another line parsed");
        }
        return students;
    }

    private void LoadSubjectsAndGrades(string[] toSubjects, string[] subjects, int[][] grades)
    {
        string[] gradesData = toSubjects[1].Split('/');
        for (int i = 0; i < gradesData.Length; i++)
        {
            string[] data = gradesData[i].Trim('\"').Split(':');
            subjects[i] = data[0];
            string[] toGrades = data[1].Trim().TrimStart('{').TrimEnd('}').Split(",");
            grades[i] = new int[toGrades.Length];
            for (int j = 0; j < toGrades.Length; j++)
            {
                if (int.TryParse(toGrades[j], out int gradeResult))
                {
                    grades[i][j] = gradeResult;
                }
            }
        }
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
