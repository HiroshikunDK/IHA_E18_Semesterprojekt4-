--
-- Create Table    : 'SelectedAnswer'   
-- SelectedAnswerID :  
-- UserID          :  (references Userr.UserID)
-- QuestionID      :  (references Question.QuestionID)
-- IsSelectedCorrect :  
--
--
-- Create Table    : 'SelectedAnswer'   
-- SelectedAnswerID :  
-- IsSelectedCorrect :  
-- QuestionID      :  (references Question.QuestionID)
-- QuizUserStatisticID :  (references QuizUserStatistic.QuizUserStatisticID)
--
CREATE TABLE SelectedAnswer (
    SelectedAnswerID BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    IsSelectedCorrect CHAR(1) NOT NULL,
    QuestionID     BIGINT NOT NULL,
    QuizUserStatisticID BIGINT NULL,
CONSTRAINT pk_SelectedAnswer PRIMARY KEY CLUSTERED (SelectedAnswerID),
CONSTRAINT fk_SelectedAnswer FOREIGN KEY (QuestionID)
    REFERENCES Question (QuestionID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_SelectedAnswer2 FOREIGN KEY (QuizUserStatisticID)
    REFERENCES QuizUserStatistic (QuizUserStatisticID)
    ON UPDATE NO ACTION)