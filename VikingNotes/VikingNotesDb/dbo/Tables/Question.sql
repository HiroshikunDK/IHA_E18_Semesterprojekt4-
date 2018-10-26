--
-- Create Table    : 'Question'   
-- QuestionID      :  
-- Question        :  
-- QuizID          :  (references Quiz.QuizID)
--
--
-- Create Table    : 'Question'   
-- QuestionID      :  
-- Question        :  
-- QuizID          :  (references Quiz.QuizID)
--
--
-- Create Table    : 'Question'   
-- QuestionID      :  
-- Question        :  
-- QuizID          :  (references Quiz.QuizID)
--
CREATE TABLE Question (
    QuestionID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Question       NCHAR(400) NOT NULL,
    QuizID         BIGINT NOT NULL,
CONSTRAINT pk_Question PRIMARY KEY CLUSTERED (QuestionID),
CONSTRAINT fk_Question FOREIGN KEY (QuizID)
    REFERENCES Quiz (QuizID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)