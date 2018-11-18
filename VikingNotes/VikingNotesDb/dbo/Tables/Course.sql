--
-- Create Table    : 'Course'   
-- CourseID        :  
-- Name            :  
-- SemesterID      :  (references Semester.SemesterID)
--
--
-- Create Table    : 'Course'   
-- CourseID        :  
-- Name            :  
-- SemesterID      :  (references Semester.SemesterID)
--
CREATE TABLE Course (
    CourseID       BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NCHAR(300) NOT NULL UNIQUE,
    SemesterID     BIGINT NOT NULL,
CONSTRAINT pk_Course PRIMARY KEY CLUSTERED (CourseID),
CONSTRAINT fk_Course FOREIGN KEY (SemesterID)
    REFERENCES Semester (SemesterID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)