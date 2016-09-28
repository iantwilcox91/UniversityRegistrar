
###### sql
```sql
-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'students'
--
-- ---

DROP TABLE IF EXISTS `students`;

CREATE TABLE `students` (
  `id` INTEGER NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  `enrollment` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'courses'
--
-- ---

DROP TABLE IF EXISTS `courses`;

CREATE TABLE `courses` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `course_name` VARCHAR(255) NULL DEFAULT NULL,
  `course_number` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'students_courses'
--
-- ---

DROP TABLE IF EXISTS `students_courses`;

CREATE TABLE `students_courses` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `students_id` INTEGER NULL DEFAULT NULL,
  `couses_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE `students_courses` ADD FOREIGN KEY (students_id) REFERENCES `students` (`id`);
ALTER TABLE `students_courses` ADD FOREIGN KEY (couses_id) REFERENCES `courses` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `students` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `courses` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `students_courses` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `students` (`id`,`name`,`enrollment`) VALUES
-- ('','','');
-- INSERT INTO `courses` (`id`,`course_name`,`course_number`) VALUES
-- ('','','');
-- INSERT INTO `students_courses` (`id`,`students_id`,`couses_id`) VALUES
-- ('','','');
```
