--
-- Create Table    : 'QuizUserStatistic'   
-- QuizUserStatisticID :  
-- correctPercentage :  
-- QuizID          :  (references Quiz.QuizID)
-- UserID          :  (references Userr.UserID)
--
CREATE TABLE QuizUserStatistic (
    QuizUserStatisticID BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    correctPercentage INT NOT NULL,
    QuizID         BIGINT NOT NULL,
    UserID         BIGINT NULL,
CONSTRAINT pk_QuizUserStatistic PRIMARY KEY CLUSTERED (QuizUserStatisticID),
CONSTRAINT fk_QuizUserStatistic FOREIGN KEY (QuizID)
    REFERENCES Quiz (QuizID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_QuizUserStatistic2 FOREIGN KEY (UserID)
    REFERENCES Userr (UserID)
    ON UPDATE NO ACTION)