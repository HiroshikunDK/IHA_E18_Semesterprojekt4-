--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
--
--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
-- Name            :  
-- Description     :  
-- CatagoryID      :  (references Catagory.CatagoryID)
--
--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
-- Name            :  
-- Description     :  
-- CatagoryID      :  (references Catagory.CatagoryID)
--
--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
-- Name            :  
-- Description     :  
-- CatagoryID      :  (references Catagory.CatagoryID)
--
--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
-- Name            :  
-- Description     :  
-- CatagoryID      :  (references Catagory.CatagoryID)
--
--
-- Create Table    : 'Quiz'   
-- QuizID          :  
-- UserID          :  (references Userr.UserID)
-- Name            :  
-- Description     :  
-- CatagoryID      :  (references Catagory.CatagoryID)
--
CREATE TABLE Quiz (
    QuizID         BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserID         BIGINT NOT NULL,
    Name           NVARCHAR(100) NOT NULL,
    Description    NVARCHAR(1000) NULL,
    CatagoryID     BIGINT NOT NULL,
CONSTRAINT pk_Quiz PRIMARY KEY CLUSTERED (QuizID),
CONSTRAINT fk_Quiz FOREIGN KEY (UserID)
    REFERENCES Userr (UserID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
CONSTRAINT fk_Quiz2 FOREIGN KEY (CatagoryID)
    REFERENCES Catagory (CatagoryID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)