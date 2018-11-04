--
-- Create Table    : 'Rating'   
-- RatingID        :  
-- Rating          :  
-- Reason          :  
-- QuizID          :  (references Quiz.QuizID)
--
--
-- Create Table    : 'Rating'   
-- RatingID        :  
-- Rating          :  
-- Reason          :  
-- QuizID          :  (references Quiz.QuizID)
--
CREATE TABLE Rating (
    RatingID       BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Rating         INT NOT NULL,
    Reason         NCHAR(100) NOT NULL,
    QuizID         BIGINT NOT NULL,
CONSTRAINT pk_Rating PRIMARY KEY CLUSTERED (RatingID),
CONSTRAINT fk_Rating FOREIGN KEY (QuizID)
    REFERENCES Quiz (QuizID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)