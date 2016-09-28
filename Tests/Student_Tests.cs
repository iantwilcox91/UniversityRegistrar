using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace University
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString =  "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_tests;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_checkGetFunction()
    {
      DateTime enrollment = new DateTime(2008,8,4);
      Student newStudent = new Student("Johnny", enrollment );

      DateTime aDate = new DateTime(2008,8,4);
      Assert.Equal("Johnny", newStudent.GetName() );
      Assert.Equal(aDate, newStudent.GetEnrollment() );
    }

    [Fact]
    public void Test_CheckForEmptyDataBase()
    {
      //Arrange
      //Act
      int tableRows = Student.GetAll().Count;
      //Assert
      Assert.Equal( 0, tableRows);
    }
    [Fact]
    public void Test_Save_CanWeSaveACourseToTheDatabase()
    {
      //Arrange
      DateTime enrollment = new DateTime(2008,8,4);
      Student student =  new Student ("Sid", enrollment);
      //Act
      student.Save();
      //Assert
      List<Student> allStudent = Student.GetAll();
      List<Student> testStudent = new List<Student> {student};
      Assert.Equal( testStudent, allStudent );
    }

    [Fact]
    public void Test_DeleteOnlyOneStudent()
    {
      //Arrange
      DateTime enrollment = new DateTime(2008,8,4);
      Student student =  new Student ("Jerry", enrollment);
      student.Save();
      DateTime enrollment1 = new DateTime(2008,8,4);
      Student student1 =  new Student ("Tom", enrollment1);
      student1.Save();
      // Act
      student1.Delete();
      //Arrange
      List<Student> allStudents = Student.GetAll();
      List<Student> resultStudents = new List<Student> {student};
      //Assert
      Assert.Equal(resultStudents , allStudents);
    }
    [Fact]
    public void Test_Find_WillReturnAStudent()
    {
      //Arrange
      DateTime enrollment = new DateTime(2008,8,4);
      Student student =  new Student ("Ian", enrollment);
      student.Save();
      DateTime enrollment2 = new DateTime(2008,8,4);
      Student student2 =  new Student ("Jonathan", enrollment2);
      student2.Save();
      //Act
      Student foundStudent = Student.Find( student2.GetId() );
      //Assert
      Assert.Equal(student2, foundStudent);

    }

    public void Dispose()
    {
      Course.DeleteAll();
      Student.DeleteAll();
    }



  }
}
