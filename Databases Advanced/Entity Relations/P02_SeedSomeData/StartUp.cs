using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;
using System;

namespace P02_SeedSomeData
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (StudentSystemContext contex = new StudentSystemContext())
            {
                contex.Database.EnsureDeleted();

                contex.Database.Migrate();

                Seed(contex);
            }
        }

        private static void Seed(StudentSystemContext contex)
        {
            Course[] courses = new[] {
                         new Course {
                             Name = "C# DB Fundameltals",
                             Description = "This course is...",
                             StartDate = new DateTime(2018, 6, 1),
                             EndDate = new DateTime(2018, 8, 27),
                             Price = 390
                         },

                         new Course {
                             Name = "Tech Module",
                             Description = "Thech Module will...",
                             StartDate = new DateTime(2018, 5, 10),
                             EndDate = new DateTime(2018, 8, 5),
                             Price = 390
                         }
            };

            contex.Courses.AddRange(courses);

            Student[] students = new[] {
                         new Student {
                             Name = "Nikolai Iliev",
                             PhoneNumber = "0886842778",
                             RegisteredOn = new DateTime(2017, 12, 23)
                         },

                         new Student {
                             Name = "Victor Stoyanov",
                             PhoneNumber = "0886213123",
                             RegisteredOn = new DateTime(2017, 10, 12)
                         }
            };

            contex.Students.AddRange(students);

            Homework[] homeworks = new[] {
                         new Homework {
                             ContentType = ContentType.Pdf,
                             SubmissionTime = new DateTime(2018, 5, 15),
                             Student = students[0],
                             Course = courses[1]
                         },

                          new Homework {
                             ContentType = ContentType.Zip,
                             SubmissionTime = new DateTime(2018, 5, 23),
                             Student = students[1],
                             Course = courses[0]
                         }
            };

            contex.HomeworkSubmissions.AddRange(homeworks);

            Resource[] resources = new[] {
                         new Resource {
                             Name = "MyFirstResource",
                             ResourceType = ResourceType.Document,
                             CourseId = courses[0].CourseId
                         },

                          new Resource {
                             Name = "MySecondResource",
                             ResourceType = ResourceType.Presentation,
                             CourseId = courses[1].CourseId
                         }
            };


            contex.Resources.AddRange(resources);
            contex.SaveChanges();
        }
    }
}