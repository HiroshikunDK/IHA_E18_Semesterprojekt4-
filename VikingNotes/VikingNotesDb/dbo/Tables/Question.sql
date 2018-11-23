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
--
-- Create Table    : 'Question'   
-- QuestionID      :  
-- Question        :  
-- QuizID          :  (references Quiz.QuizID)
-- WrongCount      :  
-- CorrectCount    :  
--
--
-- Create Table    : 'Question'   
-- QuestionID      :  
-- Question        :  
-- QuizID          :  (references Quiz.QuizID)
-- WrongCount      :  
-- CorrectCount    :  
--
--
-- Create Table    : 'Question'   
-- QuestionID      :  
-- Question        :  
-- QuizID          :  (references Quiz.QuizID)
-- WrongCount      :  
-- CorrectCount    :  
--
CREATE TABLE Question (
    QuestionID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Question       NVARCHAR(1000) NOT NULL,
    QuizID         BIGINT NOT NULL,
    WrongCount     BIGINT NOT NULL,
    CorrectCount   BIGINT NOT NULL,
CONSTRAINT pk_Question PRIMARY KEY CLUSTERED (QuestionID),
CONSTRAINT fk_Question FOREIGN KEY (QuizID)
    REFERENCES Quiz (QuizID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)