
###### sql
```sql
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'students'
--
-- ---
--CREATE database university;
--GO
USE university;
GO

DROP TABLE IF EXISTS students;

CREATE TABLE students (
  id INTEGER NOT NULL IDENTITY(1,1),
  name VARCHAR(255) NULL DEFAULT NULL,
  enrollment DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

-- ---
-- Table 'courses'
--
-- ---

DROP TABLE IF EXISTS courses;

CREATE TABLE courses (
  id INTEGER IDENTITY(1,1),
  course_name VARCHAR(255) NULL DEFAULT NULL,
  course_number VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

-- ---
-- Table 'students_courses'
--
-- ---

DROP TABLE IF EXISTS students_courses;

CREATE TABLE students_courses (
  id INTEGER IDENTITY(1,1),
  students_id INTEGER NULL DEFAULT NULL,
  courses_id INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (id)
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE students_courses ADD FOREIGN KEY (students_id) REFERENCES students (id);
ALTER TABLE students_courses ADD FOREIGN KEY (courses_id) REFERENCES courses (id);

```
