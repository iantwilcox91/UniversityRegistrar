using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Xunit;
using Nancy;

namespace University
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        Dictionary<string, object> model = RoutingView.GetStudentCourses();
        return View["index.cshtml", model];
      };
      Post["/add_course"] = _ =>
      {
        string courseName = Request.Form["courseName"];
        string courseNumber = Request.Form["courseNumber"];
        Course course = new Course(courseName,courseNumber);
        course.Save();

        Dictionary<string, object> model = RoutingView.GetStudentCourses();
        return View["index.cshtml", model];
      };

      Post["/add_student"] = _ =>
      {
        string studentName = Request.Form["studentName"];
        DateTime enrollment = Request.Form["enrollment"];
        Student student = new Student(studentName,enrollment);
        student.Save();

        Dictionary<string, object> model = RoutingView.GetStudentCourses();
        return View["index.cshtml", model];
      };

      Delete["/delete_all_students"] = _ =>
      {
        Student.DeleteAll();
        Dictionary<string, object> model = RoutingView.GetStudentCourses();
        return View["index.cshtml", model];
      };

      Delete["/delete_all_courses"] = _ =>
      {
        Course.DeleteAll();
        Dictionary<string, object> model = RoutingView.GetStudentCourses();
        return View["index.cshtml", model];
      };

      // Get["/students/{id}"] = parameter =>
      // {
      //   Student student = Student.Find(parameter.id);
      //   List<Course> studentCourses = student.ViewCourses();
      //
      // };


    }


  }
}
