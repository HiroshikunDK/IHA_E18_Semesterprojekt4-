--
-- Create Table    : 'Semester'   
-- SemesterID      :  
-- SemesterNumber  :  
-- StudyID         :  (references Study.StudyID)
--
CREATE TABLE Semester (
    SemesterID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    SemesterNumber NVARCHAR(5) NOT NULL,
    StudyID        BIGINT NOT NULL,
CONSTRAINT pk_Semester PRIMARY KEY CLUSTERED (SemesterID),
CONSTRAINT fk_Semester FOREIGN KEY (StudyID)
    REFERENCES Study (StudyID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)