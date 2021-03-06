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

      Get["/students/{id}"] = parameter =>
      {
        Dictionary<string, object> model = RoutingView.GetStudentView(parameter.id);
        return View["students.cshtml", model];
      };

      Post["/student/{id}"] = parameter =>
      {
        Student student = Student.Find(parameter.id);
        Course course = Course.Find(Request.Form["course"]);
        student.AddCourse(course);
        Dictionary<string, object> model = RoutingView.GetStudentView(parameter.id);
        return View["students.cshtml", model];
      };

      Get["/courses/{id}"] = parameter =>
      {
        Dictionary<string, object> model = RoutingView.GetCourseView(parameter.id);
        return View["courses.cshtml", model];
      };

      Post["/course/{id}"] = parameter =>
      {
        Course course = Course.Find(parameter.id);
        Student student = Student.Find(Request.Form["student"]);
        course.AddStudent(student);
        Dictionary<string, object> model = RoutingView.GetCourseView(parameter.id);
        return View["courses.cshtml", model];
      };


    }
  }
}
