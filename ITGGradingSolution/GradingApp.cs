namespace ITGGradingSolution;
internal class GradingApp
{
    public void Run()
    {
        var dataLoader = new DataLoader();
        string path = "./data.txt";
        var students = dataLoader.LoadData(path);

        bool proceed = true;
        while (proceed)
        {
            Utils.PrintBanner("Type student number");
            // reads student uniqueNumber
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ProcessData(students, input);
            }
            else
            {
                proceed = false;
            }
        }
    }

    private void ProcessData(List<(Guid, string, string, string, string, string[], int[][], int, int, int)> students, string input)
    {
        for (int i = 0; i < students.Count; i++)
        {
            // student
            if (input == students[i].Item4)
            {
                ProcessSingleStudent(students[i]);
            }
        }
    }

    private void ProcessSingleStudent((Guid, string, string, string, string, string[], int[][], int, int, int) student)
    {
        Utils.PrintBanner($"{student.Item3} {student.Item2} ({student.Item5})");
        // subject
        for (int j = 0; j < student.Item6.Length; j++)
        {
            int sum = 0;
            Console.WriteLine($"Předmět: {student.Item6[j]}");
            Console.Write("\t");
            // single grade
            for (int k = 0; k < student.Item7[j].Length; k++)
            {
                sum += student.Item7[j][k];
                Console.Write(student.Item7[j][k] + ",");
            }
            Console.WriteLine();
            Console.WriteLine($"\tPrůměr: {sum / (double)student.Item7[j].Length}");
            Console.WriteLine($"\tPočet známek: {student.Item7[j].Length}");
        }
        Utils.PrintBanner("End of student");
    }
}
