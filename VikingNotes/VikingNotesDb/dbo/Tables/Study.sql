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
CREATE TABLE Study (
    StudyID        BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NCHAR(40) NOT NULL UNIQUE,
CONSTRAINT pk_Study PRIMARY KEY CLUSTERED (StudyID))