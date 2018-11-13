--
-- Create Table    : 'Faculty'   
-- FacultyID       :  
-- Name            :  
--
--
-- Create Table    : 'Faculty'   
-- FacultyID       :  
-- Name            :  
--
CREATE TABLE Faculty (
    FacultyID      BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NCHAR(300) NOT NULL UNIQUE,
CONSTRAINT pk_Faculty PRIMARY KEY CLUSTERED (FacultyID))