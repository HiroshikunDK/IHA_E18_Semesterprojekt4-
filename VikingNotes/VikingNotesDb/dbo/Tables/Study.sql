--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
--
--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
--
--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
-- FacultyID       :  (references Faculty.FacultyID)
--
--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
-- FacultyID       :  (references Faculty.FacultyID)
--
--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
-- FacultyID       :  (references Faculty.FacultyID)
--
--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
-- FacultyID       :  (references Faculty.FacultyID)
--
CREATE TABLE Study (
    StudyID        BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NVARCHAR(100) NOT NULL UNIQUE,
    FacultyID      BIGINT NOT NULL,
CONSTRAINT pk_Study PRIMARY KEY CLUSTERED (StudyID),
CONSTRAINT fk_Study FOREIGN KEY (FacultyID)
    REFERENCES Faculty (FacultyID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)