using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Xunit;

namespace University
{
  public class Course
  {
    private int _id;
    private string _courseName;
    private string _courseNumber;

    public Course(string courseName, string courseNumber, int id = 0)
    {
      _id = id;
      _courseName = courseName ;
      _courseNumber = courseNumber;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _courseName;
    }

    public string GetNumber()
    {
      return _courseNumber;
    }

    public void SetName(string newName)
    {
      _courseName = newName;
    }

    public void SetNumber(string newNumber)
    {
      _courseNumber = newNumber;
    }

    public override bool Equals(System.Object otherCourses)
    {
      if (!(otherCourses is Course))
      {
        return false;
      }
      else
      {
        Course newCourses = (Course) otherCourses;
        bool idEquality = (this.GetId() == newCourses.GetId());
        bool nameEquality = (this.GetName() == newCourses.GetName());
        return (idEquality && nameEquality);
      }
    }
    public static List<Course> GetAll()
    {
      List<Course> courseList = new List<Course> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      string query = "SELECT * FROM courses;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;
      string courseName = null;
      while ( rdr.Read() )
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        courseName = rdr.GetString(2);
        Course course = new Course(name, courseName, id);
        courseList.Add(course);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return courseList;
    }
    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students_courses;", conn);
      cmd.ExecuteNonQuery();

      SqlCommand cmd1 = new SqlCommand("DELETE FROM courses;", conn);
      cmd1.ExecuteNonQuery();
      conn.Close();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "INSERT INTO courses (course_name, course_number) OUTPUT INSERTED.id VALUES (@courseName, @courseNumber);";
      SqlCommand cmd = new SqlCommand(query,conn);
      SqlParameter pam = new SqlParameter ("@courseName", this._courseName);
      cmd.Parameters.Add(pam);
      SqlParameter pam2 = new SqlParameter ("@courseNumber", this._courseNumber);
      cmd.Parameters.Add(pam2);
      SqlDataReader rdr = cmd.ExecuteReader();

      while ( rdr.Read() )
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlParameter pam = new SqlParameter ("@courseId", this._id);
      string query = "DELETE FROM students_courses WHERE courses_id = @courseId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.Add(pam);
      cmd.ExecuteNonQuery();

      SqlParameter pam2 = new SqlParameter ("@courseId", this._id);
      string query2 = "DELETE FROM courses WHERE id = @courseId;";
      SqlCommand cmd2 = new SqlCommand(query2, conn);
      cmd2.Parameters.Add(pam2);
      cmd2.ExecuteNonQuery();

      conn.Close();
    }
    public List<Student> ViewStudents()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN students_courses ON (courses.id = students_courses.courses_id) JOIN students ON (students_courses.students_id = students.id) WHERE courses.id = @CourseId;", conn);
      SqlParameter CourseIdParam = new SqlParameter();
      CourseIdParam.ParameterName = "@CourseId";
      CourseIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(CourseIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Student> studentList = new List<Student>{};

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime enrollment = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, enrollment, studentId);
        Console.WriteLine(studentId);
        Console.WriteLine(studentName );
        Console.WriteLine(enrollment);
        studentList.Add(newStudent);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return studentList;
    }
    public void AddStudent(Student student)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string query = "INSERT INTO students_courses (students_id , courses_id) VAlUES (@studentId ,@courseId );";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter pamStudentId = new SqlParameter("@studentId", student.GetId() );
      cmd.Parameters.Add(pamStudentId);
      SqlParameter pamCourseId = new SqlParameter("@courseId", this._id );
      cmd.Parameters.Add(pamCourseId);
      cmd.ExecuteNonQuery();

      conn.Close();
    }
    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "SELECT * FROM courses WHERE id = @courseId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter paramId = new SqlParameter("@courseId", id );
      cmd.Parameters.Add(paramId);
      SqlDataReader rdr = cmd.ExecuteReader();

      int courseId = 0;
      string courseName = null;
      string courseNumber = null;
      while(rdr.Read())
      {
        courseId = rdr.GetInt32(0);
        courseName = rdr.GetString(1);
        courseNumber = rdr.GetString(2);
      }
      Course course = new Course( courseName, courseNumber, courseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return course;
    }

  }
}
