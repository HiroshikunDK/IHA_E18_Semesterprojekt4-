--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
-- Name            :  
-- Description     :  
-- CourseID        :  (references Course.CourseID)
--
CREATE TABLE Quiz (
    QuizID         BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserID         BIGINT NOT NULL,
    Name           NVARCHAR(100) NOT NULL,
    Description    NVARCHAR(1000) NULL,
    CourseID       BIGINT NULL,
CONSTRAINT pk_Quiz PRIMARY KEY CLUSTERED (QuizID),
CONSTRAINT fk_Quiz FOREIGN KEY (UserID)
    REFERENCES Userr (UserID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
CONSTRAINT fk_Quiz2 FOREIGN KEY (CourseID)
    REFERENCES Course (CourseID)
    ON UPDATE CASCADE)