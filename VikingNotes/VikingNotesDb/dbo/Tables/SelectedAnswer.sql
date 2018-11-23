--
-- Create Table    : 'SelectedAnswer'   
-- SelectedAnswerID :  
-- UserID          :  (references Userr.UserID)
-- QuestionID      :  (references Question.QuestionID)
-- IsSelectedCorrect :  
--
CREATE TABLE SelectedAnswer (
    SelectedAnswerID BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserID         BIGINT NULL,
    QuestionID     BIGINT NOT NULL,
    IsSelectedCorrect CHAR(1) NOT NULL,
CONSTRAINT pk_SelectedAnswer PRIMARY KEY CLUSTERED (SelectedAnswerID),
CONSTRAINT fk_SelectedAnswer FOREIGN KEY (UserID)
    REFERENCES Userr (UserID)
    ON UPDATE CASCADE,
CONSTRAINT fk_SelectedAnswer2 FOREIGN KEY (QuestionID)
    REFERENCES Question (QuestionID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)