using System;
using System.Collections.Generic;

namespace Delegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Exam> Exams = new List<Exam>();
            AddExamFromConsole(Exams);
            ShowExams(Exams);
        }

        static bool ConvertToDate(string format, string dateString)
        {
            var provider = System.Globalization.CultureInfo.InvariantCulture;

            try
            {
                DateTime test = DateTime.ParseExact(dateString, format, provider);
                if (test>DateTime.Now)
                {
                    throw new FormatException();
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        static void AddExamFromConsole(List<Exam> Exams)
        {

            
            string countStr;
            int count;
            do
            {
                Console.WriteLine("Nece isci daxil etmek isteyirsiniz?");
                countStr = Console.ReadLine();
            } while (!int.TryParse(countStr, out count) || count < 0);

            for (int i = 0; i < count; i++)
            {
                // telebe adi
                Console.WriteLine($"{i+1} Telebe adi daxil edin");
                string name = Console.ReadLine();

                // fenn adi
                Console.WriteLine($"{i + 1} Fenn adi daxil edin");
                string subject = Console.ReadLine();

                // Point 
                string pointStr;
                int point;
                do
                {
                    Console.WriteLine($"{i + 1} Qiymeti daxil edin");
                    pointStr = Console.ReadLine();
                } while (!int.TryParse(pointStr, out point));
                
                // baslama vaxti
                DateTime startDate;
                string startDateString;
                string format = "yyyy/MM/dd hh:mm";
                do
                {
                    Console.WriteLine($"{i + 1} Baslama vaxtini daxil edin (yyyy/mm/dd hh:mm)");
                    startDateString = Console.ReadLine();
                      
                    

                } while (!ConvertToDate(format, startDateString));
                startDate=DateTime.Parse(startDateString);

                // bitme vaxti
                string endDateString;
                DateTime endDate;
                do
                {
                    Console.WriteLine($"{i + 1} Bitme vaxtini daxil edin (yyyy/mm/dd hh:mm)");
                    endDateString = Console.ReadLine();
                } while (!ConvertToDate(format,endDateString));
                endDate = DateTime.Parse(endDateString);

                // new exam
                Exam exam = new Exam()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Point = point,
                    StudentName = name,
                    Subject = subject
                };

                // adding list
                Exams.Add(exam);



            }
        }

        static void ShowExams(List<Exam> Exams)
        {
            Console.WriteLine("\nPointi 50 den cox olan exam-ler");
            List<Exam> exams1 = Exams.FindAll(x => x.Point > 50);

            foreach (var item in exams1)
            {
                Console.WriteLine(item.StudentName);
            }
            Console.WriteLine("\nSon 1 hefte icerisinde olan exam-ler ");
            var exams2 = Exams.FindAll(e => e.StartDate < DateTime.Now && e.StartDate > DateTime.Now.AddDays(-7));

            foreach (var item in exams2)
            {
                Console.WriteLine(item.StartDate);
            }


            Console.WriteLine("1 saatdan uzun olan omtahanlar");
            // 10:45
            // 12:30
           var oneHour= Exams.FindAll(x => (x.StartDate- x.EndDate).Minutes > 60);
            int minutes;
            
            foreach (var item in oneHour)
            {
               string a= (item.EndDate - item.StartDate).Minutes.ToString();
               int.TryParse(a, out minutes);
                Console.WriteLine($"{minutes/60} saat - {minutes%60} deqiqe ");
            }
            
        }
    }
}
