using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Xunit;

namespace University
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _enrollment;

    public Student(string name, DateTime enrollment , int id = 0)
    {
      _id = id;
      _name = name;
      _enrollment = enrollment;
    }
    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public DateTime GetEnrollment()
    {
      return _enrollment;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = (this.GetId() == newStudent.GetId());
        bool nameEquality = (this.GetName() == newStudent.GetName());
        return (idEquality && nameEquality);
      }
    }

    public static List<Student> GetAll()
    {
      List<Student> studentList = new List<Student> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      string query = "SELECT * FROM students;";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;
      DateTime date = new DateTime(2009,8,4);
//date time cant be null ^^^ added for function
      while ( rdr.Read() )
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        date = rdr.GetDateTime(2);
        Student student = new Student(name, date, id);
        studentList.Add(student);
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
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      string query = "INSERT INTO students (name, enrollment) OUTPUT INSERTED.id VALUES (@name, @enrollment);";
      SqlCommand cmd = new SqlCommand(query,conn);
      SqlParameter pam = new SqlParameter ("@name", this._name);
      cmd.Parameters.Add(pam);
      SqlParameter pam2 = new SqlParameter ("@enrollment", this._enrollment);
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
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students_courses;", conn);
      cmd.ExecuteNonQuery();

      SqlCommand cmd1 = new SqlCommand("DELETE FROM students;", conn);
      cmd1.ExecuteNonQuery();
      conn.Close();
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlParameter pam = new SqlParameter ("@studentsId", this._id);
      string query = "DELETE FROM students_courses WHERE students_id = @studentsId;";
      SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.Add(pam);
      cmd.ExecuteNonQuery();

      SqlParameter pam2 = new SqlParameter ("@student_id", this._id);
      SqlCommand cmd1 = new SqlCommand("DELETE FROM students WHERE id = @student_id ;", conn);
      cmd1.Parameters.Add(pam2);
      cmd1.ExecuteNonQuery();

      conn.Close();
    }

    public List<Courses> ViewCourses()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN students_courses ON (students.id = students_courses.students_id) JOIN courses ON (students_courses.courses_id = courses.id) WHERE students.id = @StudentId;", conn);
      SqlParameter StudentIdParam = new SqlParameter();
      StudentIdParam.ParameterName = "@StudentId";
      StudentIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(StudentIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Courses> courseList = new List<Courses>{};

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        courseList.Add(newCourse);
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
    public void AddCourse(Course course)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      string query = "INSERT INTO students_courses (students_id , courses_id) VAlUES (@studentId ,@courseId );";
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlParameter pamCourseId = new SqlParameter("@courseId", course.GetId() );
      cmd.Parameters.Add(pamCourseId);
      SqlParameter pamStudentId = new SqlParameter("@studentId", this._id );
      cmd.Parameters.Add(pamStudentId);
      cmd.ExecuteNonQuery();

      conn.Close();
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

  }
}
