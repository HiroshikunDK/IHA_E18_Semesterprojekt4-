--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
--
--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
-- EmailAdress     :  
-- UserTypeID      :  (references UserType.UserTypeID)
-- StudyID         :  (references Study.StudyID)
--
--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
-- EmailAdress     :  
-- UserTypeID      :  (references UserType.UserTypeID)
-- StudyID         :  (references Study.StudyID)
-- StudentNumber   :  
--
--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
-- EmailAdress     :  
-- UserTypeID      :  (references UserType.UserTypeID)
-- StudyID         :  (references Study.StudyID)
-- StudentNumber   :  
--
--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
-- EmailAdress     :  
-- UserTypeID      :  (references UserType.UserTypeID)
-- StudyID         :  (references Study.StudyID)
-- StudentNumber   :  
--
--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
-- EmailAdress     :  
-- UserTypeID      :  (references UserType.UserTypeID)
-- StudyID         :  (references Study.StudyID)
-- StudentNumber   :  
-- AuthToken       :  
-- Salt            :  
--
CREATE TABLE Userr (
    UserID         BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserName       NVARCHAR(100) NOT NULL UNIQUE,
    Password       NCHAR(256) NOT NULL,
    EmailAdress    NVARCHAR(50) NOT NULL UNIQUE,
    UserTypeID     BIGINT NOT NULL,
    StudyID        BIGINT NOT NULL,
    StudentNumber  NVARCHAR(20) NOT NULL UNIQUE,
    AuthToken      NVARCHAR(100) NOT NULL,
    Salt           NVARCHAR(20) NOT NULL,
CONSTRAINT pk_Userr PRIMARY KEY CLUSTERED (UserID),
CONSTRAINT fk_Userr FOREIGN KEY (UserTypeID)
    REFERENCES UserType (UserTypeID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_Userr2 FOREIGN KEY (StudyID)
    REFERENCES Study (StudyID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)