--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
--
CREATE TABLE Quiz (
    QuizID         BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserID         BIGINT NOT NULL,
CONSTRAINT pk_Quiz PRIMARY KEY CLUSTERED (QuizID),
CONSTRAINT fk_Quiz FOREIGN KEY (UserID)
    REFERENCES Userr (UserID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)