using System.Runtime.InteropServices.JavaScript;

namespace ITGGradingSolutionAdvanced;
internal class GradingApp
{
    readonly string _path = "./data.txt";
    bool _proceed = true;

    public void Run()
    {
        var dataLoader = new DataLoader();
        var students = dataLoader.LoadData(_path);
        while (_proceed)
        {
            Utils.PrintBanner("Type student number");
            // reads student uniqueNumber
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ProcessData(students, input);
            }
            else
            {
                _proceed = false;
            }
        }
    }

    private void ProcessData(List<Student> students, string input)
    {
        foreach (var s in students.Where(x => x.UniqueNumer == input))
        {
            ProcessSingleStudent(s);
        }
    }

    private void ProcessSingleStudent(Student student)
    {
        Utils.PrintBanner($"{student.LastName} {student.FirstName} ({student.Group})");
        // subject
        foreach (var subject in student.SubjectGrades)
        {
            PrintGradeResults(subject.Key, subject.Value);
        }
        Utils.PrintBanner("End of student");
    }

    private static void PrintGradeResults(string subject, List<int> grades)
    {
        Console.WriteLine($"Předmět: {subject}");
        Console.WriteLine($"\t{string.Join(',', grades)}");
        Console.WriteLine($"\tPrůměr: {grades.Average()}");
        Console.WriteLine($"\tPočet známek: {grades.Count}");
    }
}
