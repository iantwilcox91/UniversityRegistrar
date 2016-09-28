using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace University
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString =  "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_tests;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_checkGetFunction()
    {
      Course newCourses = new Course("math","mat101");
      Assert.Equal("math", newCourses.GetName() );
      Assert.Equal("mat101", newCourses.GetNumber() );
    }
    [Fact]
    public void Test_CheckForEmptyDataBase()
    {
      //Arrange
      Course course = new Course ("Chemistry of the world", "Chem101");
      //Act
      int tableRows = Course.GetAll().Count;
      //Assert
      Assert.Equal( 0, tableRows);
    }
    [Fact]
    public void Test_Save_CanWeSaveACourseToTheDatabase()
    {
      //Arrange
      Course course = new Course ("Chemistry of undergraound", "Chem102");
      //Act
      course.Save();
      //Assert
      List<Course> allCourses = Course.GetAll();
      List<Course> testAllCourses = new List<Course> {course};
      Assert.Equal( testAllCourses, allCourses );
    }

    [Fact]
    public void Test_DeleteOnlyOneCourse()
    {
      //Arrange
      Course course = new Course ("Shakespear Literature" , "Eng200");
      course.Save();
      Course course1 = new Course ("Lovecraft Literature" , "Eng200");
      course1.Save();
      // Act
      course1.Delete();
      //Arrange
      List<Course> allCourses = Course.GetAll();
      List<Course> resultCourses = new List<Course> {course};
      //Assert
      Assert.Equal(allCourses , resultCourses);
    }


    public void Dispose()
    {
      Course.DeleteAll();
    }
  }
}
