--
-- Create Table    : 'Answer'   
-- AnswerID        :  
-- Answer          :  
-- IsCorrect       :  
-- QuestionID      :  (references Question.QuestionID)
--
--
-- Create Table    : 'Answer'   
-- AnswerID        :  
-- Answer          :  
-- IsCorrect       :  
-- QuestionID      :  (references Question.QuestionID)
--
--
-- Create Table    : 'Answer'   
-- AnswerID        :  
-- Answer          :  
-- IsCorrect       :  
-- QuestionID      :  (references Question.QuestionID)
--
CREATE TABLE Answer (
    AnswerID       BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Answer         NCHAR(50) NOT NULL,
    IsCorrect      CHAR(1) NOT NULL,
    QuestionID     BIGINT NOT NULL,
CONSTRAINT pk_Answer PRIMARY KEY CLUSTERED (AnswerID),
CONSTRAINT fk_Answer FOREIGN KEY (QuestionID)
    REFERENCES Question (QuestionID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)