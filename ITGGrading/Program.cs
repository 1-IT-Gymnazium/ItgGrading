bool proceed = true;
while (proceed)
{
    Console.WriteLine("#############################################");
    Console.WriteLine("############ Type student number ############");
    Console.WriteLine("#############################################");

    // reads student uniqueNumber
    string input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        proceed = false;
    }
    else
    {
        // data format: Id;FirstName;LastName;Number;Class;Grades;----------;Absence;Excused;Unexcused
        string path = "./data.txt";
        using StreamReader sr = new StreamReader(path);
        string? line = string.Empty;
        List<(Guid, string, string, string, string, string[], int[][], int, int, int)> students =
            new List<(Guid, string, string, string, string, string[], int[][], int, int, int)>();

        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine("#############################################");
            Console.WriteLine("############ Another line starts ############");
            Console.WriteLine("#############################################");
            bool error = false;
            string[] lineArr = line.Split(';');

            if (lineArr.Length == 10)
            {
                Console.WriteLine("#############################################");
                Console.WriteLine("############## Parsing basics ###############");
                Console.WriteLine("#############################################");
                Guid id = new Guid();

                if (Guid.TryParse(lineArr[0].Trim('\"'), out Guid result))
                {
                    id = result;
                }
                else
                {
                    error = true;
                }
                string first = lineArr[1];
                if (string.IsNullOrEmpty(first))
                {
                    error = true;
                }
                string last = lineArr[2];
                if (string.IsNullOrEmpty(last))
                {
                    error = true;
                }
                string uniqueNumber = lineArr[3];
                if (string.IsNullOrEmpty(uniqueNumber))
                {
                    error = true;
                }
                string group = lineArr[4];
                if (string.IsNullOrEmpty(group))
                {
                    error = true;
                }

                string[] toSubjects = lineArr[5].Split('-');
                int subjectNumber = 0;
                if (int.TryParse(toSubjects[0], out int subjectResult))
                {
                    subjectNumber = subjectResult;
                }
                else
                {
                    error = true;
                }

                Console.WriteLine("#############################################");
                Console.WriteLine("############## Parsing grades ###############");
                Console.WriteLine("#############################################");
                string[] subjects = new string[subjectNumber];
                int[][] grades = new int[subjectNumber][];

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

                Console.WriteLine("#############################################");
                Console.WriteLine("############# Parsing absence ###############");
                Console.WriteLine("#############################################");
                int absence = 0;
                if (int.TryParse(toSubjects[0], out int absenceResult))
                {
                    absence = absenceResult;
                }
                else
                {
                    error = true;
                }
                int excused = 0;
                if (int.TryParse(toSubjects[0], out int excusedResult))
                {
                    excused = absenceResult;
                }
                else
                {
                    error = true;
                }
                int nonExcused = 0;
                if (int.TryParse(toSubjects[0], out int nonExcuedResult))
                {
                    nonExcused = absenceResult;
                }
                else
                {
                    error = true;
                }
                if (!error)
                {
                    students.Add((id, first, last, uniqueNumber, group, subjects, grades, absence, excused, nonExcused));
                }
                else
                {
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!There was some error!!!!!!!!!!!!!!!!!!!!!!!");
                }

                Console.WriteLine("#############################################");
                Console.WriteLine("############ Another line parsed ############");
                Console.WriteLine("#############################################");
            }
            else
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!Not enough fields!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        for (int i = 0; i < students.Count; i++)
        {
            // student
            if (input == students[i].Item4)
            {
                Console.WriteLine("#############################################");
                Console.WriteLine($"########### {students[i].Item3} {students[i].Item2} ({students[i].Item5}) ###########");
                Console.WriteLine("#############################################");
                // subject
                for (int j = 0; j < students[i].Item6.Length; j++)
                {
                    int sum = 0;
                    Console.WriteLine($"Předmět: {students[i].Item6[j]}");
                    Console.Write("\t");
                    // single grade
                    for (int k = 0; k < students[i].Item7[j].Length; k++)
                    {
                        sum += students[i].Item7[j][k];
                        Console.Write(students[i].Item7[j][k] + ",");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"\tPrůměr: {sum / (double)students[i].Item7[j].Length}");
                    Console.WriteLine($"\tPočet známek: {students[i].Item7[j].Length}");
                }
                Console.WriteLine("#############################################");
                Console.WriteLine("############## End of student ##############");
                Console.WriteLine("#############################################");
            }
        }
    }
}
