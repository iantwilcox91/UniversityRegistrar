using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;

namespace University
{
  public class RoutingView
  {
    public static Dictionary<string, object> GetStudentCourses()
    {
      List<Student> studentList = Student.GetAll();
      List<Course> courseList = Course.GetAll();
      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("studentList", studentList);
      model.Add("courseList", courseList);
      return model;
    }
  }
}
