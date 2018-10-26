--
-- Target: Microsoft SQL Server 
-- Syntax: isql /Uuser /Ppassword /Sserver -i\path\filename.sql
-- Date  : Oct 26 2018 16:51
-- Script Generated by Database Design Studio 2.21.3 
--


--
-- Select Database: 'db_name'
--
USE db_name
GO
 
IF DB_NAME() = 'db_name'
    RAISERROR('''db_name'' DATABASE CONTEXT NOW IN USE.',1,1)
ELSE
    RAISERROR('ERROR IN BATCH FILE, ''USE db_name'' FAILED!  KILLING THE SPID NOW.',22,127) WITH LOG
 
GO
EXECUTE SP_DBOPTION 'db_name' ,'TRUNC. LOG ON CHKPT.' ,'TRUE'
GO

--
-- Create Table    : 'Catagory'   
-- CatagoryID      :  
-- Name            :  
--
CREATE TABLE Catagory (
    CatagoryID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_Catagory PRIMARY KEY CLUSTERED (CatagoryID))
GO

--
-- Create Table    : 'Study'   
-- StudyID         :  
-- Name            :  
--
CREATE TABLE Study (
    StudyID        BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NCHAR(40) NOT NULL UNIQUE,
CONSTRAINT pk_Study PRIMARY KEY CLUSTERED (StudyID))
GO

--
-- Create Table    : 'UserType'   
-- UserTypeID      :  
-- Type            :  
--
CREATE TABLE UserType (
    UserTypeID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Type           NCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_UserType PRIMARY KEY CLUSTERED (UserTypeID))
GO

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
CREATE TABLE Userr (
    UserID         BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserName       NCHAR(20) NOT NULL UNIQUE,
    Password       NCHAR(20) NOT NULL,
    EmailAdress    NCHAR(50) NOT NULL UNIQUE,
    UserTypeID     BIGINT NOT NULL,
    StudyID        BIGINT NOT NULL,
    StudentNumber  NCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_Userr PRIMARY KEY CLUSTERED (UserID),
CONSTRAINT fk_Userr FOREIGN KEY (UserTypeID)
    REFERENCES UserType (UserTypeID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_Userr2 FOREIGN KEY (StudyID)
    REFERENCES Study (StudyID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
GO

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
    Name           NCHAR(20) NOT NULL,
    Description    NCHAR(100) NULL,
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
GO

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
GO

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
GO

--
-- Permissions for: 'public'
--
GRANT ALL ON Catagory TO public
GO
GRANT ALL ON Study TO public
GO
GRANT ALL ON UserType TO public
GO
GRANT ALL ON Userr TO public
GO
GRANT ALL ON Quiz TO public
GO
GRANT ALL ON Question TO public
GO
GRANT ALL ON Answer TO public
GO
